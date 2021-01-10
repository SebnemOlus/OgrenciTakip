using JIDBFramework;
using OgrenciTakip.Ekran.Tema;
using OgrenciTakip.Models.Kullanicilar;
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
using JIDBFramework.Windows;

namespace OgrenciTakip.Ekran
{
    public partial class LoginForm : TemplateForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());

                bool isLoginDetailsCorrect = Convert.ToBoolean(db.GetScalarValue("usp_KullaniciCheckLoginDetails", GetParameters()));

                if (isLoginDetailsCorrect)
                {
                    GetLoggedInUserSettings();

                    this.Hide();

                    Dashboard df = new Dashboard();
                    df.Show();
                }
                else
                {
                    JIMessageBox.ShowErrorMessage("Kullanıcı Adı/Şifre Doğru Girmeniz Gerekli");

                }
            }

        }

        private void GetLoggedInUserSettings()
        {
            LoggedInUser.KullaniciAdi = UserNameTextBox.Text.Trim();
        }

        private DbParameter[] GetParameters()
        {
            List<DbParameter> parameters = new List<DbParameter>();

            DbParameter dbparam1 = new DbParameter();
            dbparam1.Parameter = "@KullaniciAdi";
            dbparam1.Value = UserNameTextBox.Text;
            parameters.Add(dbparam1);

            DbParameter dbparam2 = new DbParameter();
            dbparam2.Parameter = "@Sifre";
            dbparam2.Value = PasswordTextBox.Text;
            parameters.Add(dbparam2);

            return parameters.ToArray();
        }

        private bool IsFormValid()
        {
            if (UserNameTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Kullanıcı Adı Girmeniz Gerekli");
                UserNameTextBox.Clear();
                UserNameTextBox.Focus();
                return false;
            }
            if (PasswordTextBox.Text.Trim() == string.Empty)
            {
                JIMessageBox.ShowErrorMessage("Şifre Girmeniz Gerekli");
                PasswordTextBox.Clear();
                PasswordTextBox.Focus();
                return false;
            }
            return true;
        }
    }
}
