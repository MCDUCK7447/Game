using System.Drawing;
using System.Windows.Forms;

namespace WarPlane
{
    public class PlayerView
    {
        public PictureBox AirplaneView;

        public PlayerView()
        {
            AirplaneView = new PictureBox();
            //AirplaneView.Image = ;
            AirplaneView.SizeMode = PictureBoxSizeMode.StretchImage;
            AirplaneView.Location = new Point(300, 300);
        }

        public void UpdatePosition(int x, int y)
        {
            AirplaneView.Left = x;
            AirplaneView.Top = y;
        }
    }
}