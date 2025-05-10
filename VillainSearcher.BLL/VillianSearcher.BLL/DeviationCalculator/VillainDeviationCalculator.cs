using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianSearcher.DAL.Models;

namespace VillianSearcher.BLL.DeviationCalculator
{
    public class VillainDeviationCalculator : DeviationCalculatorBase<Villain>
    {
        public override double GetDeviationPercentage(Villain left, Villain right)
        {
            var nameDeviation = this.CalculateDeviationPercentage(left.Surename, right.Surename);
            var heightDeviation = this.CalculateDeviationPercentage(left.Height, right.Height);
            var headWidthDeviation = this.CalculateDeviationPercentage(left.HeadWidth, right.HeadWidth);
            var headHeightDeviation = this.CalculateDeviationPercentage(left.HeadHeight, right.HeadHeight);
            var eyeDistanceDeviation = this.CalculateDeviationPercentage(left.EyeDistance, right.EyeDistance);
            var armLengthDeviation = this.CalculateDeviationPercentage(left.ArmLength, right.ArmLength);

            return nameDeviation + heightDeviation 
                + headWidthDeviation 
                + headHeightDeviation 
                + eyeDistanceDeviation
                + armLengthDeviation;
        }
    }
}
