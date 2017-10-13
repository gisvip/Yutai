using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Yutai.Security
{
	public class ORGRoleHelper
	{
		private string string_0 = "";

		private DataProviderType dataProviderType_0 = DataProviderType.Sql;

		private string string_1 = "";

		public ORGRoleHelper()
		{
			string item = ConfigurationManager.AppSettings["SYSPRIVDB"];
			string[] strArrays = new string[] { "||" };
			string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			if (!(strArrays1[0].ToLower() == "sqlserver" ? false : !(strArrays1[0].ToLower() == "sql")))
			{
				this.dataProviderType_0 = DataProviderType.Sql;
			}
			else if (strArrays1[0].ToLower() == "oracle")
			{
				this.dataProviderType_0 = DataProviderType.Oracle;
			}
			else if (strArrays1[0].ToLower() == "oledb")
			{
				this.dataProviderType_0 = DataProviderType.OleDb;
			}
			else if (strArrays1[0].ToLower() == "odbc")
			{
				this.dataProviderType_0 = DataProviderType.Odbc;
			}
			else if (strArrays1[0].ToLower() == "access")
			{
				this.dataProviderType_0 = DataProviderType.Access;
			}
			this.string_1 = strArrays1[1];
			this.string_0 = "ORGROLES";
		}

		public ORGRoleHelper(string string_2)
		{
			string item = ConfigurationManager.AppSettings["SYSPRIVDB"];
			string[] strArrays = new string[] { "||" };
			string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			if (!(strArrays1[0].ToLower() == "sqlserver" ? false : !(strArrays1[0].ToLower() == "sql")))
			{
				this.dataProviderType_0 = DataProviderType.Sql;
			}
			else if (strArrays1[0].ToLower() == "oracle")
			{
				this.dataProviderType_0 = DataProviderType.Oracle;
			}
			else if (strArrays1[0].ToLower() == "oledb")
			{
				this.dataProviderType_0 = DataProviderType.OleDb;
			}
			else if (strArrays1[0].ToLower() == "odbc")
			{
				this.dataProviderType_0 = DataProviderType.Odbc;
			}
			else if (strArrays1[0].ToLower() == "access")
			{
				this.dataProviderType_0 = DataProviderType.Access;
			}
			this.string_1 = strArrays1[1];
			this.string_0 = string_2;
		}

		public void Add(ORGRole orgrole_0)
		{
			object[] string0 = new object[] { this.string_0, orgrole_0.RoleID, orgrole_0.RoleName, orgrole_0.DESCRIPT };
			string str = string.Format("insert into {0}([RoleID],[RoleName],[DESCRIPT],ROLETYPE) values('{1}','{2}','{3}','100')", string0);
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
			dataAccessLayer.Close();
		}

		public void Delete(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			dataAccessLayer.ExecuteNonQuery(CommandType.Text, string.Format("delete from {0} where ROLEID='{1}'", this.string_0, string_2));
			ORGStaffRoleHelper.DeleteByRoleID(dataAccessLayer, string_2);
			SysGrants.DeleteByRoleID(dataAccessLayer, string_2);
			dataAccessLayer.Close();
		}

		public ORGRole GetRole(string string_2)
		{
			ORGRole oRGRole = null;
			List<ORGRole> oRGRoles = new List<ORGRole>();
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			string[] string0 = new string[] { "select * from ", this.string_0, " where ROLEID='", string_2, "'" };
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
			if (dataTable.Rows.Count > 0)
			{
				DataRow item = dataTable.Rows[0];
				oRGRole = new ORGRole()
				{
					RoleID = Convert.ToString(item["ROLEID"]),
					RoleName = Convert.ToString(item["ROLENAME"]),
					DESCRIPT = Convert.ToString(item["DESCRIPT"])
				};
			}
			dataAccessLayer.Close();
			return oRGRole;
		}

		public bool HasExist(string string_2)
		{
			bool count = false;
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			string[] string0 = new string[] { "select * from ", this.string_0, " where RoleName='", string_2, "'" };
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
			count = dataTable.Rows.Count > 0;
			dataAccessLayer.Close();
			return count;
		}

		public List<ORGRole> Load()
		{
			List<ORGRole> oRGRoles = new List<ORGRole>();
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from ", this.string_0));
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow item = dataTable.Rows[i];
				ORGRole oRGRole = new ORGRole()
				{
					RoleID = Convert.ToString(item["ROLEID"]),
					RoleName = Convert.ToString(item["ROLENAME"]),
					DESCRIPT = Convert.ToString(item["DESCRIPT"])
				};
				oRGRoles.Add(oRGRole);
			}
			dataAccessLayer.Close();
			return oRGRoles;
		}
	}
}