using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Security
{
    public class ORGStaffHelper
    {
        private string _tableName;

        private DataProviderType _dataProviderType = DataProviderType.Access;

        private string _connectionString;

        public ORGStaffHelper()
        {
            string item = ConfigurationManager.AppSettings["SYSPRIVDB"];
            string[] strArrays = new string[] { "||" };
            string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
            if (!(strArrays1[0].ToLower() != "sqlserver" && strArrays1[0].ToLower() != "sql"))
            {
                this._dataProviderType = DataProviderType.Sql;
            }
            else if (strArrays1[0].ToLower() == "oracle")
            {
                this._dataProviderType = DataProviderType.Oracle;
            }
            else if (strArrays1[0].ToLower() == "oledb")
            {
                this._dataProviderType = DataProviderType.OleDb;
            }
            else if (strArrays1[0].ToLower() == "odbc")
            {
                this._dataProviderType = DataProviderType.Odbc;
            }
            else if (strArrays1[0].ToLower() == "access")
            {
                this._dataProviderType = DataProviderType.Access;
            }
            this._connectionString = strArrays1[1];
            this._tableName = "ORGSTAFF";
        }

        public ORGStaffHelper(string tableName)
        {
            string item = ConfigurationManager.AppSettings["SYSPRIVDB"];
            string[] strArrays = new string[] { "||" };
            string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
            if (!(strArrays1[0].ToLower() != "sqlserver" && strArrays1[0].ToLower() != "sql"))
            {
                this._dataProviderType = DataProviderType.Sql;
            }
            else if (strArrays1[0].ToLower() == "oracle")
            {
                this._dataProviderType = DataProviderType.Oracle;
            }
            else if (strArrays1[0].ToLower() == "oledb")
            {
                this._dataProviderType = DataProviderType.OleDb;
            }
            else if (strArrays1[0].ToLower() == "odbc")
            {
                this._dataProviderType = DataProviderType.Odbc;
            }
            else if (strArrays1[0].ToLower() == "access")
            {
                this._dataProviderType = DataProviderType.Access;
            }
            this._connectionString = strArrays1[1];
            this._tableName = tableName;
        }

        public static bool IsValide()
        {
            return ConfigurationManager.AppSettings["SYSPRIVDB"] != null;
        }

        public void Add(Staff staff)
        {
            object[] string0 = new object[] { this._tableName, staff.StaffID, staff.RealName, Regedit.EncryptTextNet(staff.Password) };
            string str = string.Format("insert into {0}([ID],[NAME],[PASSWORD]) values('{1}','{2}','{3}')", string0);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }

        public void Delete(string string2)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, string.Format("delete from {0} where ID='{1}'", this._tableName, string2));
            ORGStaffRoleHelper.DeleteByStaffID(dataAccessLayer, string2);
            SysGrants.DeleteByStaffID(dataAccessLayer, string2);
            dataAccessLayer.Close();
        }

        public bool HasExist(string string2)
        {
            bool count = false;
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            string[] string0 = new string[] { "select * from ", this._tableName, " where id='", string2, "'" };
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
            count = dataTable.Rows.Count > 0;
            dataAccessLayer.Close();
            return count;
        }

        public List<Staff> Load()
        {
            List<Staff> staffs = new List<Staff>();
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from ", this._tableName));
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow item = dataTable.Rows[i];
                Staff staff = new Staff()
                {
                    StaffID = Convert.ToString(item["ID"]),
                    LoginName = Convert.ToString(item["ID"]),
                    RealName = Convert.ToString(item["NAME"]),
                    Password = Regedit.DecryptTextNet(Convert.ToString(item["PASSWORD"]))
                };
                staffs.Add(staff);
            }
            dataAccessLayer.Close();
            return staffs;
        }

        public Staff Load(string string2)
        {
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            string[] string0 = new string[] { "select * from ", this._tableName, " where ID='", string2, "'" };
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
            Staff staff = null;
            if (dataTable.Rows.Count > 0)
            {
                DataRow item = dataTable.Rows[0];
                staff = new Staff()
                {
                    StaffID = Convert.ToString(item["ID"]),
                    LoginName = Convert.ToString(item["ID"]),
                    RealName = Convert.ToString(item["NAME"]),
                    Password = Regedit.DecryptTextNet(Convert.ToString(item["PASSWORD"]))
                };
            }
            dataAccessLayer.Close();
            return staff;
        }

        public void Update(Staff staff)
        {
            object[] string0 = new object[] { this._tableName, staff.RealName, Regedit.EncryptTextNet(staff.Password), staff.StaffID };
            string str = string.Format("UPDATE  {0} set [NAME]='{1}',[PASSWORD]='{2}' where ID='{3}'", string0);
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
            dataAccessLayer.Close();
        }

        public bool ValidePassword(string string2, string string3)
        {
            bool flag;
            DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this._dataProviderType, this._connectionString);
            dataAccessLayer.Open();
            string str = Regedit.EncryptTextNet(string3);
            string[] string0 = new string[] { "select * from ", this._tableName, " where ID='", string2, "' and PASSWORD = '", str, "'" };
            DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
            dataAccessLayer.Close();
            flag = (dataTable.Rows.Count != 0 ? true : false);
            return flag;
        }

    }
}
