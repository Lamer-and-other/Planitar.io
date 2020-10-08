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
        MyService ms;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); 
        public Form1()
        {
            InitializeComponent();
            Canal canal = new Canal("127.0.0.1", 2020); 
            ms = new MyService(canal);
            ms.SetDelegats(selfIdentity, resetName, setNewData, updataPlayerList); 
            timer.Interval = 1; 
            timer.Tick += _Tick;
            //timer.Start();
            ms.connectToServer();
            
        }
      
        public void _Tick(object o, EventArgs e)
        {
            timer.Stop(); 
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            if(NameBox.Text.Count() != 0)
            {
                ms.changeNickName(NameBox.Text);  

            }
        }

        public void resetName(string newName)
        {
            Player.myseft.Nickname = newName;
            BeginInvoke(new MethodInvoker(delegate
            {
                this.NameBox.Text = Player.myseft.Nickname;
            }));
            
        } 
        public void selfIdentity(int id, string name) 
        {
            Player.myseft = new Player(id, name);
                
            BeginInvoke(new MethodInvoker(delegate
            {
                this.NameBox.Text = Player.myseft.Nickname; 
            }));
            ms.getPlayers();
        }
        
        public void setNewData(int someData)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                this.label1.Text = someData.ToString();  
            }));
        }

        public void getPlayerList()
        {
            foreach (Player p in Player.playerList) 
                PlayerList.Items.Add(p.Nickname + ":" + p.Record); 
        }
        
        public void updataPlayerList()
        {            
            BeginInvoke(new MethodInvoker(delegate
            {
                PlayerList.Items.Clear();
                foreach (Player p in Player.playerList)
                    PlayerList.Items.Add("id: " + p.id.ToString() + " - " + p.Nickname);
            }));
          
        } 

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ms.Disconnect(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ms.chekNotify(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ms.getPlayers();
        }
    }
}
