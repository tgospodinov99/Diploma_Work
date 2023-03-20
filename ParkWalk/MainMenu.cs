using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkWalk
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void buttonEasy_Click(object sender, EventArgs e)
        {
            GameForm form1 = new GameForm(5);
            form1.Show();
        }

        private void buttonMedium_Click(object sender, EventArgs e)
        {
            GameForm form1 = new GameForm(7);
            form1.Show();
        }

        private void buttonHard_Click(object sender, EventArgs e)
        {
            GameForm form1 = new GameForm(9);
            form1.Show();
        }
    }
}
