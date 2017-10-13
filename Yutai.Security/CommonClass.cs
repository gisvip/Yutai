using System.Collections;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Security
{
	public class CommonClass
	{
		private static IWorkspace m_pWorkspace;

		private static string _server;

		private static string _database;

		private static string _user;

		private static Hashtable m_hash;

		public static string Database
		{
			get
			{
				return CommonClass._database;
			}
		}

		public static string Server
		{
			get
			{
				return CommonClass._server;
			}
		}

		public static string User
		{
			get
			{
				return CommonClass._user;
			}
		}

		public static IWorkspace Workspace
		{
			get
			{
				return CommonClass.m_pWorkspace;
			}
		}

		static CommonClass()
		{
			CommonClass.m_pWorkspace = null;
			CommonClass.m_hash = new Hashtable();
		}

		public CommonClass()
		{
		}

		public static ITable OpenTable(string TableName)
		{
			string tableName;
			ITable table;
			if (CommonClass.m_pWorkspace == null)
			{
				CommonClass.m_pWorkspace = AppConfigInfo.GetWorkspace();
			}
			if (CommonClass.m_pWorkspace != null)
			{
				object item = CommonClass.m_hash[TableName];
				if (item != null)
				{
					table = item as ITable;
				}
				else
				{
					if (TableName.IndexOf(".") != -1)
					{
						tableName = TableName;
					}
					else
					{
						ISQLSyntax workspace = CommonClass.Workspace as ISQLSyntax;
						tableName = workspace.QualifyTableName(CommonClass.Database, CommonClass.User, TableName);
					}
					ITable table1 = (CommonClass.m_pWorkspace as IFeatureWorkspace).OpenTable(tableName);
					CommonClass.m_hash[TableName] = table1;
					table = table1;
				}
			}
			else
			{
				table = null;
			}
			return table;
		}
	}
}