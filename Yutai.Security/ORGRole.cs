namespace Yutai.Security
{
	public class ORGRole
	{
		private string string_0 = "";

		private string string_1 = "";

		private string string_2 = "";

		public string DESCRIPT
		{
			get
			{
				return this.string_2;
			}
			set
			{
				this.string_2 = value;
			}
		}

		public string RoleID
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public string RoleName
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public ORGRole()
		{
		}
	}
}