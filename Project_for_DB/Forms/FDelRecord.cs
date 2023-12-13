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
    public partial class FDelRecord : Form
    {
        public DataGridView dg;
        public FDelRecord()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            using (DoorContext db = new DoorContext())
            {
                var auditList1 = Metods.ViewRecord().ToList();
                comboBox1.DataSource = auditList1;
                comboBox1.DisplayMember = "number";
                comboBox1.ValueMember = "number";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {            
                db.Audits.Remove(db.Audits.AsNoTracking().Where(x => x.Number == Convert.ToInt32(comboBox1.Text)).FirstOrDefault());
                db.SaveChanges();

                var auditList = Metods.ViewRecord().ToList();
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
            if (comboBox1.SelectedValue != null)
            {
                FromAuditClass selectedrecord = (FromAuditClass)comboBox1.SelectedItem;
                textBox2.Text = Convert.ToString(selectedrecord.login);
                textBox3.Text = Convert.ToString(selectedrecord.idstreet);
                textBox4.Text = Convert.ToString(selectedrecord.iddoor);
                textBox5.Text = Convert.ToString(selectedrecord.date);
                checkBox1.Checked = selectedrecord.closed;
            }
        }
    }
}
