using System.Drawing;
using WarPlane.Models;

namespace WarPlane.Views
{
    public static class BulletDrawer
    {
        private static readonly Image _enemySprite = Image.FromFile("Sprites/Bullet.png");
        private const int BulletOffsetX = 25;

        public static void Draw(BulletModel bulletModel, Graphics graphics)
        {
            var enemyX = (int) (bulletModel.Position.X * ViewConstants.ViewSize.Width);
            var enemyY = (int) (bulletModel.Position.Y * ViewConstants.ViewSize.Height);
            graphics.DrawImage(_enemySprite, new Point(enemyX + BulletOffsetX, enemyY));
        }
    }
}