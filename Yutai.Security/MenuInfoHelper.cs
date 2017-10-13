using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Yutai.Security
{
	public class MenuInfoHelper
	{
		private string string_0 = "";

		private DataProviderType dataProviderType_0 = DataProviderType.Sql;

		private string string_1 = "";

		public MenuInfoHelper()
		{
			try
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
			}
			catch
			{
			}
			this.string_0 = "MAINMENUCONFIG";
		}

		public MenuInfoHelper(string string_2)
		{
			try
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
			}
			catch
			{
			}
			this.string_0 = string_2;
		}

		public int Add(MenuInfo menuInfo_0)
		{
			int num;
			int value;
			object str;
			object obj;
			object obj1;
			object str1;
			object obj2;
			object obj3;
			if (!this.IsExist(menuInfo_0))
			{
				object[] string0 = new object[14];
				string0[0] = this.string_0;
				string0[1] = (string.IsNullOrEmpty(menuInfo_0.NAME) ? "NULL" : string.Concat("'", menuInfo_0.NAME, "'"));
				string0[2] = (string.IsNullOrEmpty(menuInfo_0.PARENTIDS) ? "NULL" : string.Concat("'", menuInfo_0.PARENTIDS, "'"));
				object[] objArray = string0;
				if (menuInfo_0.ORDERBY.HasValue)
				{
					value = menuInfo_0.ORDERBY.Value;
					str = value.ToString();
				}
				else
				{
					str = "NULL";
				}
				objArray[3] = str;
				string0[4] = (string.IsNullOrEmpty(menuInfo_0.PROGID) ? "NULL" : string.Concat("'", menuInfo_0.PROGID, "'"));
				string0[5] = (string.IsNullOrEmpty(menuInfo_0.SHORTCUT) ? "NULL" : string.Concat("'", menuInfo_0.SHORTCUT, "'"));
				object[] objArray1 = string0;
				if (menuInfo_0.SUBTYPE.HasValue)
				{
					value = menuInfo_0.SUBTYPE.Value;
					obj = value.ToString();
				}
				else
				{
					obj = "NULL";
				}
				objArray1[6] = obj;
				object[] objArray2 = string0;
				if (menuInfo_0.VISIBLE.HasValue)
				{
					obj1 = (menuInfo_0.VISIBLE.Value ? "1" : "0");
				}
				else
				{
					obj1 = "NULL";
				}
				objArray2[7] = obj1;
				object[] objArray3 = string0;
				if (menuInfo_0.ItemCol.HasValue)
				{
					value = menuInfo_0.ItemCol.Value;
					str1 = value.ToString();
				}
				else
				{
					str1 = "NULL";
				}
				objArray3[8] = str1;
				object[] objArray4 = string0;
				if (menuInfo_0.ISPOPMENUITEM.HasValue)
				{
					obj2 = (menuInfo_0.ISPOPMENUITEM.Value ? "1" : "0");
				}
				else
				{
					obj2 = "NULL";
				}
				objArray4[9] = obj2;
				string0[10] = (string.IsNullOrEmpty(menuInfo_0.COMPONENTDLLNAME) ? "NULL" : string.Concat("'", menuInfo_0.COMPONENTDLLNAME, "'"));
				string0[11] = (string.IsNullOrEmpty(menuInfo_0.CLASSNAME) ? "NULL" : string.Concat("'", menuInfo_0.CLASSNAME, "'"));
				object[] objArray5 = string0;
				if (menuInfo_0.BEGINGROUP.HasValue)
				{
					obj3 = (menuInfo_0.BEGINGROUP.Value ? "1" : "0");
				}
				else
				{
					obj3 = "NULL";
				}
				objArray5[12] = obj3;
				string0[13] = (string.IsNullOrEmpty(menuInfo_0.CAPTION) ? "NULL" : string.Concat("'", menuInfo_0.CAPTION, "'"));
				string str2 = string.Format("insert into {0}([NAME],PARENTIDS,ORDERBY,[PROGID],[SHORTCUT],SUBTYPE,VISIBLE,ItemCol,ISPOPMENUITEM,COMPONENTDLLNAME,CLASSNAME,BEGINGROUP,CAPTION) values({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", string0);
				DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
				dataAccessLayer.Open();
				dataAccessLayer.ExecuteNonQuery(CommandType.Text, str2);
				dataAccessLayer.Close();
				num = 0;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		public void ClearAll()
		{
			try
			{
				DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
				dataAccessLayer.Open();
				dataAccessLayer.ExecuteNonQuery(string.Concat("delete from ", this.string_0), new object[0]);
				dataAccessLayer.Close();
			}
			catch
			{
			}
		}

		public MenuInfo GetByClassName(string string_2, string string_3)
		{
			MenuInfo menuInfo = null;
			try
			{
				DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
				dataAccessLayer.Open();
				string[] string0 = new string[] { "select * from ", this.string_0, " where COMPONENTDLLNAME='", string_2, "' and CLASSNAME='", string_3, "'" };
				DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
				if (dataTable.Rows.Count > 0)
				{
					DataRow item = dataTable.Rows[0];
					menuInfo = new MenuInfo()
					{
						MenuID = Convert.ToString(item["MenuID"]),
						NAME = Convert.ToString(item["NAME"])
					};
					object obj = item["ORDERBY"];
					menuInfo.ORDERBY = new int?((!(item["ORDERBY"] is DBNull) ? Convert.ToInt32(item["ORDERBY"]) : -1));
					menuInfo.PROGID = Convert.ToString(item["PROGID"]);
					menuInfo.SHORTCUT = Convert.ToString(item["SHORTCUT"]);
					menuInfo.SUBTYPE = new int?((!(item["SUBTYPE"] is DBNull) ? Convert.ToInt32(item["SUBTYPE"]) : -1));
					menuInfo.VISIBLE = new bool?((!(item["VISIBLE"] is DBNull) ? Convert.ToInt32(item["VISIBLE"]) == 1 : true));
					menuInfo.ItemCol = new int?((!(item["ItemCol"] is DBNull) ? Convert.ToInt32(item["ItemCol"]) : -1));
					menuInfo.ISPOPMENUITEM = new bool?((!(item["ISPOPMENUITEM"] is DBNull) ? Convert.ToInt32(item["ISPOPMENUITEM"]) == 1 : false));
					menuInfo.COMPONENTDLLNAME = Convert.ToString(item["COMPONENTDLLNAME"]);
					menuInfo.CLASSNAME = Convert.ToString(item["CLASSNAME"]);
					menuInfo.BEGINGROUP = new bool?((!(item["BEGINGROUP"] is DBNull) ? Convert.ToInt32(item["BEGINGROUP"]) == 1 : false));
					menuInfo.CAPTION = Convert.ToString(item["CAPTION"]);
					menuInfo.PARENTIDS = Convert.ToString(item["PARENTIDS"]);
				}
				dataAccessLayer.Close();
			}
			catch
			{
			}
			return menuInfo;
		}

		public MenuInfo GetByClassName(string string_2)
		{
			MenuInfo menuInfo = null;
			try
			{
				DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
				dataAccessLayer.Open();
				string[] string0 = new string[] { "select * from ", this.string_0, " where PROGID='", string_2, "'" };
				DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat(string0));
				if (dataTable.Rows.Count > 0)
				{
					DataRow item = dataTable.Rows[0];
					menuInfo = new MenuInfo()
					{
						MenuID = Convert.ToString(item["MenuID"]),
						NAME = Convert.ToString(item["NAME"])
					};
					object obj = item["ORDERBY"];
					menuInfo.ORDERBY = new int?((!(item["ORDERBY"] is DBNull) ? Convert.ToInt32(item["ORDERBY"]) : -1));
					menuInfo.PROGID = Convert.ToString(item["PROGID"]);
					menuInfo.SHORTCUT = Convert.ToString(item["SHORTCUT"]);
					menuInfo.SUBTYPE = new int?((!(item["SUBTYPE"] is DBNull) ? Convert.ToInt32(item["SUBTYPE"]) : -1));
					menuInfo.VISIBLE = new bool?((!(item["VISIBLE"] is DBNull) ? Convert.ToInt32(item["VISIBLE"]) == 1 : true));
					menuInfo.ItemCol = new int?((!(item["ItemCol"] is DBNull) ? Convert.ToInt32(item["ItemCol"]) : -1));
					menuInfo.ISPOPMENUITEM = new bool?((!(item["ISPOPMENUITEM"] is DBNull) ? Convert.ToInt32(item["ISPOPMENUITEM"]) == 1 : false));
					menuInfo.COMPONENTDLLNAME = Convert.ToString(item["COMPONENTDLLNAME"]);
					menuInfo.CLASSNAME = Convert.ToString(item["CLASSNAME"]);
					menuInfo.BEGINGROUP = new bool?((!(item["BEGINGROUP"] is DBNull) ? Convert.ToInt32(item["BEGINGROUP"]) == 1 : false));
					menuInfo.CAPTION = Convert.ToString(item["CAPTION"]);
					menuInfo.PARENTIDS = Convert.ToString(item["PARENTIDS"]);
				}
				dataAccessLayer.Close();
			}
			catch
			{
			}
			return menuInfo;
		}

		public MenuInfo GetByName(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from ", this.string_0, " where NAME=", string_2));
			MenuInfo menuInfo = null;
			if (dataTable.Rows.Count > 0)
			{
				DataRow item = dataTable.Rows[0];
				menuInfo = new MenuInfo()
				{
					MenuID = Convert.ToString(item["MenuID"])
				};
				object obj = item["NAME"];
				menuInfo.NAME = Convert.ToString(item["NAME"]);
				menuInfo.ORDERBY = new int?((!(item["ORDERBY"] is DBNull) ? Convert.ToInt32(item["ORDERBY"]) : -1));
				menuInfo.PROGID = Convert.ToString(item["PROGID"]);
				menuInfo.SHORTCUT = Convert.ToString(item["SHORTCUT"]);
				menuInfo.SUBTYPE = new int?((!(item["SUBTYPE"] is DBNull) ? Convert.ToInt32(item["SUBTYPE"]) : -1));
				menuInfo.VISIBLE = new bool?((!(item["VISIBLE"] is DBNull) ? Convert.ToInt32(item["VISIBLE"]) == 1 : true));
				menuInfo.ItemCol = new int?((!(item["ItemCol"] is DBNull) ? Convert.ToInt32(item["ItemCol"]) : -1));
				menuInfo.ISPOPMENUITEM = new bool?((!(item["ISPOPMENUITEM"] is DBNull) ? Convert.ToInt32(item["ISPOPMENUITEM"]) == 1 : false));
				menuInfo.COMPONENTDLLNAME = Convert.ToString(item["COMPONENTDLLNAME"]);
				menuInfo.CLASSNAME = Convert.ToString(item["CLASSNAME"]);
				menuInfo.BEGINGROUP = new bool?((!(item["BEGINGROUP"] is DBNull) ? Convert.ToInt32(item["BEGINGROUP"]) == 1 : false));
				menuInfo.CAPTION = Convert.ToString(item["CAPTION"]);
				menuInfo.PARENTIDS = Convert.ToString(item["PARENTIDS"]);
			}
			dataAccessLayer.Close();
			return menuInfo;
		}

		public bool IsExist(MenuInfo menuInfo_0)
		{
			string str = "";
			str = (!string.IsNullOrEmpty(menuInfo_0.PROGID) ? string.Format("select * from {0} where  PROGID='{1}' ", this.string_0, menuInfo_0.PROGID) : string.Format("select * from {0} where  COMPONENTDLLNAME='{1}' and CLASSNAME='{2}'", this.string_0, menuInfo_0.COMPONENTDLLNAME, menuInfo_0.CLASSNAME));
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(str);
			dataAccessLayer.Close();
			return dataTable.Rows.Count > 0;
		}

		public List<MenuInfo> Load()
		{
			List<MenuInfo> menuInfos = new List<MenuInfo>();
			try
			{
				DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
				dataAccessLayer.Open();
				DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from ", this.string_0));
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow item = dataTable.Rows[i];
					MenuInfo menuInfo = new MenuInfo()
					{
						MenuID = Convert.ToString(item["MenuID"]),
						NAME = Convert.ToString(item["NAME"])
					};
					object obj = item["ORDERBY"];
					menuInfo.ORDERBY = new int?((!(item["ORDERBY"] is DBNull) ? Convert.ToInt32(item["ORDERBY"]) : -1));
					menuInfo.PROGID = Convert.ToString(item["PROGID"]);
					menuInfo.SHORTCUT = Convert.ToString(item["SHORTCUT"]);
					menuInfo.SUBTYPE = new int?((!(item["SUBTYPE"] is DBNull) ? Convert.ToInt32(item["SUBTYPE"]) : -1));
					menuInfo.VISIBLE = new bool?((!(item["VISIBLE"] is DBNull) ? Convert.ToInt32(item["VISIBLE"]) == 1 : true));
					menuInfo.ItemCol = new int?((!(item["ItemCol"] is DBNull) ? Convert.ToInt32(item["ItemCol"]) : -1));
					menuInfo.ISPOPMENUITEM = new bool?((!(item["ISPOPMENUITEM"] is DBNull) ? Convert.ToInt32(item["ISPOPMENUITEM"]) == 1 : false));
					menuInfo.COMPONENTDLLNAME = Convert.ToString(item["COMPONENTDLLNAME"]);
					menuInfo.CLASSNAME = Convert.ToString(item["CLASSNAME"]);
					menuInfo.PARENTIDS = Convert.ToString(item["PARENTIDS"]);
					menuInfo.BEGINGROUP = new bool?((!(item["BEGINGROUP"] is DBNull) ? Convert.ToInt32(item["BEGINGROUP"]) == 1 : false));
					menuInfo.CAPTION = Convert.ToString(item["CAPTION"]);
					menuInfos.Add(menuInfo);
				}
				dataAccessLayer.Close();
			}
			catch
			{
			}
			return menuInfos;
		}

		public MenuInfo Load(string string_2)
		{
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			DataTable dataTable = dataAccessLayer.ExecuteDataTable(string.Concat("select * from ", this.string_0, " where MenuID=", string_2));
			MenuInfo menuInfo = null;
			if (dataTable.Rows.Count > 0)
			{
				DataRow item = dataTable.Rows[0];
				menuInfo = new MenuInfo()
				{
					MenuID = Convert.ToString(item["MenuID"])
				};
				object obj = item["NAME"];
				menuInfo.NAME = Convert.ToString(item["NAME"]);
				menuInfo.ORDERBY = new int?((!(item["ORDERBY"] is DBNull) ? Convert.ToInt32(item["ORDERBY"]) : -1));
				menuInfo.PROGID = Convert.ToString(item["PROGID"]);
				menuInfo.SHORTCUT = Convert.ToString(item["SHORTCUT"]);
				menuInfo.SUBTYPE = new int?((!(item["SUBTYPE"] is DBNull) ? Convert.ToInt32(item["SUBTYPE"]) : -1));
				menuInfo.VISIBLE = new bool?((!(item["VISIBLE"] is DBNull) ? Convert.ToInt32(item["VISIBLE"]) == 1 : true));
				menuInfo.ItemCol = new int?((!(item["ItemCol"] is DBNull) ? Convert.ToInt32(item["ItemCol"]) : -1));
				menuInfo.ISPOPMENUITEM = new bool?((!(item["ISPOPMENUITEM"] is DBNull) ? Convert.ToInt32(item["ISPOPMENUITEM"]) == 1 : false));
				menuInfo.COMPONENTDLLNAME = Convert.ToString(item["COMPONENTDLLNAME"]);
				menuInfo.CLASSNAME = Convert.ToString(item["CLASSNAME"]);
				menuInfo.BEGINGROUP = new bool?((!(item["BEGINGROUP"] is DBNull) ? Convert.ToInt32(item["BEGINGROUP"]) == 1 : false));
				menuInfo.CAPTION = Convert.ToString(item["CAPTION"]);
				menuInfo.PARENTIDS = Convert.ToString(item["PARENTIDS"]);
			}
			dataAccessLayer.Close();
			return menuInfo;
		}

		public void Update(MenuInfo menuInfo_0)
		{
			string str = string.Format("update {0} set PARENTIDS='{1}' where MenuID={2}", this.string_0, (string.IsNullOrEmpty(menuInfo_0.PARENTIDS) ? "NULL" : string.Concat("'", menuInfo_0.PARENTIDS, "'")), menuInfo_0.MenuID);
			DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(this.dataProviderType_0, this.string_1);
			dataAccessLayer.Open();
			dataAccessLayer.ExecuteNonQuery(CommandType.Text, str);
			dataAccessLayer.Close();
		}
	}
}