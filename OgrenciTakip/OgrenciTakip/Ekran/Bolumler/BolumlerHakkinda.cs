using JIDBFramework;
using JIDBFramework.Windows;
using OgrenciTakip.Ekran.Tema;
using OgrenciTakip.Models.Bolumler;
using OgrenciTakip.Models.Kullanicilar;
using OgrenciTakip.Utilities;
using OgrenciTakip.Utilities.Liste;
using OgrenciTakip.Yonetici;
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
    public partial class BolumlerHakkinda : TemplateForm
    {
        public BolumlerHakkinda()
        {
            InitializeComponent();
        }
        public int BranchId { get; set; }

        private void BranchNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TopPanelLabel.Text = BranchNameTextBox.Text;
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogoPictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Logo Seçiniz";
            ofd.Filter = "Logo File (*.png; *.jpg; *.bmf;*.gif) | *.png; *.jpg; *.bmf;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
               
                LogoPictureBox.Image = new Bitmap(ofd.FileName);
            }

        }
        

        private void BolumlerHakkinda_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxes();
            LoadDataAndBindToControlIfUpdate();
        }

        //Sehir ve ilcelerin Sql baglantisini burada gerceklestirdim.
        private void LoadDataIntoComboBoxes()
        {
            ListData.LoadDataIntoComboBox(CityNameComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.City });
            ListData.LoadDataIntoComboBox(DistrictComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.District });
        }

        private void LoadDataAndBindToControlIfUpdate()
        {
          if (this.IsUpdate)
            {
                DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
                
                DataTable dtBranch = db.GetDataList("usp_BranchesGetBranchDetailByBranchId", new DbParameter { Parameter= "@BolumId", Value= this.BranchId});
                DataRow row = dtBranch.Rows[0];

                BranchNameTextBox.Text = row["BolumAdi"].ToString();
                EmailAddressTextBox.Text = row["Email"].ToString();
                TelephoneTextBox.Text = row["Telefon"].ToString();
                WebAddressTextBox.Text = row["Website"].ToString();
                LogoPictureBox.Image = row["BolumResmi"] is DBNull ? null :
                    ImageManipulations.PutPhoto((byte[])row["BolumResmi"]);
                AddressLineTextBox.Text = row["AdressLine"].ToString();
                CityNameComboBox.SelectedValue = row["CityId"];
                DistrictComboBox.SelectedValue = row["DistrictId"];
                PostCodeTextBox.Text = row["PostCode"].ToString();
            }
        }

        private void KaydiKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsFormValidated())
            {
                if (this.IsUpdate)
                {
                    SaveorUpdateRecord("usp_BranchesUpdateBranchDetails");
                    JIMessageBox.ShowSuccessMessage("Kayıt başarıyla güncellenmiştir.");

                }
                else
                {
                    SaveorUpdateRecord("usp_BranchesAddNewBranch");
                    JIMessageBox.ShowSuccessMessage("Kayıt başarıyla eklenmiştir.");

                }

                this.Close();
            }
        }



        private void SaveorUpdateRecord(string storedProceName)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            db.SaveOrUpdateRecord(storedProceName, GetObject());
        }
       

        private Branch GetObject()
        {
            Branch branch = new Branch();
            branch.BolumId = (this.IsUpdate) ? this.BranchId : 0;
            branch.BolumAdi = BranchNameTextBox.Text;
            branch.Email = EmailAddressTextBox.Text;
            branch.Telefon = TelephoneTextBox.Text;
            branch.Website = WebAddressTextBox.Text;
            branch.AdressLine = AddressLineTextBox.Text;
            branch.CityId = Convert.ToInt32(CityNameComboBox.SelectedValue);
            branch.DistrictId = Convert.ToInt32(DistrictComboBox.SelectedValue);
            branch.PostCode = PostCodeTextBox.Text;
            branch.BolumResmi = (LogoPictureBox == null) ? null : ImageManipulations.GetPhoto(LogoPictureBox);
            branch.CreatedBy = LoggedInUser.KullaniciAdi;

            return branch;
        }



        private bool IsFormValidated()
        {
            if (BranchNameTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Bölüm ismi gereklidir.");
                BranchNameTextBox.Focus();
                return false;
            }
            if (EmailAddressTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("E-posta adresiniz gereklidir.");
                EmailAddressTextBox.Focus();
                return false;
            }
            if (TelephoneTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Telefon numaranız gereklidir.");
                TelephoneTextBox.Focus();
                return false;
            }
            return true;
        }



        

        
    }
}
