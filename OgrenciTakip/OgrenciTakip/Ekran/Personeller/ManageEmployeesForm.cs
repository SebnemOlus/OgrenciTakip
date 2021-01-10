using OgrenciTakip.Ekran.Tema;
using OgrenciTakip.Utilities.Liste;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciTakip.Ekran.Personeller
{
    public partial class ManageEmployeesForm : TemplateForm
    {
        public ManageEmployeesForm()
        {
            InitializeComponent();
        }

        private void KapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void YeniPersonelEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEmplooyeInfoScreen(0, false);
        }

        private void ShowEmplooyeInfoScreen(int personelId, bool isUpdate)
        {
            EmployeeInfoForm eif = new EmployeeInfoForm();
            eif.PersonelId = personelId;
            eif.IsUpdate = isUpdate;
            eif.ShowDialog();

            LoadDataIntoDataGridView();

        }

        private void ManageEmployeesForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            ListData.LoadDataIntoDataGridView(EmployeesDataGridView, "usp_EmployeesGetEmployees");
        }

        private void EmployeesDataGridView_DoubleClick(object sender, EventArgs e)
        {
            int rowIndex = EmployeesDataGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int personelId =Convert.ToInt32(EmployeesDataGridView.Rows[rowIndex].Cells["PersonelId"].Value);
            ShowEmplooyeInfoScreen(personelId, true);
        }
    }
}
