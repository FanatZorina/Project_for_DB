using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Xml.Linq;
using static System.Windows.Forms.DataFormats;
using Project_for_DB.Forms;

namespace Project_for_DB
{
    public partial class FAudit : Form
    {
        public FAudit()
        {
            InitializeComponent();
            using (DoorContext db = new DoorContext())
            {
                var q = from audit in db.Audits.AsNoTracking()
                        join user in db.Users.AsNoTracking()
                        on audit.Login equals user.Login
                        join departament in db.Departaments.AsNoTracking()
                        on user.DepartamentId equals departament.Id
                        join locks in db.Locks.AsNoTracking()
                        on new { IdDoor = audit.IdDoor, IdStreet = audit.IdStreet } equals new { IdDoor = locks.Id, IdStreet = locks.IdStreet }
                        join adress in db.Adresses.AsNoTracking()
                        on locks.IdStreet equals adress.Id
                        select new FromBigAuditClass()
                        {
                            number = audit.Number,
                            login = audit.Login,
                            name = user.Name,
                            sname = user.Sname,
                            departament = departament.Name,
                            date = audit.Date,
                            iddoor = locks.Id,
                            closed = audit.Closed,
                            street = adress.Street,
                            numberst = adress.Number,
                            building = adress.Building
                        };
                var auditList = q.ToList();
                foreach (var adress in auditList)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.numberst} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.numberst} корпус.{adress.building}";
                }
                dataGridView1.DataSource = auditList;
                dataGridView1.Columns[0].HeaderText = "Номер записи";
                dataGridView1.Columns[1].HeaderText = "Логин";
                dataGridView1.Columns[2].HeaderText = "Имя";
                dataGridView1.Columns[3].HeaderText = "Фамилия";
                dataGridView1.Columns[4].HeaderText = "Отдел";
                dataGridView1.Columns[5].HeaderText = "Дата";
                dataGridView1.Columns[6].HeaderText = "Номер двери";
                dataGridView1.Columns[7].HeaderText = "Адрес";
                dataGridView1.Columns[8].HeaderText = "Закрыто";
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
            }
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Получите данные из выделенной строки
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string number = selectedRow.Cells[0].Value.ToString();


                // Создайте новую форму и передайте данные
                FUpBigAudit detailForm = new FUpBigAudit(number);
                detailForm.dg = this.dataGridView1;
                detailForm.Show();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)Tag;
            form1.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
