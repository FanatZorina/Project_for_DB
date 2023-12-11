namespace Project_for_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddAudit addaudit = new AddAudit();
            addaudit.Tag = this;
            addaudit.Show(this);
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FDate fdate = new FDate();
            fdate.Tag = this;
            fdate.Show(this);
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FAudit faudit = new FAudit();
            faudit.Tag = this;
            faudit.Show(this);
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}