using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToDoApp.ViewModels;

namespace XamarinToDoApp
{
    public partial class MainPage : ContentPage
    {
        private ToDoListViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            // Get the ViewModel from the App class (assuming it's initialized in the App class)
            _viewModel = (Application.Current as App)?.ViewModel;

            // Set the BindingContext of the page to the ViewModel
            BindingContext = _viewModel;
        }

        // This is called when the page is displayed
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Check if ViewModel is initialized
            if (_viewModel != null)
            {
                await _viewModel.LoadData();
            }
        }

    }
}
