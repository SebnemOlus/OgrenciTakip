using OgrenciTakip.Ekran.Bolumler;
using OgrenciTakip.Ekran.Ogrenci;
using OgrenciTakip.Ekran.Personeller;
using OgrenciTakip.Ekran.Tema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciTakip.Ekran
{
    public partial class Dashboard : TemplateForm
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ManageBrachesForm mbf = new ManageBrachesForm();
            mbf.ShowDialog();
        }

        private void ManageEmployeesToolStripButton_Click(object sender, EventArgs e)
        {
            ManageEmployeesForm mef = new ManageEmployeesForm();
            mef.ShowDialog();
        }

        private void NewStudentToolStripButton_Click(object sender, EventArgs e)
        {
            ManageOgrenciForm mof = new ManageOgrenciForm();
            mof.ShowDialog();

        }
    }
}
