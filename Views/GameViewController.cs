using System;
using System.Drawing;
using System.Windows.Forms;
using WarPlane.Controllers;
using WarPlane.Models;

namespace WarPlane.Views
{
    public class GameViewController : Form
    {
        private GameModel _gameModel;
        public GameViewController() => InitializeGameView();

        private GameTimerController _timerController = new GameTimerController(10);
        private MemoryKeyboardHandler _keyboardHandler = new MemoryKeyboardHandler();

        private void InitializeGameView()
        {
            _gameModel = new GameModel();
            //          _gameModel.SpawnInteralChanged += GameModelOnSpawnInteralChanged;
            _gameModel.GameBeenOver += OnGameOver;

            _timerController.RegisterAction(UpdateGame, 1);
            _timerController.RegisterAction(SpawnEnemy, 100);
            _timerController.RegisterAction(EnemyAttack, 50);

            _keyboardHandler.RegisterKeyHandler(Keys.Left, MovePlayerLeft);
            _keyboardHandler.RegisterKeyHandler(Keys.Right, MovePlayerRight);
            _keyboardHandler.RegisterKeyHandler(Keys.Space, _gameModel.ShootOrWaitCooldown);

            MaximizeBox = false;
            Size = ViewConstants.ViewSize;
            DoubleBuffered = true;
            Text = "Game";
        }

        private void EnemyAttack()
        {
            _gameModel.StartEnemyAttack();
        }

        private void OnGameOver()
        {
            _timerController.Stop();

            BeginInvoke((Action)(() =>
            {
                Hide();
                var mainMenu = new MainMenuView();
                mainMenu.Show();
            }));
        }

        private void MovePlayerLeft() => _gameModel.PlayerModel.Move(new SizeF(-0.01f, 0));

        private void MovePlayerRight() => _gameModel.PlayerModel.Move(new SizeF(0.01f, 0));

        private void SpawnEnemy()
        {
            _gameModel.SpawnEnemy();
            Invalidate();
        }

        private void UpdateGame()
        {
            _gameModel.UpdateGame();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            var graphics = e.Graphics;
            PlayerDrawer.Draw(_gameModel.PlayerModel, graphics);
            PlayerDrawer.DrawHealth(_gameModel.PlayerModel, graphics);

            foreach (var enemy in _gameModel.Enemies)
            {
                EnemyDrawer.Draw(enemy, graphics);
            }

            foreach (var bullet in _gameModel.Bullets)
            {
                BulletDrawer.Draw(bullet, graphics);
            }
            

            base.OnPaint(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _keyboardHandler.AddPressedKey(e.KeyCode);
            _keyboardHandler.InvokeNecessaryActions();

            Invalidate();

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _keyboardHandler.RemovePressedKey(e.KeyCode);
            _keyboardHandler.InvokeNecessaryActions();

            Invalidate();

            base.OnKeyUp(e);
        }
    }
}