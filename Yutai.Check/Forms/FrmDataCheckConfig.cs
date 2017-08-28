using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Yutai.Check.Classes;
using Yutai.Check.Enums;

namespace Yutai.Check.Forms
{
    public partial class FrmDataCheckConfig : Form, IDataCheckConfig
    {
        private readonly string _path;
        private ISelectField _selectField;
        private BindingList<DomainItem> _domainItems;

        public FrmDataCheckConfig(string path)
        {
            InitializeComponent();
            _path = path;

            _domainItems = new BindingList<DomainItem>();
            listBoxFields.DataSource = _domainItems;
            listBoxFields.DisplayMember = "FieldName";

            LoadXml();
        }

        public void LoadXml()
        {
            if (File.Exists(_path) == false)
                return;
            XmlDocument document = new XmlDocument();
            document.Load(_path);
            SurfaceTolerance = LoadDoubleNode(document, "SurfaceTolerance");
            ElevationTolerance = LoadDoubleNode(document, "ElevationTolerance");
            CompareRadius = LoadDoubleNode(document, "CompareRadius");
            CompareLimit = LoadDoubleNode(document, "CompareLimit");
            EnumElevationCheckType elevationCheckType;
            ElevationCheckType = Enum.TryParse(LoadStringNode(document, "ElevationCheckType"), out elevationCheckType) ? elevationCheckType : EnumElevationCheckType.UseAttribute;
            ZxValue = LoadStringNode(document, "ZxValue");
            NxValue = LoadStringNode(document, "NxValue");
            StraightPnt = LoadStringNode(document, "StraightPnt");
            ThreeConnect = LoadStringNode(document, "ThreeConnect");
            FourConnect = LoadStringNode(document, "FourConnect");
            MultiConnect = LoadStringNode(document, "MultiConnect");
            PointMinimumSpacing = LoadDoubleNode(document, "PointMinimumSpacing");
            LineMinimumSpacing = LoadDoubleNode(document, "LineMinimumSpacing");
            MaxLength = LoadDoubleNode(document, "MaxLength");
            GroundElevationMin = LoadDoubleNode(document, "GroundElevationMin");
            GroundElevationMax = LoadDoubleNode(document, "GroundElevationMax");
            DepthMin = LoadDoubleNode(document, "DepthMin");
            DepthMax = LoadDoubleNode(document, "DepthMax");

            XmlNodeList domainsNodeList = document.SelectNodes("/CheckConfig/Domains/Domain");
            if (domainsNodeList == null)
            {
                return;
            }
            foreach (XmlNode node in domainsNodeList)
            {
                if (node.Attributes?["Name"] == null)
                    continue;
                DomainItem domainItem = new DomainItem(node.Attributes["Name"].Value);
                XmlNodeList valueNodeList =
                    document.SelectNodes($"/CheckConfig/Domains/Domain[@Name='{domainItem.FieldName}']/Values/Value");
                if (valueNodeList == null)
                    continue;
                foreach (XmlNode xmlNode in valueNodeList)
                {
                    domainItem.ValueList.Add(xmlNode.InnerText);
                }
                _domainItems.Add(domainItem);
            }
        }

        private double LoadDoubleNode(XmlDocument document, string name)
        {
            XmlNode node = document.SelectSingleNode($"/CheckConfig/{name}");
            if (node == null)
                return 0;
            string text = node.InnerText;
            double d;
            if (double.TryParse(text, out d))
                return d;
            else
                return 0;
        }

        private string LoadStringNode(XmlDocument document, string name)
        {
            XmlNode node = document.SelectSingleNode($"/CheckConfig/{name}");
            if (node == null)
                return "";
            return node.InnerText;
        }

        public double SurfaceTolerance
        {
            get { return (double)numSurfaceTolerance.Value; }
            set { numSurfaceTolerance.Value = (decimal)value; }
        }

        public double ElevationTolerance
        {
            get { return (double)numElevationTolerance.Value; }
            set { numElevationTolerance.Value = (decimal)value; }
        }

        public double CompareRadius
        {
            get { return (double)numCompareRadius.Value; }
            set { numCompareRadius.Value = (decimal)value; }
        }

        public double CompareLimit
        {
            get { return (double)numCompareLimit.Value; }
            set { numCompareLimit.Value = (decimal)value; }
        }

        public EnumElevationCheckType ElevationCheckType
        {
            get
            {
                return (EnumElevationCheckType)Enum.Parse(typeof(EnumElevationCheckType), radioGroupElevationCheckType.EditValue.ToString());
            }
            set
            {
                radioGroupElevationCheckType.EditValue = Enum.GetName(typeof(EnumElevationCheckType), value);
            }
        }

        public string ZxValue
        {
            get { return textBoxZX.Text; }
            set { textBoxZX.Text = value; }
        }

        public string NxValue
        {
            get { return textBoxNX.Text; }
            set { textBoxNX.Text = value; }
        }

        public string StraightPnt
        {
            get { return textBoxStraightPnt.Text; }
            set { textBoxStraightPnt.Text = value; }
        }

        public string ThreeConnect
        {
            get { return textBoxThreeConnect.Text; }
            set { textBoxThreeConnect.Text = value; }
        }

        public string FourConnect
        {
            get { return textBoxFourConnect.Text; }
            set { textBoxFourConnect.Text = value; }
        }

