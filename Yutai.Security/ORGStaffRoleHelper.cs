using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Yutai.Security
{
	public class ORGStaffRoleHelper
	{
		private string string_0 = "";

		private DataProviderType dataProviderType_0 = DataProviderType.Sql;

		private string string_1 = "";

		public ORGStaffRoleHelper()
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
			this.string_0 = "ORGSTAFFROLES";
		}

		public ORGStaffRoleHelper(string string_2)
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

		public void Add(string string_2, string string_3)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			string str = string.Format("insert into ORGSTAFFROLES(STAFFID,ROLEID) values('{0}', '{1}')", string_3, string_2);
			dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
			dataAccessLayer.Close();
		}

		public void DeleteByRoleID(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			dataAccessLayer.ExecuteNonQuery(CommandType.Text, string.Format("delete from {0} where ROLEID='{1}'", this.string_0, string_2));
			dataAccessLayer.Close();
		}

		public static void DeleteByRoleID(DataAccessLayerBaseClass dataAccessLayerBaseClass_0, string string_2)
		{
			dataAccessLayerBaseClass_0.ExecuteNonQuery(CommandType.Text, string.Format("delete from ORGSTAFFROLES where ROLEID='{0}'", string_2));
		}

		public static void DeleteByStaffID(DataAccessLayerBaseClass dataAccessLayerBaseClass_0, string string_2)
		{
			dataAccessLayerBaseClass_0.ExecuteNonQuery(CommandType.Text, string.Format("delete from ORGSTAFFROLES where STAFFID='{0}'", string_2));
		}

		public void DeleteByStaffID(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			dataAccessLayer.ExecuteNonQuery(CommandType.Text, string.Format("delete from {0} where STAFFID='{1}'", this.string_0, string_2));
			dataAccessLayer.Close();
		}

		public List<string> GetRoleIDs(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			string[] string0 = new string[] { "select * from ", this.string_0, " where STAFFID='", string_2, "'" };
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
			List<string> strs = new List<string>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow item = dataTable.Rows[i];
				ORGRole oRGRole = new ORGRole();
				strs.Add(Convert.ToString(item["ROLEID"]));
			}
			dataAccessLayer.Close();
			return strs;
		}

		public List<string> GetStaffIDs(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			string[] string0 = new string[] { "select * from ", this.string_0, " where ROLEID='", string_2, "'" };
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
			List<string> strs = new List<string>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow item = dataTable.Rows[i];
				ORGRole oRGRole = new ORGRole();
				strs.Add(Convert.ToString(item["STAFFID"]));
			}
			dataAccessLayer.Close();
			return strs;
		}
	}
}