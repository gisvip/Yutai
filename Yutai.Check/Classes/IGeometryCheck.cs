using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Check.Classes
{
    interface IGeometryCheck
    {
        List<FeatureItem> Check();
    }
}
