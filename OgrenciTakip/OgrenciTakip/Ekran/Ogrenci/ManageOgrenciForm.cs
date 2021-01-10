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

namespace OgrenciTakip.Ekran.Ogrenci
{
    public partial class ManageOgrenciForm : TemplateForm
    {
        public ManageOgrenciForm()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void öğrenciEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOgrenciInfoScreen(0, false);
        }

        private void ShowOgrenciInfoScreen(int ogrenciId, bool isUpdate)
        {
            OgrenciInfoForm oif = new OgrenciInfoForm();
            oif.OgrenciNo = ogrenciId;
            oif.IsUpdate = isUpdate;
            oif.ShowDialog();

            LoadDataIntoDataGridView();
        }

        private void ManageOgrenciForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            ListData.LoadDataIntoDataGridView(OgrencilerDataGridView, "usp_OgrencileriAlOgr");
        }

        private void OgrencilerDataGridView_DoubleClick(object sender, EventArgs e)
        {
            int rowIndex = OgrencilerDataGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int ogrenciId = Convert.ToInt32(OgrencilerDataGridView.Rows[rowIndex].Cells["OgrenciNo"].Value);
            ShowOgrenciInfoScreen(ogrenciId, true);
        }
    }
}
