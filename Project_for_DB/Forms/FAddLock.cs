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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Project_for_DB.Forms
{
    public partial class FAddLock : Form
    {
        public DataGridView dg;
        public FAddLock()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
              
                var auditList = Metods.ViewAdress().ToList(); ;

                foreach (var adress in auditList)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.number} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.number} корпус.{adress.building}";
                }
                comboBox1.DataSource = auditList;
                comboBox1.DisplayMember = "fulladress";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            

                List<int> level = new List<int> { 1, 2, 3, 4, 5 };
                comboBox2.DataSource = level;


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != ""))
            {
                using (DoorContext enty = new DoorContext())
                {
                    FromAdressClass selectedlock = (FromAdressClass)comboBox1.SelectedItem;
                    int intlock = (int)comboBox2.SelectedItem;
                    Lock locks = new Lock()
                    {
                        Id = Convert.ToInt32(textBox1.Text),
                        IdStreet = selectedlock.Id,
                        Level = intlock,
                        Closed = checkBox1.Checked
                    };
                    enty.Locks.Add(locks);
                    enty.SaveChanges();
                }
                using (DoorContext db = new DoorContext())
                {                
                    var auditList = Metods.ViewLock().ToList();
                    dg.DataSource = auditList;
                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FAddLock_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
