using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using VillainSearcher.ViewModels;
using VillainSearcher.Views;
using VillainSearcher.WindowManagers;
using VillianSearcher.BLL.DeviationCalculator;
using VillianSearcher.DAL.Data;
using VillianSearcher.DAL.Repositories;
using VillianSearcher.DAL.RepositoryWrappers;

namespace VillainSearcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider m_serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (m_serviceProvider == null)
                    m_serviceProvider = Init().BuildServiceProvider();

                return m_serviceProvider;
            }
        }

        private static IServiceCollection Init()
        {
            var services = new ServiceCollection();

            #region Do Initial Setup Here
            services.AddSingleton<IDataProvider, JsonDataProvider>();
            services.AddSingleton<IVillainRepository, VillainRepository>();
            services.AddSingleton<IRepositoryWrapper, RepositoryWrapper>();
            
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<VillainDeviationCalculator>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<SearchWindowViewModel>();

            services.AddSingleton(c =>
            {
                var vm = c.GetRequiredService<MainWindowViewModel>();
                var mainwindow = new MainWindow();

                mainwindow.DataContext = vm;
                vm.Dispatcher = mainwindow.Dispatcher;

                return mainwindow;
            });

            services.AddTransient(c =>
            {
                var scope = c.CreateScope();

                var repWindow = new SearchWindow();
                var vm = scope.ServiceProvider.GetRequiredService<SearchWindowViewModel>();
                repWindow.DataContext = vm;
                vm.Dispatcher = repWindow.Dispatcher;
                repWindow.Closed += (object sender, EventArgs e) =>
                {
                    scope.Dispose();
                };

                return repWindow;
            });

            //Mapper configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                var assembly = Assembly.GetExecutingAssembly();

                var profiles = assembly.DefinedTypes.Where(t => t.BaseType != null && t.BaseType.Name.Equals(nameof(Profile))).Select(t => t);

                foreach (var p in profiles)
                {
                    mc.AddProfile((Profile)Activator.CreateInstance(p));
                }
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var windowManager = ServiceProvider.GetRequiredService<IWindowManager>();

            windowManager.OpenWindow(typeof(MainWindow));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            IRepositoryWrapper repositoryWrapper = ServiceProvider.GetRequiredService<IRepositoryWrapper>();

            repositoryWrapper.SaveData();

            base.OnExit(e);
        }
    }
}
