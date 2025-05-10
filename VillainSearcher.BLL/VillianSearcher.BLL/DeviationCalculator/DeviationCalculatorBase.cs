using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillianSearcher.BLL.DeviationCalculator
{
    public abstract class DeviationCalculatorBase<TObj> : IDeviationCalculator<TObj>
    {
        public abstract double GetDeviationPercentage(TObj left, TObj right);

        protected double CalculateDeviationPercentage(string p1, string p2)
        {
            int hash1 = p1.GetHashCode();
            int hash2 = p2.GetHashCode();

            return CalculateDeviationPercentage(hash1, hash2);
        }

        protected double CalculateDeviationPercentage(double p1, double p2)
        {
            double avg = (p1 + p2) / 2;

            return Math.Abs(p1 - p2) / avg * 100;
        }
    }
}
