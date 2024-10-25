using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinToDoApp.Models;

namespace XamarinToDoApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToDoDetailsPage : ContentPage
	{
		public ToDoDetailsPage (ToDoItem toDoItem)
		{
			InitializeComponent ();
			Console.WriteLine($"In the details page for the item {toDoItem.Id}");
		}
	}
}