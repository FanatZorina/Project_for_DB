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
    public partial class FLock : Form
    {
        public FLock()
        {
            InitializeComponent();
            using (DoorContext db = new DoorContext())
            {
                var q = from Lock in db.Locks.AsNoTracking()
                        join Adress in db.Adresses.AsNoTracking()
                        on Lock.IdStreet equals Adress.Id
                        select new FromLockClass()
                        {
                            Id = Lock.Id,
                            Level = Lock.Level,
                            Closed = Lock.Closed,
                            Id_street = Lock.IdStreet,
                            fulladress = Convert.ToString("ул." + Adress.Street + " дом." + Adress.Number + " " + Adress.Building)
                        };
                var auditList = q.ToList();
                dataGridView1.DataSource = auditList;
                dataGridView1.Columns["Id"].HeaderText = "Номер";
                dataGridView1.Columns["fulladress"].HeaderText = "Улица";
                dataGridView1.Columns["Level"].HeaderText = "Уровень доступа";
                dataGridView1.Columns["Closed"].HeaderText = "Закрыто?";
                dataGridView1.Columns["Id_street"].Visible = false;
                dataGridView1.ReadOnly = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fdate = (FDate)Tag;
            fdate.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FAddLock faddlock = new FAddLock();
            faddlock.Tag = this;
            faddlock.dg = this.dataGridView1;
            faddlock.Show(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FDelLock fdellock = new FDelLock();
            fdellock.Tag = this;
            fdellock.dg = this.dataGridView1;
            fdellock.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FUpLock fuplock = new FUpLock();
            fuplock.Tag = this;
            fuplock.dg = this.dataGridView1;
            fuplock.Show(this);
        }
    }
}
