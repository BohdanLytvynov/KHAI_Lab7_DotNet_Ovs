using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using VillainSearcher.ViewModels.Base.Commands;
using VillainSearcher.ViewModels.Base.VM;
using VillainSearcher.WindowManagers;
using VillianSearcher.DAL.RepositoryWrappers;
using VillainSearcher.ViewModels.Model;
using VillianSearcher.DAL.Models;
using VillainSearcher.Enums;
using Action = VillainSearcher.Enums.Action;

namespace VillainSearcher.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private string m_title;

        IRepositoryWrapper m_repWrapper;

        IWindowManager m_windowManager;

        IMapper m_mapper;

        private ObservableCollection<VillainViewModel> m_Villains;

        private int m_selectedVillainIndex;

        #endregion

        #region Properties

        public string Title
        {
            get => m_title;
            set => Set(ref m_title, value);
        }

        public ObservableCollection<VillainViewModel> Villains
        {
            get => m_Villains;
            set => m_Villains = value;
        }

        public int SelectedVillainIndex
        {
            get => m_selectedVillainIndex;
            set => Set(ref m_selectedVillainIndex, value);
        }

        #endregion

        #region Commands
        public ICommand OnLoadDataButtonPressed { get; }

        public ICommand OnSaveButtonPressed { get; }

        public ICommand OnAddButtonPressed { get; }

        public ICommand OnEditButtonPressed { get; }

        public ICommand OnRemoveButtonPressed { get; }

        public ICommand OnGenerateReportPressed { get; }

        public ICommand OnAboutButtonPressed { get; }
        #endregion

        #region Ctor

        public MainWindowViewModel(IRepositoryWrapper repositoryWrapper,
            IMapper mapper, IWindowManager windowManager) : this()
        {
            m_repWrapper = repositoryWrapper;

            m_mapper = mapper;

            m_windowManager = windowManager;

            var votersData = m_repWrapper.VoterRepository.GetAll();

            m_Villains = new ObservableCollection<VillainViewModel>();

            UpdateDataFromDataBase(votersData, Villains, mapper);
        }

        public MainWindowViewModel() : base(0)
        {
            #region Init Fields
            m_title = "Villain Searcher App V1";

            Villains = new ObservableCollection<VillainViewModel>();

            m_selectedVillainIndex = -1;

            #endregion 

            #region Init Commands
            OnLoadDataButtonPressed = new Command(
                OnLoadDataButtonPressedExecute,
                CanOnLoadDataButtonPressedExecute);

            OnSaveButtonPressed = new Command(
                OnSaveButtonPressedExecute,
                CanOnSaveButtonPressedExecute
                );

            OnAddButtonPressed = new Command(
                OnAddButtonPressedExecute,
                CanOnAddButtonPressedExecute);

            OnEditButtonPressed = new Command(
                OnEditButtonButtonPressedExecute,
                CanOnEditButtonPressedExecute);

            OnRemoveButtonPressed = new Command(
                OnRemoveButtonPressedExecute,
                CanOnRemoveButtonPressedExecute);

            OnGenerateReportPressed = new Command(
                OnGenerateReportButtonPressedExecute,
                CanOnGenerateReportButtonPressedExecute
                );

            OnAboutButtonPressed = new Command(
                OnAboutButtonPressedExecute,
                CanOnAboutButtonPressedExecute
                );
            #endregion
        }

        #endregion

        #region Methods

        #region On Load Data Button Pressed
        private bool CanOnLoadDataButtonPressedExecute(object p) => true;

        private void OnLoadDataButtonPressedExecute(object p)
        {
            m_repWrapper.LoadData();

            var voters = m_repWrapper.VoterRepository.GetAll();

            if (Villains.Count > 0)
                Villains.Clear();

            UpdateDataFromDataBase(voters, Villains, m_mapper);
        }
        #endregion

        #region On Save Button Pressed 

        private bool CanOnSaveButtonPressedExecute(object p) => Villains.Count > 0 && AllRecordsAreValid(Villains);

        private void OnSaveButtonPressedExecute(object p)
        {
            var repo = m_repWrapper.VoterRepository;

            foreach (VillainViewModel v in Villains)
            {
                switch (v.Action)
                {
                    case Action.Add:
                        repo.Add(m_mapper.Map<Villain>(v));
                        v.Action = Action.None;
                        break;
                    case Action.Edit:
                        repo.Edit(v.Id, m_mapper.Map<Villain>(v));
                        v.Action = Action.None;
                        break;
                    case Action.Delete:
                        repo.Remove(v.Id);
                        Villains.Remove(v);
                        break;
                }
            }

            repo.SaveData();
        }

        #endregion

        #region On Add Button Pressed

        private bool CanOnAddButtonPressedExecute(object p) => true;

        private void OnAddButtonPressedExecute(object p)
        {
            Villains.Add(new VillainViewModel() { Action = Action.Add });

            UpdateVillainsShowNumbers(Villains);
        }

        #endregion

        #region On Edit Button Pressed

        private bool CanOnEditButtonPressedExecute(object p)
        {
            return SelectedVillainIndex >= 0;
        }

        private void OnEditButtonButtonPressedExecute(object p)
        {
            Villains[SelectedVillainIndex].Action = Action.Edit;
        }

        #endregion

        #region On Remove Button Pressed

        private bool CanOnRemoveButtonPressedExecute(object p)
        {
            return SelectedVillainIndex >= 0 && Villains[SelectedVillainIndex].Action != Action.Add;
        }

        private void OnRemoveButtonPressedExecute(object p)
        {
            Villains[SelectedVillainIndex].Action = Action.Delete;
        }

        #endregion

        #region On Generate Report Button Pressed

        private bool CanOnGenerateReportButtonPressedExecute(object p) => Villains.Count > 0;

        private void OnGenerateReportButtonPressedExecute(object p)
        {
            
        }

        #endregion

        #region On About Button Pressed

        private bool CanOnAboutButtonPressedExecute(object p) => true;

        private void OnAboutButtonPressedExecute(object p)
        {
            MessageBox.Show("Литивнов Богда Юрійович Гр 125 Варіант 9\n" +
                "Клас Злочинець із член - даними: прізвище, зріст, ширина і висота голови, \n" +
                "довжина руки, відстань між очима.Розробити метод обчислення близькості заданих \n" +
                "параметрів підозрюваного злочинця як суму відсотків відхилення фізичних параметрів.\n" +
                "За заданими фізичними параметрами підозрюваного знайти у базі N найближчих \n"+ 
                "а параметрами злочинців.Значення N запитати у користувача \n",
                Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        private static void UpdateDataFromDataBase(IEnumerable<Villain> src, 
            ObservableCollection<VillainViewModel> dest,
            IMapper mapper)
        {
            int i = 1;
            foreach (var voter in src)
            {
                var vm = mapper.Map<VillainViewModel>(voter);
                vm.Action = Action.None;
                vm.IsValid = true;
                vm.ShowNumber = i;
                dest.Add(vm);
                i++;
            }
        }

        private static void UpdateVillainsShowNumbers(ObservableCollection<VillainViewModel> voters)
        {
            int length = voters.Count;

            for (int i = 0; i < length; i++)
            {
                voters[i].ShowNumber = i + 1;
            }
        }

        private static bool AllRecordsAreValid(ObservableCollection<VillainViewModel> voters)
        {
            foreach (var voter in voters)
            {
                if (!voter.IsValid)
                    return false;
            }

            return true;
        }

        #endregion
    }
}
