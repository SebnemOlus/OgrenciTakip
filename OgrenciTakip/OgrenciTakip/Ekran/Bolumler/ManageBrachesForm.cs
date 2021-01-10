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

namespace OgrenciTakip.Ekran.Bolumler
{
    public partial class ManageBrachesForm : TemplateForm
    {
        public ManageBrachesForm()
        {
            InitializeComponent();
        }

      

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewBranchhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBolumlerHakkindaScreen(0, false);
        }

        private void ShowBolumlerHakkindaScreen(int branchId, bool isUpdate)
        {
            BolumlerHakkinda bif = new BolumlerHakkinda();
            bif.BranchId = branchId;
            bif.IsUpdate = isUpdate;
            bif.ShowDialog();

            LoadDataIntoDataGridView();

        }

        private void ManageBrachesForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            ListData.LoadDataIntoDataGridView(BranchesDataGridView, "usp_BranchesGetAllBranches");
        }

        private void BranchesDataGridView_DoubleClick(object sender, EventArgs e)
        {
            int rowIndex = BranchesDataGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int branchId = Convert.ToInt32(BranchesDataGridView.Rows[rowIndex].Cells["BolumId"].Value);

            ShowBolumlerHakkindaScreen(branchId, true);
        }
    }
}
