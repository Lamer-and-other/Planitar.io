using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planetario
{
    public partial class MainForm : Form
    {
        Player p = new Player();
        Graphics g;
        Map map;
        Point corner = new Point(0, 0);

        public MainForm()
        {
            InitializeComponent();
            map = new Map();
            g = panel1.CreateGraphics();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            map.Eat(e,p);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            map.Scene(g, panel1);
        }
    }
}
