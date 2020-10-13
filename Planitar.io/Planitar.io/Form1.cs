using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers; 

namespace Planitar.io
{
    delegate void identification(int id, string name);
    delegate void reName(string newName);
    delegate void reDrawing(int someData);
    delegate void updataPlayerList();
    
    public delegate void InvokePrintMessages(string m);

    public partial class Form1 : Form
    {

        public static Form1 thisForm;    // Ссылка на форму
        public static BufferedGraphics panelBuffer;
        BufferedGraphicsContext panelContext;
        Graphics panelGraphics;

        public static Point globalCenter = new Point(0, 0); // Центр для отрисовки всех фигур
        public static Point myMouse = new Point(0, 0);      // Координаты мыши

        Map gameMap = new Map();              // Карта игры

        

        MyService ms = null;
        public Form1()
        {
            InitializeComponent();
            Canal canal = new Canal("127.0.0.1", 2020);
            canal.map = gameMap; 
            ms = new MyService(canal); 
            ms.SetDelegats(selfIdentity, resetName, setNewData, updataPlayerList); 
            Player.myseft.Color =  Color.FromArgb(0, 255, 0); 
            thisForm = this;
            
            SetGlobalCenter();  // Определяем центр формы
            DoubleBuffering();  // Устанавливаем двойнную буферизацию для панели
            ChangeCenter();

            T_MouseMove.Interval = 1000 / 60;   
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

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            // Запоминаем координаты мыши для дальнейшего перемещения
            myMouse = e.Location;
        }
        
        private void panel_Resize(object sender, EventArgs e)
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










        ////////////////////////////////////////////////////////////////////////////////////////////











        private void Form1_Shown(object sender, EventArgs e)
        {
            ms.connectToServer();
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            NameBox.Text = NameBox.Text.Replace(" ", "_");
            string text = NameBox.Text;
            if (text != Player.oldName)
            {
                if (NameBox.Text.Count() != 0)
                {
                    ms.changeNickName(NameBox.Text);
                }
            }
        }      

        // идентификация 
        public void selfIdentity(int id, string name)
        {
            Player.myseft = new Player(id, name); 
            Player.oldName = name; 
            //BeginInvoke(new MethodInvoker(delegate 
            //{
            //    this.NameBox.Text = Player.myseft.Nickname;
            //}));        
            ms.getPlayers();           
           
        }       
        // переименовка игрока на клиенте  
        public void resetName(string newName)
        {
            Player.myseft.Nickname = newName; 
            BeginInvoke(new MethodInvoker(delegate
            {
                this.NameBox.Text = Player.myseft.Nickname;
            }));
            ms.getPlayers();
        }
        
        // кнопка "В бой" 
        private void actionButton_Click(object sender, EventArgs e)
        {
            ms.startGame(Player.myseft.id);
            WellcomePanel.Visible = false; 
        }
        // пестовое изменение значения        
        public void setNewData(int someData)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                this.labelChekNotifyLable.Text = someData.ToString();  
            }));
        }       
        
        // обновление списка игроков 
        public void updataPlayerList()
        {            
            BeginInvoke(new MethodInvoker(delegate
            {
                PlayerList.Items.Clear();
                foreach (Player p in Player.playerList)
                    PlayerList.Items.Add("id: " + p.id.ToString() + " - " + p.Nickname);
            }));          
        } 
        
        // при закрытии программы отправляется запрос об отсоединении на сервер 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ms.Disconnect(); 
        }
        
        // тестовая проверка получениdя игроков 
        private void getPlayerByHandButton_Click(object sender, EventArgs e)
        {
            ms.getPlayers();
        }
        // тестовая проверка получения новых значений для остальных игроков 
        private void chekNotifyButton_Click(object sender, EventArgs e)
        {
            ms.chekNotify();
        }
    }
}
