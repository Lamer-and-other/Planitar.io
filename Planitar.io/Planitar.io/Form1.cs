using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planitar.io
{
    delegate void messages(string message);
    public delegate void InvokePrintMessages(string m);

    public partial class Form1 : Form
    {
        MyService ms;
        string m = "";
        public Form1()
        {
            InitializeComponent();
            Canal canal = new Canal("127.0.0.1", 2020); 
            ms = new MyService(canal);
            ms.SetDelegats(getSomeMessage); 
        }
      
        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            ms.sendSomeMessage(); 
        }
        
        void getSomeMessage(string message)
        {
            m = message; 
            textBox1.Invoke(new InvokePrintMessages(printMessage));
        }

        public void printMessage(string message)
        {
            textBox1.Text = m; 
        }
    }
}
