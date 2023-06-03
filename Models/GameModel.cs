using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WarPlane.Models
{
    public class GameModel
    {
        public event Action GameBeenOver;

        public event Action SpawnInteralChanged;

        private int _spawnInterval;

        private int SpawnInterval
        {
            get => _spawnInterval;
            set
            {
                _spawnInterval = value;
                SpawnInteralChanged?.Invoke();
            }
        }

        private const int CooldownInterval = 10;
        private int? _cooldownCounter;

        public PlayerModel PlayerModel { get; }
        public List<EnemyModel> Enemies { get; }
        public List<BulletModel> Bullets { get; }

        private readonly Random _random = new Random();

        private readonly object _lockObject = new();

        public GameModel()
        {
            SpawnInterval = 5000;

            PlayerModel = new PlayerModel(new SizeF(0.05f, 0.05f));
            PlayerModel.PlayerDied += GameOver;
            
            Enemies = new List<EnemyModel>();
            Bullets = new List<BulletModel>();
        }
        
        public void UpdateGame()
        {
            UpdateEnemies();
            UpdateBullets();
            UpdateShootCooldown();

            CheckEnemiesCollision();
            CheckPlayerCollision();
        }

        private void CheckPlayerCollision()
        {
            if (!TryGetPlayerCollision(out var bullet)) return;
            PlayerModel.GetDamage();
            Bullets.Remove(bullet);
        }

        private void CheckEnemiesCollision()
        {
            var collisions = GetCollidedEnemies();
            foreach (var record in collisions)
            {
                Enemies.Remove(record.CollidedObject);
                Bullets.Remove(record.CollidedBy);
            }
        }

        private void UpdateEnemies() => Enemies.ForEach(enemy => enemy.Move());

        private void UpdateBullets() => Bullets.ForEach(bullet => bullet.Move());

        private void UpdateShootCooldown()
        {
            if (_cooldownCounter is not null)
                _cooldownCounter++;

            if (_cooldownCounter >= CooldownInterval)
                _cooldownCounter = null;
        }

        public void SpawnEnemy()
        {
            lock (_lockObject)
            {
                var enemy = new EnemyModel(new PointF((float)_random.NextDouble(), 0), new SizeF(0.05f, 0.05f));
                Enemies.Add(enemy);
            }
        }

        public void ShootOrWaitCooldown()
        {
            if (_cooldownCounter is not null)
                return;

            Shoot(PlayerModel.Position, EntityType.Player);
            _cooldownCounter = 0;
        }

        public void StartEnemyAttack()
        {
            lock (_lockObject)
                foreach (var enemy in Enemies)
                    Shoot(enemy.Position, EntityType.Enemy);
        }
        
        private void Shoot(PointF position, EntityType bulletOwner)
        {
            var bullet = new BulletModel(position, bulletOwner, new SizeF(0.01f, 0.01f));
            Bullets.Add(bullet);
        }
        
        private List<CollisionRecord> GetCollidedEnemies()
        {
            var collidedEnemies = new List<CollisionRecord>();

            lock (_lockObject)
                foreach (var enemy in Enemies)
                {
                    var enemyHitbox = new RectangleF(enemy.Position, enemy.HitBoxSize);
                    foreach (var bullet in Bullets.Where(b => b.BulletOwner == EntityType.Player))
                    {
                        var bulletHitbox = new RectangleF(bullet.Position, bullet.HitBoxSize);
                        if (enemyHitbox.IntersectsWith(bulletHitbox))
                            collidedEnemies.Add(new CollisionRecord(enemy, bullet));
                    }
                }

            return collidedEnemies;
        }


        private bool TryGetPlayerCollision(out BulletModel collidedBullet)
        {
            var playerHitbox = new RectangleF(PlayerModel.Position, PlayerModel.HitBoxSize);

            lock (_lockObject)
            {
                foreach (var bullet in Bullets.Where(b => b.BulletOwner == EntityType.Enemy))
                {
                    var bulletHitbox = new RectangleF(bullet.Position, bullet.HitBoxSize);
                    if (!bulletHitbox.IntersectsWith(playerHitbox)) continue;
                    collidedBullet = bullet;
                    return true;
                }
            }

            collidedBullet = null;
            return false;
        }

        private void GameOver() => GameBeenOver?.Invoke();
    }

    public class CollisionRecord
    {
        public EnemyModel CollidedObject { get; }
        public BulletModel CollidedBy { get; }

        public CollisionRecord(EnemyModel enemy, BulletModel bullet)
        {
            CollidedObject = enemy;
            CollidedBy = bullet;
        }
    }
}