using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormPlanetario
{
    public partial class Form1 : Form
    {
        Graphics panel_g;
        Map map;

        public Form1()
        {
            InitializeComponent();

            panel_g = panel1.CreateGraphics();
            map = new Map();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            map.Eat(e);
            label1.Text = "Очки: " + map.Score;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Scene(panel_g, panel1);
        }

        // перерисовка
        public void Scene(Graphics g, Panel panel)
        {
            Bitmap bmp = new Bitmap(panel.Width, panel.Height, g);
            Graphics g1 = Graphics.FromImage(bmp);

            g1.Clear(panel.BackColor);
            map.DrawFood(g1);

            g.DrawImage(bmp, 0, 0);
        }
    }
}
