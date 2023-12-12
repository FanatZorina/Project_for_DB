using Microsoft.EntityFrameworkCore;
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
    public partial class FAdress : Form
    {

        public FAdress()
        {

            InitializeComponent();

            using (DoorContext db = new DoorContext())
            {               
                var auditList = Metods.ViewAdress().ToList(); ;
                dataGridView1.DataSource = auditList;
                dataGridView1.Columns[0].HeaderText = "Индификатор";
                dataGridView1.Columns[1].HeaderText = "Улица";
                dataGridView1.Columns[2].HeaderText = "Адрес";
                dataGridView1.Columns[3].HeaderText = "Строение";
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[4].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fdate = (FDate)Tag;
            fdate.Show();
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FAddAdress faddddress = new FAddAdress();
            faddddress.Tag = this;
            faddddress.dg = this.dataGridView1;
            faddddress.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FDelAdress faddddress = new FDelAdress();
            faddddress.Tag = this;
            faddddress.dg = this.dataGridView1;
            faddddress.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FUpAdress fupadress = new FUpAdress();
            fupadress.Tag = this;
            fupadress.dg = this.dataGridView1;
            fupadress.Show(this);
        }
    }
}
