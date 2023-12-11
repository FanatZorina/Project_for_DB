using Microsoft.VisualBasic.ApplicationServices;
using Project_for_DB.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_for_DB
{
    public partial class FDate : Form
    {
        public FDate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)Tag;
            form1.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FAdress faddadress = new FAdress();
            faddadress.Tag = this;
            faddadress.Show(this);
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FLock flock = new FLock();
            flock.Tag = this;
            flock.Show(this);
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FDept fdept = new FDept();
            fdept.Tag = this;
            fdept.Show(this);
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FUser fuser = new FUser();
            fuser.Tag = this;
            fuser.Show(this);
            Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FRecord frecord = new FRecord();
            frecord.Tag = this;
            frecord.Show(this);
            Hide();
        }
    }
}
