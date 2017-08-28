// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ISelectField.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/08/25  09:41
// 更新时间 :  2017/08/25  09:41

using System.Windows.Forms;

namespace Yutai.Check.Classes
{
    public interface ISelectField
    {
        string FieldName { get; }
        DialogResult ShowDialog();
    }
}