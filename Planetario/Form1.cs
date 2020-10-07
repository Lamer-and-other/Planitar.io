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
        int H, W; 
        Bitmap bmp;
        Point oldpos = new Point();
        Graphics g;
        Map map;
        Point corner = new Point(0, 0);

        public MainForm()
        {
            InitializeComponent();
            map = new Map();
            g = panel1.CreateGraphics();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oldpos = e.Location;
                map.Paint_old(g);
                bmp = new Bitmap(GameConst.MaxWidthMap, GameConst.MaxHeightMap);
                H = bmp.Height - panel1.Height;
                W = bmp.Width - panel1.Width;
            }
            else if (e.Button == MouseButtons.Right)
            {
                g.DrawImage(bmp, 0, 0);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // если нажата левая кнопка мыши
            {
                int dx = e.X - oldpos.X;
                int dy = e.Y - oldpos.Y;

                corner.X -= dx; // смещяем координату по х
                corner.Y -= dy; // смещяем координату по у

                Rectangle DestRect = new Rectangle(0, 0, bmp.Width, bmp.Height); // получаем прямоугольник который мы перемещяем куда-то
                Rectangle SrcRect = new Rectangle(corner.X, corner.Y, bmp.Width, bmp.Height); // отображениекартинки

                g.DrawImage(bmp, DestRect, SrcRect, GraphicsUnit.Pixel); // запись перемещенной картинки в пикчербоксы

                oldpos = e.Location; // присваеваем старой позиции текущую

                if (corner.X < 0) corner.X = 0; // if-ы для того чтоб мы не выходили за пределы картинки со всех сторон
                else if (corner.Y < 0) corner.Y = 0;
                else if (corner.X > W) corner.X = W;
                else if (corner.Y > H) corner.Y = H;
            }
        }
    }
}
