using System;
using System.Windows.Forms;

namespace WarPlane
{
    public class GameView : Form
    {
        private GameController _gameController;
        private PlayerView _playerView;
        private PlayerModel _playerModel;
        
        public GameView() => InitializeGameView();
        
        private void InitializeGameView()
        {
            _playerModel = new PlayerModel();
           _playerView = new PlayerView();
           _gameController = new GameController();

           Controls.Add(_playerView.AirplaneView);
           this.Text = "aboba";
        }
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Maximized;
        }
    }
}