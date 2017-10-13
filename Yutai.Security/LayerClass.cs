namespace Yutai.Security
{
	internal class LayerClass
	{
		private bool _Exist = false;

		private bool bChange = false;

		private int _oid;

		private string _fcname;

		private int _privilege = 0;

		private string _name;

		public bool Change
		{
			get
			{
				return this.bChange;
			}
		}

		public string DataType
		{
			get;
			set;
		}

		public bool Exist
		{
			get
			{
				return this._Exist;
			}
		}

		public string FeatureClassName
		{
			get
			{
				return this._fcname;
			}
			set
			{
				this._fcname = value;
			}
		}

		public string LayerName
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		public int OID
		{
			get
			{
				return this._oid;
			}
			set
			{
				this._oid = value;
			}
		}

		public int Privilege
		{
			get
			{
				return this._privilege;
			}
			set
			{
				if (this._privilege != value)
				{
					this.bChange = true;
					this._privilege = value;
				}
			}
		}

		public LayerClass(int oid, string LayerName, string fcname, int PrivilegsFlag, bool bExist)
		{
			this._oid = oid;
			this._name = LayerName;
			this._fcname = fcname;
			this._privilege = PrivilegsFlag;
			this._Exist = bExist;
			this.DataType = "gisLayer";
		}

		public LayerClass(int oid, string LayerName, string fcname, int PrivilegsFlag, bool bExist, string dt)
		{
			this._oid = oid;
			this._name = LayerName;
			this._fcname = fcname;
			this._privilege = PrivilegsFlag;
			this._Exist = bExist;
			this.DataType = dt;
		}

		public override string ToString()
		{
			return this._name;
		}
	}
}