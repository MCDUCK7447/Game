using System.Windows.Forms;

namespace WarPlane
{
    public class MainMenuController
    {
        private readonly MainMenuView _mainMenuView;
        private GameController _gameController;
        private GameView _gameView;

        public MainMenuController(MainMenuView mainMenuView)
        {
            _mainMenuView = mainMenuView;
            
            _mainMenuView.NewGameClicked += NewGameClicked;
            _mainMenuView.HighScoresClicked += HighScoresClicked;
            _mainMenuView.ExitGameClicked += ExitGameClicked;

            _gameController = new GameController();
        }
        
        private void NewGameClicked()
        {
            _mainMenuView.Hide();
            _gameView = new GameView();
            _gameView.Show();
        }

        private void HighScoresClicked()
        {
            
        }

        private static void ExitGameClicked() => Application.Exit();
    }
}