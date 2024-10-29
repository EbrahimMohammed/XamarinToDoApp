using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToDoApp.Persistance;
using XamarinToDoApp.ViewModels;

namespace XamarinToDoApp
{
    public partial class App : Application
    {
        public App()
        {
            var toDoItemStore = new SqlLiteToDoItemStore(DependencyService.Get<ISQLiteDb>());
            ViewModel = new ToDoListViewModel(toDoItemStore);

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
