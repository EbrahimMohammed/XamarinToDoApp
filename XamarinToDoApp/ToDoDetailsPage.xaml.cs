using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToDoApp.Models;
using XamarinToDoApp.Persistance;
using XamarinToDoApp.ViewModels;

namespace XamarinToDoApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToDoDetailsPage : ContentPage
	{
        private ToDoDetailsViewModel _viewModel;
        private ToDoItem _item;
        public ToDoDetailsPage(ToDoItem item)
        {
            _item = item;
            InitializeComponent();
            var toDoItemDetailsStore = new SqlLiteToDoItemDetailsStore(DependencyService.Get<ISQLiteDb>());

            _viewModel = new ToDoDetailsViewModel(toDoItemDetailsStore);
            BindingContext = _viewModel;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.InitializeAsync(_item);
        }

        
    }
}