using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Planetario
{
    public partial class MainForm : Form
    {
        public static MainForm thisForm;    // Ссылка на форму

        public static BufferedGraphics panelBuffer;         
                      BufferedGraphicsContext panelContext;
                      Graphics panelGraphics;

        public static Point globalCenter = new Point(0, 0); // Центр для отрисовки всех фигур
        public static Point myMouse = new Point(0, 0);      // Координаты мыши

        public static Map gameMap = new Map();              // Карта игры


        public MainForm()
        {
            InitializeComponent();

            thisForm = this;

            SetGlobalCenter();  // Определяем центр формы
            DoubleBuffering();  // Устанавливаем двойнную буферизацию для панели
            ChangeCenter();

            T_MouseMove.Start();
        }
        public void SetGlobalCenter()
        {
            // Установка центра панели
            globalCenter = new Point(panel1.Width / 2, panel1.Height / 2);
        }
        private void T_MouseMove_Tick(object sender, EventArgs e)
        {
            // Каждый тик узнаем новые координаты мыши и перемещаем все вокруг игрока
            gameMap.MoveThisPlayer();

            // Каждый тик отрисовываем все по новой
            DrawThis();
        }
        public void ChangeCenter()
        {
            // Меняем центр относительно панели
            gameMap.CenterPlayer(new Point(panel1.Width / 2, panel1.Height / 2));
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // Запоминаем координаты мыши для дальнейшего перемещения
            myMouse = e.Location;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            // Пересоздаем графику для отрисовки в панели
            DoubleBuffering();

            // Меняем центр для отрисовки отностельно панели
            gameMap.CenterPlayer(new Point(panel1.Width / 2, panel1.Height / 2));
        }
       
        public void DoubleBuffering()
        {
            // Графика
            panelContext = BufferedGraphicsManager.Current;
            panelGraphics = panel1.CreateGraphics();
            panelBuffer = panelContext.Allocate(panelGraphics, panel1.ClientRectangle);
            panelBuffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }
        public void DrawThis()
        {
            // Метод для отрисовки всех фигур
            try
            {
                // Если все необходимое для отрисовки присутствует, рисуем
                gameMap.DrawIt();
               
            }
            catch
            {
                // Если нету, пересоздаем графику
                DoubleBuffering();
            }
        }

       
    }
}
