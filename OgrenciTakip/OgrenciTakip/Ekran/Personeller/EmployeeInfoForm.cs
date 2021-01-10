using JIDBFramework;
using JIDBFramework.Windows;
using OgrenciTakip.Ekran.Tema;
using OgrenciTakip.Models.Kullanicilar;
using OgrenciTakip.Models.Personeller;
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

namespace OgrenciTakip.Ekran.Personeller
{
    public partial class EmployeeInfoForm : TemplateForm
    {
        public EmployeeInfoForm()
        {
            InitializeComponent();
        }

        public int PersonelId { get; set; }

       

        private void KapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmployeeInfoForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxes();

            if (this.IsUpdate)
            {
                LoadDataAndBindIntoControls();
                EnableButtons();

            }
            else
            {
                GenerateEmployeeId();
            }
        }


        private void LoadDataAndBindIntoControls()
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            DataTable dtEmployee = db.GetDataList("usp_EmployeesGetEmployeeDetailsById", new DbParameter { Parameter = "@PersonelId", Value = this.PersonelId });
            DataRow row = dtEmployee.Rows[0];

            EmbloyeeIDTextBox.Text = row["PersonelId"].ToString();
            textBox2.Text = row["AdiveSoyadi"].ToString();
            DateOfBirthDateTimePicker.Value = Convert.ToDateTime(row["DogumTarihi"]);
            NICTextBox.Text = row["NICNumber"].ToString();
            EmailAddressTextBox.Text = row["EmailAddress"].ToString();
            MobileTextBox.Text = row["Cep"].ToString();
            TelephoneTextBox.Text = row["Telefon"].ToString();
            comboBox1.SelectedValue = row["CinsiyetId"];
            DateTimePicker2.Value = Convert.ToDateTime(row["IsTarihi"]);
            BolumComboBox.SelectedValue = row["BolumId"];

            PhotoPictureBox.Image = (row["Photo"] is DBNull) ? null : ImageManipulations.PutPhoto((byte[])row["Photo"]);

            AddressLineTextBox.Text = row["AddressLine"].ToString();
            CityComboBox.SelectedValue = row["CityId"];
            DistrictComboBox.SelectedValue = row["DistrictId"];
            PostCodeTextBox.Text = row["PostCode"].ToString();
            JobTitleComboBox.SelectedValue = row["JobTitleId"];
            MevcutMaasTextBox.Text = row["CurrentSalary"].ToString();
            IlkMaasTextBox.Text = row["StartingSalary"].ToString();

