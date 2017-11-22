using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto
{
    public class RepresentationAssist
    {
        internal static IBasicSymbol CreateBasicSymbol(IFeatureClass pFeatureClass)
        {
            IBasicSymbol symbol = null;
            if ((pFeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint) ||
                (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPoint))
            {
                return new BasicMarkerSymbolClass();
            }
            if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                return new BasicLineSymbolClass();
            }
            if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                symbol = new BasicFillSymbolClass();
            }
            return symbol;
        }

        public IRepresentationClass CreateRepClass(IFeatureClass pFeatureClass)
        {
            IDataset dataset = pFeatureClass as IDataset;
            IRepresentationWorkspaceExtension repWSExt = GetRepWSExt(dataset.Workspace);
            if (repWSExt != null)
            {
                IRepresentationRules rules = new RepresentationRulesClass();
                return repWSExt.CreateRepresentationClass(pFeatureClass, pFeatureClass.AliasName + "_Rep",
                    "My_RuleID", "My_Override", false, rules, null);
            }
            return null;
        }

        public static IRepresentationRule CreateRepresentationRule(IFeatureClass pFeatureClass)
        {
            IBasicSymbol symbol = CreateBasicSymbol(pFeatureClass);
            IRepresentationRule rule = new RepresentationRuleClass();
            rule.InsertLayer(0, symbol);
            return rule;
        }

        public static IRepresentationWorkspaceExtension GetRepWSExt(IWorkspace pWorkspace)
        {
            try
            {
                IWorkspaceExtensionManager manager = pWorkspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass
                {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (manager.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        public static IRepresentationWorkspaceExtension GetRepWSExtFromFClass(IFeatureClass pFeatureClass)
        {
            try
            {
                IDataset dataset = pFeatureClass as IDataset;
                IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass
                {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        public static bool HasRepresentation(IFeatureClass pFeatureClass)
        {
            if (pFeatureClass != null)
            {
                try
                {
                    IDataset dataset = pFeatureClass as IDataset;
                    IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                    UID gUID = new UIDClass
                    {
                        Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                    };
                    IRepresentationWorkspaceExtension extension =
                        workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                    if (extension == null)
                    {
                        return false;
                    }
                    return extension.get_FeatureClassHasRepresentations(pFeatureClass);
                }
                catch
                {
                }
            }
            return false;
        }
    }
}