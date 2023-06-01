using System.Windows.Forms;

namespace WarPlane
{
    public class PlayerController
    {
        private PlayerModel _airplaneModel;
        private PlayerView _airplaneView;
        private GameView _gameView;

        public PlayerController(PlayerModel airplaneModel, PlayerView airplaneView, GameView gameView)
        {
            _airplaneModel = airplaneModel;
            _airplaneView = airplaneView;
            _gameView = gameView;

            _gameView.Controls.Add(_airplaneView.AirplaneView);
        }

        public void ShowAirplane()
        {
            _airplaneView.UpdatePosition(_gameView.Width / 2 - _airplaneView.AirplaneView.Width / 2,
                _gameView.Height / 2 - _airplaneView.AirplaneView.Height / 2);
            _airplaneView.AirplaneView.Visible = true;
        }
    }
}