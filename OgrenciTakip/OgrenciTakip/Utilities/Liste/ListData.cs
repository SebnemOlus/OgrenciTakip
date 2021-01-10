using JIDBFramework;
using OgrenciTakip.Yonetici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciTakip.Utilities.Liste
{
    public class ListData
    {
        public static void LoadDataIntoDataGridView(DataGridView dgv, string storedProceName)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());

            dgv.DataSource = db.GetDataList(storedProceName);

            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public static void LoadDataIntoComboBox(ComboBox cb, DbParameter parameter)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList("usp_ListTypesDataGetDataByListTypeId", parameter);

            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";

            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;

        }


        public static void LoadDataIntoComboBox(ComboBox cb, string storedProceName)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList(storedProceName);

            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";

            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;

        }



        public static void LoadDataIntoComboBox(ComboBox cb, string storedProceName, DbParameter parameter)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList(storedProceName, parameter);

            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";

            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public static void LoadDataIntoComboBox(ComboBox cb, string storedProceName, DbParameter[] parameters)
        {
            DbSQLServer db = new DbSQLServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList(storedProceName, parameters);

            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";

            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;

        }
    }
}
