// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IDataCheckConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/17  15:28
// 更新时间 :  2017/08/17  15:28

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yutai.Check.Enums;

namespace Yutai.Check.Classes
{
    public interface IDataCheckConfig
    {
        void LoadXml();
        double SurfaceTolerance { get; set; }
        double ElevationTolerance { get; set; }
        double CompareRadius { get; set; }
        double CompareLimit { get; set; }
        EnumElevationCheckType ElevationCheckType { get; set; }
        string ZxValue { get; set; }
        string NxValue { get; set; }
        string StraightPnt { get; set; }
        string ThreeConnect { get; set; }
        string FourConnect { get; set; }
        string MultiConnect { get; set; }
        BindingList<DomainItem> DomainItems { get; set; }
        double PointMinimumSpacing { get; set; }
        double LineMinimumSpacing { get; set; }
        double MaxLength { get; set; }
        double GroundElevationMin { get; set; }
        double GroundElevationMax { get; set; }
        double DepthMin { get; set; }
        double DepthMax { get; set; }

        DialogResult ShowDialog();
        void Save();
        void SaveToXml(string filePath);
    }
}