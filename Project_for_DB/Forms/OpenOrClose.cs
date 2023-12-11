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
    public partial class OpenOrClose : Form
    {
        string glogin;
        int giddoor;
        int gidstreet;
        public OpenOrClose(string login, int iddoor, int idstreet)
        {
            giddoor = iddoor;
            gidstreet = idstreet;
            glogin = login;
            InitializeComponent();
            using (DoorContext db = new DoorContext())
            {

                bool cl = db.Locks.AsNoTracking().Where(x => x.Id == iddoor && x.IdStreet == idstreet).Select(x => x.Closed).FirstOrDefault();
                var result = db.Adresses.Where(x => x.Id == idstreet).Select(a => $" ул.{a.Street} дом.{a.Number} {a.Building}").FirstOrDefault();
                if (cl == false)
                {
                    button1.Visible = true;
                    label1.Text = " Аудитория " + Convert.ToString(iddoor) + " по адресу " + result + " закрыта ";

                }
                else
                {
                    button2.Visible = true;
                    label1.Text = " Аудитория " + Convert.ToString(iddoor) + " по адресу " + result + " открыта ";
                };

            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (DoorContext enty = new DoorContext())
            {

                Audit adudit = new Audit()
                {
                    Date = DateTime.Now,
                    IdDoor = giddoor,
                    IdStreet = gidstreet,
                    Login = glogin,
                    Closed = true
                };
                enty.Audits.Add(adudit);
                enty.SaveChanges();

            }
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
                using (DoorContext enty = new DoorContext())
                {
                    
                    Audit adudit = new Audit()
                    {
                        Date = DateTime.Now,
                        IdDoor = giddoor,
                        IdStreet = gidstreet,
                        Login = glogin,
                        Closed = false
                    };
                    enty.Audits.Add(adudit);
                    enty.SaveChanges();
                }         
                Close();           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
