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
            using (DoorContext db = new DoorContext())
            {
                var q = from audit in db.Audits.AsNoTracking()
                        select new FromAuditClass()
                        {
                            number = audit.Number,
                            date = audit.Date,
                            iddoor = audit.IdDoor,
                            idstreet = audit.IdStreet,
                            login = audit.Login,
                            closed = audit.Closed
                        };
                var auditList = q.ToList();
                dataGridView1.DataSource = auditList;
                dataGridView1.Columns[0].HeaderText = "Номер записи";
                dataGridView1.Columns[1].HeaderText = "Дата";
                dataGridView1.Columns[2].HeaderText = "Дверь";
                dataGridView1.Columns[3].HeaderText = "Адрес";
                dataGridView1.Columns[4].HeaderText = "Логин";
                dataGridView1.Columns[5].HeaderText = "Закрыто";
                dataGridView1.ReadOnly = true;
            }
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
    }
}
