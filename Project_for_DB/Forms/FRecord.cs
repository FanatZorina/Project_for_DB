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

namespace Project_for_DB.Forms
{
    public partial class FRecord : Form
    {
        public FRecord()
        {
            InitializeComponent();

            var auditList = Metods.ViewRecord().ToList();
            dataGridView1.DataSource = auditList;
            dataGridView1.Columns[0].HeaderText = "Номер записи";
            dataGridView1.Columns[1].HeaderText = "Дата";
            dataGridView1.Columns[2].HeaderText = "Дверь";
            dataGridView1.Columns[3].HeaderText = "Адрес";
            dataGridView1.Columns[4].HeaderText = "Логин";
            dataGridView1.Columns[5].HeaderText = "Закрыто";
            dataGridView1.ReadOnly = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fdate = (FDate)Tag;
            fdate.Show();
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FAddRecord fadrecord = new FAddRecord();
            fadrecord.Tag = this;
            fadrecord.dg = this.dataGridView1;
            fadrecord.Show(this);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FUpRecord fuprecord = new FUpRecord();
            fuprecord.Tag = this;
            fuprecord.dg = this.dataGridView1;
            fuprecord.Show(this);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FDelRecord fdelrecord = new FDelRecord();
            fdelrecord.Tag = this;
            fdelrecord.dg = this.dataGridView1;
            fdelrecord.Show(this);
        }
    }
}
