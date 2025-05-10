using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VillainSearcher.ViewModels.Base.Commands;
using VillainSearcher.ViewModels.Base.VM;
using VillianSearcher.BLL.Validators;

namespace VillainSearcher.ViewModels.Model
{
    internal class VillainViewModel : VisualModelBase
    {
        #region Fields

        private string m_Surename;
        private string m_Height;
        private string m_HeadWidth;
        private string m_HeadHeight;
        private string m_ArmLength;
        private string m_EyeDistance;

        #endregion

        #region Properties

        public string Surename 
        {
            get=> m_Surename;
            set=>Set(ref m_Surename, value);
        }

        public string Height 
        {
            get=>m_Height;
            set=>Set(ref m_Height, value);
        }

        public string HeadWidth 
        {
            get=>m_HeadWidth;
            set => Set(ref m_HeadWidth, value);
        }

        public string HeadHeight 
        {
            get=>m_HeadHeight;
            set=>Set(ref m_HeadHeight, value);
        }

        public string ArmLength 
        {
            get=>m_ArmLength;
            set=>Set(ref m_ArmLength, value);
        }

        public string EyeDistance 
        {
            get=>m_EyeDistance;
            set=>Set(ref m_EyeDistance, value);
        }

        #endregion

        #region IDataErrorInfo

        public override string this[string columnName]
        {
            get 
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(Surename):
                        SetValidArrayValue(0, ValidationHelper.IsNSLValid(Surename, out error));
                        break;
                    case nameof(Height):
                        SetValidArrayValue(1, ValidationHelper.ValidateNumber(Height, out error)); 
                        break;
                    case nameof(HeadWidth):
                        SetValidArrayValue(2, ValidationHelper.ValidateNumber(HeadWidth, out error));
                        break;
                    case nameof(HeadHeight):
                        SetValidArrayValue(3, ValidationHelper.ValidateNumber(HeadHeight, out error));
                        break;
                    case nameof(ArmLength):
                        SetValidArrayValue(4, ValidationHelper.ValidateNumber(ArmLength, out error));
                        break;
                    case nameof(EyeDistance):
                        SetValidArrayValue(5, ValidationHelper.ValidateNumber(EyeDistance, out error));
                        break;
                }

                IsValid = ValidateFields(0, GetValidArrayCount() - 1);

                return error;
            }
        }

        #endregion

        #region Commands

        public ICommand OnAbortDeletePressed { get; }

        #endregion

        #region Ctor
        public VillainViewModel() : this(-1, "enter smth",
            "enter smth",
            "enter smth",
            "enter smth",
            "enter smth",
            "enter smth")
        {
            
        }

        public VillainViewModel(int id,
            string surename,
            string height,
            string headWidth,
            string headHeight,
            string armLength,
            string eyeDistance) : base(6)
        {
            #region Init fields
            Id = id;
            m_Surename = surename;
            m_Height = height;
            m_HeadWidth = headWidth;
            m_HeadHeight = headHeight;
            m_ArmLength = armLength;
            m_EyeDistance = eyeDistance;

            #endregion

            #region Init Commands

            OnAbortDeletePressed = new Command(
                OnAbortDeleteButtonPressedExecute,
                CanOnAbortDeleteButtonPressedExecute
                );

            #endregion
        }

        #endregion

        #region Methods

        private bool CanOnAbortDeleteButtonPressedExecute(object p) => true;

        private void OnAbortDeleteButtonPressedExecute(object p)
        {
            this.Action = Enums.Action.None;
        }

        #endregion

    }
}
