using System.Data;

namespace Yutai.Security
{
	public interface ISQLTable
	{
		DataTable ExecuteDataTable(string string_0);

		DataSet ExecuteKDDataSet(string string_0);
	}
}