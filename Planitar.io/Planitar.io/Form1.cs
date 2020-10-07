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
    delegate void messages(string message);
    delegate void reDrawing(int someData); 
        
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
            ms.SetDelegats(getSomeMessage, setNewData); 
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
        
        public void getSomeMessage(string message)
        {
            //this.BeginInvoke((Action)(() =>
            //{
            //    this.textBox1.Text = message; 
            //}));
            
            BeginInvoke(new MethodInvoker(delegate
            {
                this.NameBox.Text = message; 
            }));           
        }
        
        public void setNewData(int someData)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                this.label1.Text = someData.ToString(); 
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
    }
}
