using System;
using System.Drawing;
using WarPlane.Models;

namespace WarPlane.Views
{
    public static class PlayerDrawer
    {
        private static readonly Image _playerSprite = Image.FromFile("Sprites/Player.png");
        private static readonly Image _heartSprite = Image.FromFile("Sprites/Heart.png");

        public static void Draw(PlayerModel playerModel, Graphics graphics)
        {
            var playerX = (int) (playerModel.Position.X * ViewConstants.ViewSize.Width);
            var playerY = (int) (playerModel.Position.Y * ViewConstants.ViewSize.Height);
            graphics.DrawImage(_playerSprite,new Point(playerX, playerY));
        }

        public static void DrawHealth(PlayerModel playerModel, Graphics graphics)
        {
            var healthInPercents = playerModel.Health / (float) playerModel.MaxHealth;
            const int maxHearts = 3;

            var position = new Point(30, 30);
            var heartWidth = _heartSprite.Width;
            
            for (var i = 0; i < Math.Round(maxHearts * healthInPercents); i++)
            {
                graphics.DrawImage(_heartSprite, position with { X = position.X + (heartWidth + 10) * i });
            }
        }
    }
}