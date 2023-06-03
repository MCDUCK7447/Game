using System;
using System.Drawing;

namespace WarPlane.Models
{
    public class PlayerModel
    {
        public event Action PlayerDied;
        
        public SizeF HitBoxSize { get; }
        public int Health { get; private set; }

        public int MaxHealth { get; } = 3;

        public PointF Position { get; private set; }
        
        public PlayerModel(SizeF hitBoxSize)
        {
            Health = MaxHealth;
            Position = new PointF(0.5f, 0.8f);
            HitBoxSize = hitBoxSize;
        }

        public void Move(SizeF delta)
        {
            Position += delta;
        }

        public void GetDamage()
        {
            Health--;

            if (Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            PlayerDied?.Invoke();
        }
    }
}