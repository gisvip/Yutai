using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline3D
{
    abstract class Geometry3DBase: I3DGeometry
    {
        internal int _division;
        public Geometry3DBase(int division)
        {
            _division = division;
        }

        public abstract IGeometry CreateGeometry();
    }
}
