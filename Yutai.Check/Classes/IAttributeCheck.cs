// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IAttributeCheck.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/15  15:16
// 更新时间 :  2017/08/15  15:16

using System.Collections.Generic;
using System.ComponentModel;
using Yutai.Check.Enums;

namespace Yutai.Check.Classes
{
    public interface IAttributeCheck
    {
        BackgroundWorker Worker { get; set; }
        List<FeatureItem> Check();
    }
}