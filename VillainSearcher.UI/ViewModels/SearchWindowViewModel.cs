using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VillainSearcher.ViewModels.Base.Commands;
using VillainSearcher.ViewModels.Base.VM;
using VillainSearcher.ViewModels.Model;
using VillianSearcher.BLL.DeviationCalculator;
using VillianSearcher.BLL.Validators;
using VillianSearcher.DAL.Models;
using VillianSearcher.DAL.RepositoryWrappers;

namespace VillainSearcher.ViewModels
{
    internal class SearchWindowViewModel : ViewModelBase
    {
        #region Fields

        IMapper m_mapper;
        VillainDeviationCalculator m_villainDeviationCalculator;
        IRepositoryWrapper m_repositoryWrapper;

        private string m_title;

        private ObservableCollection<VillainViewModel> m_SearchResult;

        private int m_SelectedVillainIndex;
        
        private string m_Height;
        private string m_HeadWidth;
        private string m_HeadHeight;
        private string m_ArmLength;
        private string m_EyeDistance;

        private double m_Threshold;

        private bool m_InputValid;

        private Task m_SearchTask;

        private bool m_searching;
        #endregion

        #region Properties
        public string Title
        {
            get => m_title;
            set => Set(ref m_title, value);
        }

        public ObservableCollection<VillainViewModel> SearchResult
        {
            get => m_SearchResult;
            set => Set(ref m_SearchResult, value);
        }

        public int SelectedVillainIndex
        {
            get => m_SelectedVillainIndex;
            set => Set(ref m_SelectedVillainIndex, value);
        }
        
        public string Height
        {
            get => m_Height;
            set => Set(ref m_Height, value);
        }

        public string HeadWidth
        {
            get => m_HeadWidth;
            set => Set(ref m_HeadWidth, value);
        }

        public string HeadHeight
        {
            get => m_HeadHeight;
            set => Set(ref m_HeadHeight, value);
        }

        public string ArmLength
        {
            get => m_ArmLength;
            set => Set(ref m_ArmLength, value);
        }

        public string EyeDistance
        {
            get => m_EyeDistance;
            set => Set(ref m_EyeDistance, value);
        }

        public double Threshold
        {
            get => m_Threshold;
            set 
            {
                Set(ref m_Threshold, value);
            }
        }

        #endregion

        #region Commands

        public ICommand OnSearchButtonPressed { get; }

        #endregion

        #region IData Error

        public override string this[string columnName]
        {
            get 
            {
                string error = string.Empty;

                switch (columnName)
                {                   
                    case nameof(Height):
                        SetValidArrayValue(0, ValidationHelper.ValidateNumber(Height, out error));
                        break;
                    case nameof(HeadWidth):
                        SetValidArrayValue(1, ValidationHelper.ValidateNumber(HeadWidth, out error));
                        break;
                    case nameof(HeadHeight):
                        SetValidArrayValue(2, ValidationHelper.ValidateNumber(HeadHeight, out error));
                        break;
                    case nameof(ArmLength):
                        SetValidArrayValue(3, ValidationHelper.ValidateNumber(ArmLength, out error));
                        break;
                    case nameof(EyeDistance):
                        SetValidArrayValue(4, ValidationHelper.ValidateNumber(EyeDistance, out error));
                        break;
                }

                m_InputValid = ValidateFields(0, GetValidArrayCount() - 1);

                return error;
            }
        }

        #endregion

        #region Ctor
        public SearchWindowViewModel(IMapper mapper,
            VillainDeviationCalculator villainDevCalc,
            IRepositoryWrapper repositoryWrapper) : this()
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (villainDevCalc == null) throw new ArgumentNullException(nameof(villainDevCalc));
            if (repositoryWrapper == null) throw new ArgumentNullException(nameof(repositoryWrapper));

            m_mapper = mapper;
            m_villainDeviationCalculator = villainDevCalc;
            m_repositoryWrapper = repositoryWrapper;
        }

        public SearchWindowViewModel() : base(5)
        {
            #region Init fields
            m_title = "Villain Searcher Search";

            m_SearchResult = new ObservableCollection<VillainViewModel>();

            m_SelectedVillainIndex = -1;

            m_Threshold = 0;

            m_InputValid = false;

            m_SearchTask = new Task(DoSearch);

            m_searching = false;
            
            m_Height = string.Empty;
            m_HeadWidth = string.Empty;
            m_HeadHeight = string.Empty;
            m_ArmLength = string.Empty;
            m_EyeDistance = string.Empty;
            #endregion

            #region Init Commands
            OnSearchButtonPressed = new Command(
                OnSearchButtonPressedExecute,
                CanOnSearchButtonPressedExecute
                );
            #endregion
        }
        #endregion

        #region Methods

        #region On Search Button Pressed

        private bool CanOnSearchButtonPressedExecute(object p) => m_InputValid && !m_searching;

        private void OnSearchButtonPressedExecute(object p)
        {
            m_SearchTask.Start();
        }

        #endregion

        private void DoSearch()
        {
            if(m_searching)
                return;

            m_searching = true;

            var repo = m_repositoryWrapper.VillainRepository;

            var villains = repo.GetAll();

            VillainViewModel villainViewModel = new VillainViewModel(-1, "",
                Height, HeadWidth, HeadHeight, ArmLength, EyeDistance);

            var villainRef = m_mapper.Map<Villain>(villainViewModel);

            if (SearchResult.Count > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    SearchResult.Clear();
                });                
            }
            
            foreach (var villain in villains)
            {
                if (m_villainDeviationCalculator.GetDeviationPercentage(villainRef, villain) <= Threshold)
                {
                    Dispatcher.Invoke(() =>
                    {
                        SearchResult.Add(m_mapper.Map<VillainViewModel>(villain));
                    });
                }
            }

            m_searching = false;
        }

        #endregion
    }
}
