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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_for_DB.Forms
{

    public partial class FDelDept : Form
    {
        public DataGridView dg;
        public FDelDept()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            using (DoorContext db = new DoorContext())
            {
                var q = from departament in db.Departaments.AsNoTracking()
                        select new FromDeptClass()
                        {
                            Id = departament.Id,
                            name = departament.Name,
                            number = departament.Number,

                        };
                var auditList = q.ToList();

                comboBox1.DataSource = auditList;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
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

        private void button2_Click(object sender, EventArgs e)
        {
            using (DoorContext enty = new DoorContext())
            {
                List<string> users = enty.Users.AsNoTracking().Where(x => x.DepartamentId == Convert.ToInt32(textBox1.Text)).Select(x => x.Login).ToList();
                foreach (string user in users)
                {
                    List<int> ids = enty.Audits.AsNoTracking().Where(x => x.Login == user).Select(x => x.Number).ToList();
                    foreach (int id in ids)
                    {
                        enty.Audits.Remove(enty.Audits.AsNoTracking().Where(x => x.Number == id).FirstOrDefault());
                    }
                    enty.Users.Remove(enty.Users.AsNoTracking().Where(x => x.Login == user).FirstOrDefault());
                }
                enty.Departaments.Remove(enty.Departaments.AsNoTracking().Where(x => x.Id == Convert.ToInt32(textBox1.Text)).FirstOrDefault());
                enty.SaveChanges();

                var q = from departament in enty.Departaments.AsNoTracking()
                        select new FromDeptClass()
                        {
                            Id = departament.Id,
                            name = departament.Name,
                            number = departament.Number,

                        };
                var auditList = q.ToList();
                dg.DataSource = auditList;
            }
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FromDeptClass selecteddept = (FromDeptClass)comboBox1.SelectedItem;

                textBox1.Text = Convert.ToString(selecteddept.Id);
                textBox2.Text = selecteddept.name;
                textBox3.Text = Convert.ToString(selecteddept.number);
            }
        }
    }
}