            HasLeftComboBox.Text = (Convert.ToBoolean(row["HasLeft"]) == true) ? "Evet" : "Hayır";
            DateLeftDateTimePicker.Value = Convert.ToDateTime(row["DateLeft"]);
            EmployeeLevingReasonComboBox.SelectedValue = row["ReasonLeftId"];
            EmployeeLeavingCommentsTextBox.Text = row["Comments"].ToString();
        }


        private void GenerateEmployeeId()
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            EmbloyeeIDTextBox.Text = db.GetScalarValue("usp_EmployeesGenerateNewEmployeeId").ToString();
        }


        private void LoadDataIntoComboBoxes()
        {
            ListData.LoadDataIntoComboBox(BolumComboBox, "usp_BranchesGetAllBranchNames");

            ListData.LoadDataIntoComboBox(comboBox1, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.Gender });
            ListData.LoadDataIntoComboBox(CityComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.City });
            ListData.LoadDataIntoComboBox(DistrictComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.District });
            ListData.LoadDataIntoComboBox(JobTitleComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.EmployeeJobTitle });
            ListData.LoadDataIntoComboBox(HasLeftComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.YesNo });
            ListData.LoadDataIntoComboBox(EmployeeLevingReasonComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.EmployeeReasonLeft });

        }

     

        private void PhotoPictureBox_DoubleClick(object sender, EventArgs e)
        {
            GetPhoto();
        }

        private void GetPhoto()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Fotoğraf Seçiniz";
            ofd.Filter = "Photo File (*.png; *.jpg; *.bmf;*.gif) | *.png; *.jpg; *.bmf;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                PhotoPictureBox.Image = new Bitmap(ofd.FileName);
            }
        }

        private void GetPhotoPictureBox_Click(object sender, EventArgs e)
        {
            GetPhoto();
        }

        private void ClearPictureBox_Click(object sender, EventArgs e)
        {
            PhotoPictureBox.Image = null;
        }

        private void KaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                if(this.IsUpdate)
                {
                    SaveOrUpdate("usp_EmployeesUpdateEmployeeDetails");
                    JIMessageBox.ShowSuccessMessage("Kayıt güncellemesi başarılı.");
                }
                else
                {
                    SaveOrUpdate("usp_EmployeesAddNewEmployee");
                    JIMessageBox.ShowSuccessMessage("Kayıt başarılı.");

                    this.IsUpdate = true;
                    EnableButtons();
                }
            }
        }

        private void SaveOrUpdate(string storedProcName)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            db.SaveOrUpdateRecord(storedProcName, GetObject());

        }

        private void EnableButtons()
        {
            EkleToolStripMenuItem.Enabled = true;
            YollaToolStripMenuItem.Enabled = true;
            YazdirToolStripMenuItem.Enabled = true;
        }

        

        private Employee GetObject()
        {
            Employee employee = new Employee();
            employee.PersonelId = Convert.ToInt32(EmbloyeeIDTextBox.Text);
            employee.AdiveSoyadi = textBox2.Text.Trim();
            employee.DogumTarihi = DateOfBirthDateTimePicker.Value.Date;
            employee.NICNumber = NICTextBox.Text.Trim();
            employee.EmailAddress = EmailAddressTextBox.Text.Trim();
            employee.Cep = MobileTextBox.Text.Trim();
            employee.Telefon = TelephoneTextBox.Text.Trim();
            employee.IsTarihi = DateTimePicker2.Value.Date;


            employee.CinsiyetId = (comboBox1.SelectedIndex == -1) ? 0 : Convert.ToInt32(comboBox1.SelectedValue);
            employee.BolumId = (BolumComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(BolumComboBox.SelectedValue);

            employee.Photo = (PhotoPictureBox.Image == null) ? null : ImageManipulations.GetPhoto(PhotoPictureBox);


            employee.AddressLine = AddressLineTextBox.Text.Trim();
            employee.CityId = (CityComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(CityComboBox.SelectedValue);
            employee.DistrictId = (DistrictComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(DistrictComboBox.SelectedValue);
            employee.PostCode = PostCodeTextBox.Text.Trim();

            employee.JobTitleId = (JobTitleComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(JobTitleComboBox.SelectedValue);
            employee.CurrentSalary = Convert.ToDecimal(MevcutMaasTextBox.Text);
            employee.StartingSalary = Convert.ToDecimal(IlkMaasTextBox.Text);

            employee.HasLeft = (HasLeftComboBox.Text == "Evet") ? true : false;
            employee.DateLeft = DateLeftDateTimePicker.Value.Date;
            employee.ReasonLeftId = (EmployeeLevingReasonComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(EmployeeLevingReasonComboBox.SelectedValue);
            employee.Comments = EmployeeLeavingCommentsTextBox.Text.Trim();

            employee.CreatedBy = LoggedInUser.KullaniciAdi;




            return employee;
        }

        private bool IsFormValid()
        {
            if (textBox2.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen adınızı ve soyadınızı giriniz.");
                textBox2.Focus();
                return false;
            }

            if (NICTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen TC Kimlik numaranızı giriniz.");
                NICTextBox.Focus();
                return false;
            }

            if ((MobileTextBox.Text.Trim() == string.Empty) && (TelephoneTextBox.Text.Trim() == string.Empty))
            {
                JIMessageBox.ShowErrorMessage("Lütfen telefon numaranızı ya da cep telefonunuzu giriniz.");
                MobileTextBox.Focus();
                return false;
            }

            if(comboBox1.SelectedIndex == -1)
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


            if (JobTitleComboBox.SelectedIndex == -1)
            {
                JIMessageBox.ShowErrorMessage("Lütfen işinizi seçiniz.");
                return false;
            }

            if (MevcutMaasTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen mevcut maaşınızı giriniz.");
                MevcutMaasTextBox.Focus();
                return false;
            }
            else
            { 
                if(Convert.ToDecimal(MevcutMaasTextBox.Text.Trim()) < 1)
                {
                    JIMessageBox.ShowErrorMessage("Mecvut maaşınız 0 ya da 0'dan küçük olamaz.");
                    MevcutMaasTextBox.Focus();
                    return false;
                }
            }

            if (IlkMaasTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Lütfen başlangıç maaşınızı giriniz.");
                IlkMaasTextBox.Focus();
                return false;
            }
            else
            {
                if (Convert.ToDecimal(IlkMaasTextBox.Text.Trim()) < 1)
                {
                    JIMessageBox.ShowErrorMessage("İlk maaşınız 0 ya da 0'dan küçük olamaz.");
                    IlkMaasTextBox.Focus();
                    return false;
                }
            }

            return true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           label8.Text = textBox2.Text;
        }

       
    }
}
