using System.Drawing;
using System.Windows.Forms;
using WarPlane.Views;

namespace WarPlane.Controllers
{
    public class MainMenuController
    {
        private readonly MainMenuView _mainMenuView;
        private GameViewController _gameViewController;

        public MainMenuController(MainMenuView mainMenuView)
        {
            _mainMenuView = mainMenuView;
            
            _mainMenuView.NewGameClicked += OnNewGameClick;
            _mainMenuView.ExplanationClicked += OnExplanationClick;
            _mainMenuView.ExitGameClicked += OnExitGameClick;
        }
        
        private void OnNewGameClick()
        {
            _gameViewController = new GameViewController();
            _gameViewController.Show();
            _mainMenuView.Hide();
        }

        private static void OnExplanationClick() => CreateExplanationForm();

        private static void CreateExplanationForm()
        {
            var explanationForm = new Form();
            var explanationPicture = new PictureBox();
            explanationPicture.Size = new Size(1920, 1080);
            explanationPicture.Image = Image.FromFile("Sprites/tutorial.png");
            explanationPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            explanationForm.Controls.Add(explanationPicture);
            explanationForm.Show();
        }

        private static void OnExitGameClick() => Application.Exit();
    }
}