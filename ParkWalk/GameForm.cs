using ParkWalk.Engines;
using ParkWalk.Model;
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
    public partial class GameForm : Form
    {
        GameEngine engine;

        
        public GameForm(int NodeCount)
        {
            InitializeComponent();
            Graph gameArea = GraphGenerator.Generate(NodeCount);
            Player player = new Player(gameArea);
            engine = new GameEngine(player);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Image imag = Image.FromFile("./background/diplomna_podlojka.png");
            e.Graphics.DrawImage(imag, 0, 0, imag.Width, imag.Height);
            engine.Draw(e.Graphics);
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel.Text = e.Location.ToString();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(string command in textBox1.Text.Split('\n', '\r'))
                {
                    engine.ExecuteCommand(command);
                }
                if (engine.CheckWinner())
                {
                    MessageBox.Show($"{engine.ShowEndgameStats()}\nThe shortest path is:{string.Join(" -> ", engine.player.gameArena.GetShortestPath())}");
                    Close();
                }
                textBox1.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                panel.Invalidate();
            }
        }
    }
}
