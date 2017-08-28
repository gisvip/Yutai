using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yutai.Check.Classes
{
    public class DomainItem
    {
        private string _fieldName;
        private List<string> _valueList;

        public DomainItem(string fieldName)
        {
            _fieldName = fieldName;
            _valueList = new List<string>();
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public List<string> ValueList
        {
            get { return _valueList; }
            set { _valueList = value; }
        }
    }
}
