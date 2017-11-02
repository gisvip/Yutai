using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline3D
{
    public interface I3DItem
    {
        I3DBuilder Builder { get; }
        string Name { get; }

        enumPipelineHeightType HeightType { get; set; }
        enumPipeSectionType SectionType { get; set; }
        enumRotationAngleType RotationAngleType { get; set; }

        IPipelineLayer PipelineLayer { get; }
        IBasicLayerInfo PointLayerInfo { get; }
        IBasicLayerInfo LineLayerInfo { get; }
        IFeatureClass PointPatchClass { get; }
        IFeatureClass LinePatchClass { get; }

        List<string> FswValueList { get; }
        List<string> CylinderSubs { get; set; }
        List<string> SquareSubs { get; set; }


        string DmgcFieldName { get; set; }
        int IdxDmgcField { get; }
        string JdsdFieldName { get; set; }
        int IdxJdsdField { get; }
        string JgggFieldName { get; set; }
        int IdxJgggField { get; }
        string XzjdFieldName { get; set; }
        int IdxXzjdField { get; }
        string FswFieldName { get; set; }
        int IdxFswField { get; }
        int IdxPointLinkField { get; }

        string QdgcFieldName { get; set; }
        int IdxQdgcField { get; }
        string ZdgcFieldName { get; set; }
        int IdxZdgcField { get; }
        string QdmsFieldName { get; set; }
        int IdxQdmsField { get; }
        string ZdmsFieldName { get; set; }
        int IdxZdmsField { get; }
        string GjFieldName { get; set; }
        int IdxGjField { get; }
        int IdxLineLinkField { get; }
    }
}