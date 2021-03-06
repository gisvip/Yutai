﻿// 项目名称 :  Yutai
//! 项目描述 :  
//! 类 名 称 :  IYTField.cs
//! 版 本 号 :  
//! 说    明 :  
//! 作    者 :  
//! 创建时间 :  2017/06/22  16:59
//! 更新时间 :  2017/06/22  16:59

using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Template.Concretes;

namespace Yutai.Plugins.Template.Interfaces
{
    public interface IYTField
    {
        int ID { get; set; }
        string Name { get; set; }
        string AliasName { get; set; }
        int Length { get; set; }
        int Precision { get; set; }
        bool IsKey { get; set; }
        string FieldTypeName { get; set; }
        esriFieldType FieldType { get; set; }
        bool AllowNull { get; set; }
        //! 如果有值字段，则定义，值中间用斜杠"/"分割
        string DomainName { get; set; }

        string DefaultValue { get; set; }

        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
        IYTField Clone(bool keepClass);
        void UpdateRow(IRow pRow);

        IField CreateField();
        bool IsValid(out string msg);

    }

    public interface IYTDomain
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        List<YTDomainValue> ValuePairs
        {
            get;
            set;
        }
        string DomainValues { get; set; }

        bool UpdateRow(IRow pRow);
    }

  
}