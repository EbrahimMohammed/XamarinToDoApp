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

        public ToDoDetailsPage(ToDoItem item)
        {
            InitializeComponent();
            var toDoItemDetailsStore = new SqlLiteToDoItemDetailsStore(DependencyService.Get<ISQLiteDb>());

            _viewModel = new ToDoDetailsViewModel(toDoItemDetailsStore);
            BindingContext = _viewModel;

            // Call the async initialization method
            InitializeViewModelAsync(item);
        }

        private async void InitializeViewModelAsync(ToDoItem item)
        {
            await _viewModel.InitializeAsync(item);
        }
    }
}