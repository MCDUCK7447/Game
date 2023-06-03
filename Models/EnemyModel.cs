using System.Drawing;

namespace WarPlane.Models
{
    public class EnemyModel
    {
        public SizeF HitBoxSize { get; private set; }

        public PointF Position { get; private set; }

        public EnemyModel(PointF position, SizeF hitBoxSize)
        {
            Position = position;
            HitBoxSize = hitBoxSize;
        }

        public void Move()
        {
            Position += new SizeF(0, 0.005f);
        }
    }
}