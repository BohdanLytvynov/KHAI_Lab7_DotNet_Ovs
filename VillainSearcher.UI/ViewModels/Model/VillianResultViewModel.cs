using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillainSearcher.ViewModels.Model
{
    internal class VillianResultViewModel : VillainViewModel
    {
        #region Fields

        private double m_deviation;

        #endregion

        #region Properties
        public double Deviation 
        { get=>m_deviation; set=>Set(ref m_deviation, value); }
        #endregion

        #region Ctor
        public VillianResultViewModel()
        {
            
        }

        public VillianResultViewModel(double deviation)
        {
            #region Init Fields
            m_deviation = deviation;
            #endregion
        }
        #endregion

        #region Methods

        #endregion
    }
}
