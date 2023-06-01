using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarPlane
{
    public partial class MainMenuView : Form
    {
        private Panel _panel;
        private Button _newGameButton;
        private Button _highScoresButton;
        private Button _exitGameButton;
        private MainMenuController _mainMenuController;
        private MainMenuModel _mainMenuModel;
        
        public event Action NewGameClicked;
        public event Action HighScoresClicked;
        public event Action ExitGameClicked;

        public MainMenuView() =>  InitializeMainMenuView();
        public void HideMainMenu() => Hide();

        private void InitializeMainMenuView()
        {
            CreatePanel();
            CreateNewGameButton();
            CreateHighScoresButton();
            CreateExitGameButton();
            CenterButtons();

            _mainMenuController = new MainMenuController(this);
        }

        private void CreatePanel()
        {
            _panel = new Panel();
            _panel.Dock = DockStyle.Fill;
            Controls.Add(_panel);
        }
        
        private void CreateNewGameButton()
        {
            _newGameButton = new Button();
            _newGameButton.Text = "Новая игра";
            _newGameButton.Font = new Font(_newGameButton.Font.FontFamily, 20, FontStyle.Bold);
            _newGameButton.Anchor = AnchorStyles.None;
            _newGameButton.Size = new Size(300, 150);
            _newGameButton.Click += (sender, e) => NewGameClicked?.Invoke();
            
            _panel.Controls.Add(_newGameButton);
        }

        private void CreateHighScoresButton()
        {
            _highScoresButton = new Button();
            _highScoresButton.Text = "Посмотреть рекорды";
            _highScoresButton.Font = new Font(_highScoresButton.Font.FontFamily, 20, FontStyle.Bold);
            _highScoresButton.Anchor = AnchorStyles.None;
            _highScoresButton.Size = new Size(300, 150);
            _highScoresButton.Click += (sender, e) => HighScoresClicked?.Invoke();
            
            _panel.Controls.Add(_highScoresButton);
        }

        private void CreateExitGameButton()
        {
            _exitGameButton = new Button();
            _exitGameButton.Text = "Выйти из игры";
            _exitGameButton.Font = new Font(_exitGameButton.Font.FontFamily, 20, FontStyle.Bold);
            _exitGameButton.Anchor = AnchorStyles.None;
            _exitGameButton.Size = new Size(300, 150);
            _exitGameButton.Click += (sender, e) => ExitGameClicked?.Invoke();
            
            _panel.Controls.Add(_exitGameButton);
        }

        private void CenterButtons()
        {
            const int buttonSpacing = 30;
            
            var panelWidth = _panel.ClientSize.Width;
            var panelHeight = _panel.ClientSize.Height;

            var buttonWidth = _newGameButton.Width;
            var buttonHeight = _newGameButton.Height;
            
            var totalButtonHeight = (buttonHeight * 3) + (buttonSpacing * 2);

            var buttonX = (panelWidth - buttonWidth) / 2;
            var buttonY = (panelHeight - totalButtonHeight) / 2;

            _newGameButton.Location = new Point(buttonX, buttonY);
            _highScoresButton.Location = new Point(buttonX, buttonY + buttonHeight + buttonSpacing);
            _exitGameButton.Location = new Point(buttonX, buttonY + (buttonHeight + buttonSpacing) * 2);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Maximized;
        }
    }
}