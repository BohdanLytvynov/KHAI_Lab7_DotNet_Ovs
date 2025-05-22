using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillainSearcher.Enums;
using VillainSearcher.ViewModels.Base.VM;
using VillainSearcher.ViewModels.Base.VM.Interfaces;
using Action = VillainSearcher.Enums.Action;

namespace VillainSearcher.ViewModels.Model
{
    internal abstract class VisualModelBase : 
        ViewModelBase, IValidatable, IInteractable
    {
        #region Fields
        private int m_ShowNumber;
        
        private bool m_isValid;

        private int m_Id;

        private Action m_Action;
        #endregion

        #region Properties

        public Action Action
        {
            get => m_Action;
            set => Set(ref m_Action, value);
        }

        public int ShowNumber
        {
            get => m_ShowNumber;
            set => Set(ref m_ShowNumber, value);
        }

        public bool IsValid 
        {
            get=>m_isValid; 
            set=>Set(ref m_isValid, value);
        }

        public int Id 
        {
            get=>m_Id; 
            set=>Set(ref m_Id, value);
        }

        #endregion

        #region Ctor
        protected VisualModelBase(int fieldsAmount) 
            : base(fieldsAmount) 
        {
            #region Init fields

            m_ShowNumber = -1;
            m_isValid = false;
            m_Id = -1;

            #endregion
        }
        #endregion
    }
}
