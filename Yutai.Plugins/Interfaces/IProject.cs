﻿namespace Yutai.Plugins.Interfaces
{
    //项目文件接口
    public interface IProject
    {
        void SetModified();
        bool Modified { get; }
        string Filename { get; set; }
        bool IsEmpty { get; }
        string Title { get; set; }
    }
}