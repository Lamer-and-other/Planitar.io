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
        public MainForm()
        {
            InitializeComponent();
         
          //  if (map.Players.Count != 0)
         //   {
           
          //  }
           // else map.Players.Add(new Player());
        }

       // Graphics graphs;
        Rectangle rect;
        Bitmap bmp;
        Map map = new Map();
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          count = 10;
          DrawBoard();

        }
        int count=0;
        float otstup=40f;
        int height = 20;
        
        void DrawBoard()
        {
            bmp = new Bitmap(panel1.Width, panel1.Height);
            using (var graph = Graphics.FromImage(bmp))
            {
            
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                graph.FillRectangle(new SolidBrush(panel1.BackColor), 0, 0, panel1.Width, panel1.Height);
                    rect = new Rectangle(panel1.Width - 180, 10, 170, 52 + height*count);
                SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.LightGray));
                graph.FillRectangle(brush, rect);
                graph.DrawString("Leaderboard", new Font("Comic Sans MS", 15f, FontStyle.Bold), Brushes.White, new PointF(panel1.Width - 153f, 12f));
                try
                {
                    if (map.Players != null || map.Players.Count != 0)
                    {
                        foreach (var ts in map.Players)
                        {
                            graph.DrawString(count + ". " + ts.Nickname, new Font(Font, FontStyle.Regular), Brushes.White, new PointF(panel1.Width - 170f, 15f));
                            count++;
                        }
                    }
                    else
                    {
                        graph.DrawString(count + ". Тута пуста", new Font(Font, FontStyle.Regular), Brushes.White, new PointF(panel1.Width - 180f, 15f));
                    }
                }
                catch
                {
                   
                    graph.DrawString("1.Nobody", new Font("Comic Sans MS", 11f, FontStyle.Bold), Brushes.White, new PointF(panel1.Width - 178f, otstup));
                    for (int i = 1; i <= count; i++)
                    {
                        graph.DrawString(i+1 + ".Nobody", new Font("Comic Sans MS", 11f, FontStyle.Bold), Brushes.White, new PointF(panel1.Width - 178f, otstup += 20f));
          
                    }
                    count = 1;

                }
                count = 1;
                otstup = 40f;

            }
            panel1.CreateGraphics().DrawImage(bmp, 0, 0);
        }

       // <p id='1', style='border:color white'>что то</p>

        void AddBoardTopPlayers()
        {

        }

        int p = 0;
        private void panel1_Resize(object sender, EventArgs e)
        {
            // panel1.Invalidate();
            if (p == 10)
            {
                count = new Random().Next(1, 11);
                DrawBoard();
                p = 0;
            }
            p+=1;
        }
    }
}
