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
    public delegate void InvokePrintMessages(string m);

    public partial class Form1 : Form
    {
        //public static Form myself = null; 
        MyService ms;
        string MESSAGE = ""; 
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); 
        public Form1()
        {
            InitializeComponent();
            Canal canal = new Canal("127.0.0.1", 2020); 
            ms = new MyService(canal);
            ms.SetDelegats(getSomeMessage);
            timer.Interval = 1;
            timer.Tick += _Tick;
          
        }
      
        public void _Tick(object o, EventArgs e)
        {

        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {           
            ms.sendSomeMessage();
            timer.Start();
        }
        
        public void getSomeMessage(string message)
        {

            //this.BeginInvoke((Action)(() =>
            //{
            //    MessageBox.Show(message); 
            //}));

            //this.BeginInvoke((Action)(() =>
            //{
            //    this.textBox1.Text = message; 
            //}));

            BeginInvoke(new MethodInvoker(delegate
            {
                this.textBox1.Text = message; 
            }));           
        }
        
        public string printMessage(string message)
        {
            return message; 
        }
    }
}
