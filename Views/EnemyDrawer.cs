using System.Drawing;
using WarPlane.Models;

namespace WarPlane.Views
{
    public static class EnemyDrawer
    {
        private static readonly Image _enemySprite = Image.FromFile("Sprites/Enemy.png");
        
        public static void Draw(EnemyModel enemyModel, Graphics graphics)
        {
            var enemyX = (int) (enemyModel.Position.X * ViewConstants.ViewSize.Width);
            var enemyY = (int) (enemyModel.Position.Y * ViewConstants.ViewSize.Height);
            graphics.DrawImage(_enemySprite, new Point(enemyX, enemyY));
        }
    }
}
