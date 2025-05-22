
using VillainSearcher.Enums;

namespace VillainSearcher.ViewModels.Base.VM
{
    internal abstract class InteractableModel : ViewModelBase
    {
        #region Fields

        private Action m_Action;

        #endregion

        #region Properties

        public Action Action
        {
            get => m_Action;
            set => Set(ref m_Action, value);
        }

        #endregion

        #region Ctor
        protected InteractableModel(int fieldAmount)
            : base(fieldAmount)
        {
            #region Init Fields

            m_Action = Action.None;

            #endregion
        }
        #endregion

    }
}
