using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Check.Classes
{
    interface IGeometryCheck
    {
        BackgroundWorker Worker { get; set; }
        List<FeatureItem> Check();
    }
}
