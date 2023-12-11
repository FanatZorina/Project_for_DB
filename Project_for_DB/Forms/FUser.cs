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
    public partial class FUser : Form
    {
        public FUser()
        {
            InitializeComponent();
            using (DoorContext db = new DoorContext())
            {
                var q = from user in db.Users.AsNoTracking()
                        join Departament in db.Departaments.AsNoTracking()
                        on user.DepartamentId equals Departament.Id
                        select new FromUserClass()
                        {
                            login = user.Login,
                            password = user.Password,
                            name = user.Name,
                            sname = user.Sname,
                            level = user.Level,
                            departament_name = Departament.Name
                        };
                var auditList = q.ToList();
                dataGridView1.DataSource = auditList;
                dataGridView1.Columns[0].HeaderText = "Логин";
                dataGridView1.Columns[1].HeaderText = "Пароль";
                dataGridView1.Columns[2].HeaderText = "Имя";
                dataGridView1.Columns[3].HeaderText = "Фамилия";
                dataGridView1.Columns[4].HeaderText = "Уровень доступа";
                dataGridView1.Columns[5].HeaderText = "Отдел";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.ReadOnly = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FAddUser fadduser = new FAddUser();
            fadduser.Tag = this;
            fadduser.dg = this.dataGridView1;
            fadduser.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FUpUser fupuser = new FUpUser();
            fupuser.Tag = this;
            fupuser.dg = this.dataGridView1;
            fupuser.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FDelUser fdeluser = new FDelUser();
            fdeluser.Tag = this;
            fdeluser.dg = this.dataGridView1;
            fdeluser.Show(this);
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
