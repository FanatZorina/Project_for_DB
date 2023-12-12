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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Dynamic.Core;
using Microsoft.VisualBasic.Logging;
using System.Net;
using System.Security.Claims;

namespace Project_for_DB
{
    public partial class FAudit : Form
    {
        public FAudit()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress); 
            
            var auditList = Metods.ViewBigAudit().ToList();
            foreach (var adress in auditList)
            {
                if (adress.building == null)
                    adress.fulladress = $" ул.{adress.street} {adress.numberst} ";
                else
                    adress.fulladress = $" ул.{adress.street} {adress.numberst} корпус.{adress.building}";
            }
            dataGridView1.DataSource = auditList;
            dataGridView1.Columns["number"].HeaderText = "Номер записи";
            dataGridView1.Columns["login"].HeaderText = "Логин";
            dataGridView1.Columns["name"].HeaderText = "Имя";
            dataGridView1.Columns["sname"].HeaderText = "Фамилия";
            dataGridView1.Columns["departament"].HeaderText = "Отдел";
            dataGridView1.Columns["date"].HeaderText = "Дата";
            dataGridView1.Columns["iddoor"].HeaderText = "Номер двери";
            dataGridView1.Columns["fulladress"].HeaderText = "Адрес";
            dataGridView1.Columns["closed"].HeaderText = "Открывали?";
            dataGridView1.Columns["numberst"].Visible = false;
            dataGridView1.Columns["street"].Visible = false;
            dataGridView1.Columns["building"].Visible = false;
            dataGridView1.Columns["idadress"].Visible = false;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
                                  
            var auditList1 = Metods.ViewUser().ToList(); 
            auditList1.Insert(0, new FromUserClass { login = "-" });
            comboBox1.DataSource = auditList1;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.DisplayMember = "login";
            comboBox1.ValueMember = "login";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            var auditList2 = Metods.ViewAdress().ToList();
            foreach (var adress in auditList2)
            {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.number} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.number} корпус.{adress.building}";
            }
            auditList2.Insert(0, new FromAdressClass { fulladress = "-" });
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DataSource = auditList2;
            comboBox2.DisplayMember = "fulladress";
            comboBox2.ValueMember = "Id";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            List<string> OoC = new List<string> { "-", "Открывали", "Закрывали" };
            comboBox3.DataSource = OoC;
            comboBox3.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (DoorContext db = new DoorContext())
            {
                FromAdressClass selectedlock = (FromAdressClass)comboBox2.SelectedItem;
                FromUserClass selectedlogin = (FromUserClass)comboBox1.SelectedItem;
                var q = Metods.ViewBigAudit();
                string condition = "";
                if (comboBox1.Text != "-")
                {
                    q = q.Where(audit => audit.login == comboBox1.Text);
                }
                if (comboBox2.Text != "-")
                {
                    q = q.Where(audit => audit.idadress == selectedlock.Id);
                }
                if (textBox1.Text != "")
                {
                    q = q.Where(audit => audit.iddoor == Convert.ToInt32(textBox1.Text));
                }
                if (dateTimePicker1.Value != DateTimePicker.MinimumDateTime)
                {
                    q = q.Where(audit => audit.date.Date <= dateTimePicker1.Value.Date);
                }
                if (comboBox3.Text != "-")
                {
                    bool b = (comboBox3.Text == "Открывали");
                    q = q.Where(audit => audit.closed == b);
                }
                var auditList = q.ToList();
                foreach (var adress in auditList)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.numberst} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.numberst} корпус.{adress.building}";
                }


                dataGridView1.DataSource = auditList;

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void FAudit_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
