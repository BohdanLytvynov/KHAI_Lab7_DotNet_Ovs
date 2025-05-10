using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillianSearcher.BLL.DeviationCalculator
{
    public interface IDeviationCalculator<TObj>
    {
        double GetDeviationPercentage(TObj left, TObj right);
    }
}
