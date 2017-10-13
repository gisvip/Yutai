namespace Yutai.Security
{
	public interface ISQLSelecter
	{
		string SQL
		{
			get;
			set;
		}

		void Dispose();

		void Execute();

		string getFieldValue(string string_0);
	}
}