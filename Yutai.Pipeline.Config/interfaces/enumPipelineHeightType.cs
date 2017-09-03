namespace Yutai.Pipeline.Config.Interfaces
{
    //! 管线高程类型，管顶，管底还是管中
    public enum enumPipelineHeightType
    {
        Top=0,
        Bottom=1,
        Middle=2
    }

    //! 管线埋深数据类型，绝对高程还是埋深
    public enum enumPipelineDepthType
    {
        Relative=0,
            Absolute=1
    }

    //! 管线断面规格类型，高*宽还是宽*高
    public enum enumPipeSectionType
    {
        HeightAndWidth=0,
        WidthAndHeight=1
    }
}