        public string MultiConnect
        {
            get { return textBoxMultiConnect.Text; }
            set { textBoxMultiConnect.Text = value; }
        }

        public BindingList<DomainItem> DomainItems
        {
            get { return _domainItems; }
            set { _domainItems = value; }
        }

        public double PointMinimumSpacing
        {
            get { return (double) numPointMinimumSpacing.Value; }
            set { numPointMinimumSpacing.Value = (decimal) value; }
        }

        public double LineMinimumSpacing
        {
            get { return (double) numLineMinimumSpacing.Value; }
            set { numLineMinimumSpacing.Value = (decimal) value; }
        }

        public double MaxLength
        {
            get { return (double) numMaxLength.Value; }
            set { numMaxLength.Value = (decimal) value; }
        }

        public double GroundElevationMin
        {
            get { return (double) numGroundElevationMin.Value; }
            set { numGroundElevationMin.Value = (decimal) value; }
        }

        public double GroundElevationMax
        {
            get { return (double) numGroundElevationMax.Value; }
            set { numGroundElevationMax.Value = (decimal) value; }
        }

        public double DepthMin
        {
            get { return (double) numDepthMin.Value; }
            set { numDepthMin.Value = (decimal) value; }
        }

        public double DepthMax
        {
            get { return (double) numDepthMax.Value; }
            set { numDepthMax.Value = (decimal) value; }
        }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(_path))
                return;
            SaveToXml(_path);
        }

        public void SaveToXml(string filePath)
        {
            XmlDocument document = new XmlDocument();

            XmlNode rootNode = document.CreateElement("CheckConfig");

            rootNode.AppendChild(CreateNode(document, "SurfaceTolerance", SurfaceTolerance.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "ElevationTolerance", ElevationTolerance.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "CompareRadius", CompareRadius.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "CompareLimit", CompareLimit.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "ElevationCheckType", Enum.GetName(typeof(EnumElevationCheckType), ElevationCheckType)));
            rootNode.AppendChild(CreateNode(document, "ZxValue", ZxValue));
            rootNode.AppendChild(CreateNode(document, "NxValue", NxValue));
            rootNode.AppendChild(CreateNode(document, "StraightPnt", StraightPnt));
            rootNode.AppendChild(CreateNode(document, "ThreeConnect", ThreeConnect));
            rootNode.AppendChild(CreateNode(document, "FourConnect", FourConnect));
            rootNode.AppendChild(CreateNode(document, "MultiConnect", MultiConnect));
            rootNode.AppendChild(CreateNode(document, "PointMinimumSpacing", PointMinimumSpacing.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "LineMinimumSpacing", LineMinimumSpacing.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "MaxLength", MaxLength.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "GroundElevationMin", GroundElevationMin.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "GroundElevationMax", GroundElevationMax.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "DepthMin", DepthMin.ToString(CultureInfo.InvariantCulture)));
            rootNode.AppendChild(CreateNode(document, "DepthMax", DepthMax.ToString(CultureInfo.InvariantCulture)));

            XmlNode domainsNode = document.CreateElement("Domains");
            foreach (DomainItem domainItem in _domainItems)
            {
                XmlNode domainNode = document.CreateElement("Domain");
                XmlAttribute attribute = document.CreateAttribute("Name");
                attribute.Value = domainItem.FieldName;
                domainNode.Attributes.Append(attribute);
                XmlNode valuesNode = document.CreateElement("Values");
                foreach (string value in domainItem.ValueList)
                {
                    valuesNode.AppendChild(CreateNode(document, "Value", value));
                }
                domainNode.AppendChild(valuesNode);
                domainsNode.AppendChild(domainNode);
            }
            rootNode.AppendChild(domainsNode);
            document.AppendChild(rootNode);
            document.Save(filePath);
        }

        private XmlNode CreateNode(XmlDocument document, string name, string value)
        {
            XmlNode surfaceToleranceNode = document.CreateElement(name);
            surfaceToleranceNode.InnerText = value;
            return surfaceToleranceNode;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selectField == null)
                _selectField = new FrmSelectField();
            _selectField.ShowDialog();
            _domainItems.Add(new DomainItem(_selectField.FieldName));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBoxFields.SelectedItem == null)
                return;
            string fieldName = listBoxFields.SelectedItem.ToString();
            DomainItem item = _domainItems.FirstOrDefault(c => c.FieldName == fieldName);
            if (item == null)
                return;
            _domainItems.Remove(item);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DomainItem item = listBoxFields.SelectedItem as DomainItem;
            if (item == null)
                return;
            item.ValueList.Clear();
            string[] valueStrings = this.richTextBoxValues.Lines;
            foreach (string valueString in valueStrings)
            {
                if (string.IsNullOrWhiteSpace(valueString))
                    continue;
                item.ValueList.Add(valueString);
            }
        }

        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            DomainItem item = listBoxFields.SelectedItem as DomainItem;
            if (item == null)
                return;
            this.richTextBoxValues.Text = string.Empty;
            StringBuilder stringBuilder = new StringBuilder(string.Empty);
            foreach (string value in item.ValueList)
            {
                stringBuilder.AppendLine(value);
            }
            this.richTextBoxValues.Text = stringBuilder.ToString();
        }
        
        private void FrmDataCheckConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            Save();
        }
    }
}
