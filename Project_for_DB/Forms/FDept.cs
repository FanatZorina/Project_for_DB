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
    public partial class FDept : Form
    {
        public FDept()
        {
            InitializeComponent();
            using (DoorContext db = new DoorContext())
            {
                var q = from Departament in db.Departaments.AsNoTracking()
                        select new FromDeptClass()
                        {
                            Id = Departament.Id,
                            name = Departament.Name,
                            number = Departament.Number
                        };
                var auditList = q.ToList();
                dataGridView1.DataSource = auditList;
                dataGridView1.Columns[0].HeaderText = "Номер";
                dataGridView1.Columns[1].HeaderText = "Название";
                dataGridView1.Columns[2].HeaderText = "Колл.людей";
                dataGridView1.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FAddDept faddept = new FAddDept();
            faddept.Tag = this;
            faddept.dg = this.dataGridView1;
            faddept.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            FUpDept fupdept = new FUpDept();
            fupdept.Tag = this;
            fupdept.dg = this.dataGridView1;
            fupdept.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FDelDept fdeldept = new FDelDept();
            fdeldept.Tag = this;
            fdeldept.dg = this.dataGridView1;
            fdeldept.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fdate = (FDate)Tag;
            fdate.Show();
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
