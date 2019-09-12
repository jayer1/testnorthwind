using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page2 : ContentPage
	{

        List<FinalProjectItem> myList = new List<FinalProjectItem>();
        FinalProjectDatabase Database = App.Database;
        ObservableCollection<Questioning> questions = new ObservableCollection<Questioning>();
        public Page2 ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            myList = await Database.GetItemsAsync();

            for (int i = 0; i <myList.Count; i++)
            {
                questions.Add(new Questioning { DisplayImage = (myList[i]).imageName, DisplayQuestion = (myList[i]).Question, DisplayAnswer = (myList[i]).Answer });
            }
            //questionAnswer.Text = first.Question;
            //MyImage.Source = first.imageName;
            QuestionView.ItemsSource = questions;
        }


        public async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            Questioning details = e.Item as Questioning;
            await DisplayAlert("The correct answer is...", details.DisplayAnswer, "OK");
        }




    }
}