using System;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;

namespace Yutai.Pipeline.Analysis
{
    internal class ProjectListener
    {
        private readonly IAppContext _context;
        private PipelineAnalysisPlugin _plugin;

        public ProjectListener(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (plugin == null) throw new ArgumentNullException("plugin");

            _context = context;
            _plugin = plugin;
            _plugin.ProjectClosed += OnProjectClosed;
            _plugin.ProjectOpened += Plugin_ProjectOpened;
        }

        private void Plugin_ProjectOpened(object sender, EventArgs e)
        {
            if (_plugin.PipeConfig.ProjectFile != _context.Project.Filename)
            {
                _plugin.PipeConfig.LinkMap(_context.FocusMap);
                _plugin.PipeConfig.ProjectFile = _context.Project.Filename;
            }

        }


        private void OnProjectClosed(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_plugin.PipeConfig.ProjectFile)!=false)
                _plugin.PipeConfig.Clear();
        }
    }
}