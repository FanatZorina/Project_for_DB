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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Project_for_DB.Forms
{
    public partial class FUpUser : Form
    {
        public DataGridView dg;
        public class TC
        {
            public string departament_name { get; set; }
        }
        public FUpUser()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          
            var auditList = Metods.ViewUser().ToList();
            comboBox1.DataSource = auditList;
            comboBox1.DisplayMember = "login";
            comboBox1.ValueMember = "login";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            var auditList1 = Metods.ViewDept().ToList(); 
            comboBox3.DataSource = auditList1;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Id";
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;

            List<int> level = new List<int> { 1, 2, 3, 4, 5 };
            comboBox2.DataSource = level;
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FromUserClass selecteduser = (FromUserClass)comboBox1.SelectedItem;

                textBox1.Text = Convert.ToString(selecteduser.password);
                textBox2.Text = Convert.ToString(selecteduser.name);
                textBox3.Text = Convert.ToString(selecteduser.sname);
                comboBox2.Text = Convert.ToString(selecteduser.level);
                comboBox3.Text = Convert.ToString(selecteduser.departament_name);              
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != "") && (textBox2.Text.Trim() != "") && (textBox3.Text.Trim() != ""))
            {
                using (DoorContext db = new DoorContext())
                {

                    var temp1 = db.Users.FirstOrDefault(s => s.Login == comboBox1.Text);

                    if (temp1 != null)
                    {
                        var temp2 = db.Departaments.FirstOrDefault(s => s.Name == comboBox3.Text);
                        temp1.Password = textBox1.Text;
                        temp1.Name = textBox2.Text;
                        temp1.Sname = textBox3.Text;
                        temp1.Level = Convert.ToInt32(comboBox2.Text);
                        temp1.Departament = temp2;
                        db.Users.Update(temp1);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                  
                    var auditList = Metods.ViewUser().ToList();
                    dg.DataSource = auditList;

                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
