using System;
using System.Windows.Forms;

namespace TestLoginGmail
{
    public partial class Form1 : Form
    {
        private int counter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender,
                                EventArgs e)
        {
            counter = 0;
            timer1.Enabled = true;
            Hide();
            ShowInTaskbar = false;
            Left = (SystemInformation.WorkingArea.Size.Width - Size.Width);
            Top = (SystemInformation.WorkingArea.Size.Height - Size.Height);
        }

        private void timer1_Tick(object sender,
                                 EventArgs e)
        {
            Show();
            if(counter < GlobalVariables.emailFrom.Count)
            {
                lblFrom.Text = "From: " + GlobalVariables.emailFrom[counter];
                lblMessage.Text = "Subject: " + GlobalVariables.emailMessages[counter];
                counter++;
            }
        }

        private void Form1_FormClosed(object sender,
                                      FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
