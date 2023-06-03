using System.Drawing;

namespace WarPlane.Models
{
    public class BulletModel
    {
        public SizeF HitBoxSize { get; }
        
        public EntityType BulletOwner { get; }

        private bool _isFlipped => BulletOwner == EntityType.Enemy;
        
        public PointF Position { get; private set; }

        public BulletModel(PointF position, EntityType bulletOwner, SizeF hitBox)
        {
            Position = position;
            HitBoxSize = hitBox;
            BulletOwner = bulletOwner;
        }

        public void Move()
        {
            Position += new SizeF(0, 0.01f * (_isFlipped ? 1 : -1));
        }
    }
}