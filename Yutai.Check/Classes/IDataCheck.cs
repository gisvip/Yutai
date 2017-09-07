// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IDataCheck.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/15  11:00
// 更新时间 :  2017/08/15  11:00

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Yutai.Check.Enums;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Classes
{
    public interface IDataCheck
    {
        IAppContext AppContext { get; }
        IDataCheckConfig DataCheckConfig { get; }
        List<FeatureItem> Check(List<EnumCheckItem> items, BackgroundWorker worker);
        List<IPipelineLayer> PipelineLayers { get; }
        List<string> CheckPipelineList { get; set; }

        event EventHandler<string> ProgressChanged;
    }
}