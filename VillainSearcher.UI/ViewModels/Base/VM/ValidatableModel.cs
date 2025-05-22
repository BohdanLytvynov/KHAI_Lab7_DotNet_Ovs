using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillainSearcher.ViewModels.Base.VM
{
    internal abstract class ValidatableModel : ViewModelBase
    {
        #region Fields
        private bool m_isValid;
        #endregion

        #region Properties
        public bool IsValid
        {
            get => m_isValid;
            set => Set(ref m_isValid, value);
        }
        #endregion

        #region Ctor
        protected ValidatableModel(int amountofFields)
            :base(amountofFields) 
        {
            m_isValid = false;
        }
        #endregion
    }
}
