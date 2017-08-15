// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  EnumCheckItem.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/15  09:20
// 更新时间 :  2017/08/15  09:20
namespace Yutai.Check.Enums
{
    public enum EnumCheckItem
    {
        P_FieldFull,        // 属性检查 字段完整性检查
        P_FieldRepeat,      // 属性检查 字段重复值检查
        P_FieldType,        // 属性检查 字段类型检查
        P_FieldLength,      // 属性检查 字段长度检查
        G_SinglePoint,      // 空间检查 孤立点检查
        G_SingleLine,       // 空间检查 孤立线检查
    }
}