using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline3D
{
    public interface I3DBuilder
    {
        int Division { get; }
        string NameSuf { get; }
        List<I3DItem> Items { get; }
        IWorkspace SaveWorkspace { get; }
        List<string> GetCheckPipeline();
    }
}