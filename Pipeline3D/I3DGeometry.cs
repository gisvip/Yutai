// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  I3DGeometry.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/10/24  17:08
// 更新时间 :  2017/10/24  17:08

using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    public interface I3DGeometry
    {
        IGeometry CreateGeometry();
    }
}