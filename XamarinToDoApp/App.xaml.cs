using Plugin.LocalNotification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToDoApp.Persistance;
using XamarinToDoApp.Services;
using XamarinToDoApp.ViewModels;

namespace XamarinToDoApp
{
    public partial class App : Application
    {
        public App()
        {
            #region dependancy injection
            DependencyService.Register<INotificationsService, NotificationService>();
            var toDoItemStore = new SqlLiteToDoItemStore(DependencyService.Get<ISQLiteDb>());
            #endregion

            ViewModel = new ToDoListViewModel(toDoItemStore, DependencyService.Get<INotificationsService>());

            InitializeComponent();


            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public ToDoListViewModel ViewModel
        {
            get { return BindingContext as ToDoListViewModel; }
            set { BindingContext = value; }
        }
    }
}
