using JIDBFramework;
using JIDBFramework.Windows;
using OgrenciTakip.Ekran.Tema;
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

namespace OgrenciTakip.Ekran.Ogrenci
{
    public partial class OgrenciInfoForm : TemplateForm
    {
        public OgrenciInfoForm()
        {
            InitializeComponent();
        }

        public int OgrenciNo { get; set; }

        private void KapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            AlPhoto();
        }

        private void AlPhoto()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Fotoğraf Seçiniz";
            ofd.Filter = "Photo File (*.png; *.jpg; *.bmf;*.gif) | *.png; *.jpg; *.bmf;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void OgrenciInfoForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxes();


          if (this.IsUpdate)
            {
                LoadDataAndBindIntoControls();
            }
            else
            {
                GenerateOgrenciId();
            }
        }

        private void LoadDataAndBindIntoControls()
        {

            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            DataTable dtEmployee = db.GetDataList("usp_OgrencilerDetayById", new DbParameter { Parameter = "@OgrenciNo", Value = this.OgrenciNo });
            DataRow row = dtEmployee.Rows[0];

            OgrenciIdTextBox.Text = row["OgrenciNo"].ToString();
            FullNameTextBox.Text = row["AdiveSoyadi"].ToString();
            DateOfDateTimePicker.Value = Convert.ToDateTime(row["DogumTarihi"]);
            TcNoTextBox.Text = row["TcNo"].ToString();
            EmailTextBox.Text = row["EmailAddress"].ToString();
            CepTextBox.Text = row["Cep"].ToString();
            TelephoneTextBox.Text = row["Telefon"].ToString();
            GenderComboBox.SelectedValue = row["CinsiyetId"];
            KayitTimePicker.Value = Convert.ToDateTime(row["KayitTarihi"]);
            BranchComboBox.SelectedValue = row["BolumId"];

            pictureBox1.Image = (row["Photo"] is DBNull) ? null : ImageManipulations.PutPhoto((byte[])row["Photo"]);

            AddressLineTextBox.Text = row["AddressLine"].ToString();
            CityComboBox.SelectedValue = row["CityId"];
            DistrictComboBox.SelectedValue = row["DistrictId"];
            PostCodeTextBox.Text = row["PostCode"].ToString();

        }

        private void LoadDataIntoComboBoxes()
        {
            ListData.LoadDataIntoComboBox(BranchComboBox, "usp_BranchesGetAllBranchNames");
            
            ListData.LoadDataIntoComboBox(CityComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.City });
            ListData.LoadDataIntoComboBox(DistrictComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.District });
            ListData.LoadDataIntoComboBox(GenderComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.Gender });


        }




        private void GenerateOgrenciId()
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            OgrenciIdTextBox.Text = db.GetScalarValue("usp_OgrenciID").ToString();
        }

        private void FullNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TextTopLabel.Text = FullNameTextBox.Text;
        }

        private void KaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(IsFormValid())
            {
                if(this.IsUpdate)
                {
                    SaveOrUpdateRecord("usp_OgrenciVerileriGuncelle");
                    JIMessageBox.ShowSuccessMessage("Kayıt güncellemesi başarılı.");
                }
                else
                {
                    SaveOrUpdateRecord("usp_OgrencilerEkle");
                    JIMessageBox.ShowSuccessMessage("Kayıt başarılı.");

                    this.IsUpdate = true;
                }
            }
        }

        private void SaveOrUpdateRecord(string storedProcName)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            db.SaveOrUpdateRecord(storedProcName, GetObject());
        }

        private Ogrenciler GetObject()
        {
            Ogrenciler ogrenci = new Ogrenciler();

          
            ogrenci.OgrenciNo = Convert.ToInt32(OgrenciIdTextBox.Text);
            ogrenci.AdiveSoyadi = FullNameTextBox.Text.Trim();
            ogrenci.DogumTarihi = DateOfDateTimePicker.Value.Date;
            ogrenci.TcNo = TcNoTextBox.Text.Trim();
            ogrenci.EmailAddress = EmailTextBox.Text.Trim();
            ogrenci.Cep = CepTextBox.Text.Trim();
            ogrenci.Telefon = TelephoneTextBox.Text.Trim();
            ogrenci.KayitTarihi = KayitTimePicker.Value.Date;

            ogrenci.CinsiyetId = (GenderComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(GenderComboBox.SelectedValue);
            ogrenci.BolumId = (BranchComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(BranchComboBox.SelectedValue);

            ogrenci.Photo = (pictureBox1.Image == null) ? null : ImageManipulations.GetPhoto(pictureBox1);

            ogrenci.AddressLine = AddressLineTextBox.Text.Trim();
            ogrenci.CityId = (CityComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(CityComboBox.SelectedValue);
            ogrenci.DistrictId = (DistrictComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(DistrictComboBox.SelectedValue);
            ogrenci.PostCode = PostCodeTextBox.Text.Trim();


            ogrenci.CreatedBy = LoggedInUser.KullaniciAdi;


            return ogrenci;
        }

        private bool IsFormValid()
        {
            if (FullNameTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen adınızı ve soyadınızı giriniz.");
                FullNameTextBox.Focus();
                return false;
            }

            if (TcNoTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen TC Kimlik numaranızı giriniz.");
                TcNoTextBox.Focus();
                return false;
            }

            if ((CepTextBox.Text.Trim() == string.Empty) && (TelephoneTextBox.Text.Trim() == string.Empty))
            {
                JIMessageBox.ShowErrorMessage("Lütfen telefon numaranızı ya da cep telefonunuzu giriniz.");
                CepTextBox.Focus();
                return false;
            }

            if (GenderComboBox.SelectedIndex == -1)
            {
                JIMessageBox.ShowErrorMessage("Lütfen cinsiyetinizi seçiniz.");
                return false;
            }

            if (AddressLineTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen adresinizi giriniz.");
                AddressLineTextBox.Focus();
                return false;
            }

            if (CityComboBox.SelectedIndex == -1)
            {
                JIMessageBox.ShowErrorMessage("Lütfen şehrinizi seçiniz.");
                return false;
            }

            if (DistrictComboBox.SelectedIndex == -1)
            {
                JIMessageBox.ShowErrorMessage("Lütfen ilçenizi seçiniz.");
                return false;
            }

            if (PostCodeTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen posta kodunuzu giriniz.");
                PostCodeTextBox.Focus();
                return false;
            }




            return true;

        }
    }
}
