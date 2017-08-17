// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IDataCheckConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/17  15:28
// 更新时间 :  2017/08/17  15:28

using System.Windows.Forms;

namespace Yutai.Check.Classes
{
    public interface IDataCheckConfig
    {
        double SurfaceTolerance { get; set; }
        double ElevationTolerance { get; set; }

        DialogResult ShowDialog();
    }
}