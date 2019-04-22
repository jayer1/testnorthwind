using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardList
{
    public partial class MainPage : ContentPage
    {
        Card first = new Card("Batman wages war against criminals to avenge his daughter", "False", "batman1.jpg");
        Card second = new Card("Hawkeye is really Clint Barton from Waverly, Iowa.", "True", "hawkeye.jpg");
        Card third = new Card("Captain America was a frail young man who was biomedically enhanced with shock therapy.", "False", "captamer.png");
        Card fourth = new Card("Superman was born on Kryponite.", "False", "supermanLogo.jpg");
        Card fifth = new Card("Sylar can shape-shift.", "True", "sylar.jpg");
        Card sixth = new Card("Wonder Woman was sculpted from clay and given a life as an Amazon with superhuman powers.", "True", "ww.png");
        ArrayList myList = new ArrayList();
        ObservableCollection<Questioner> flashcards = new ObservableCollection<Questioner>();

        //ArrayList myList = new ArrayList();

        int i = 0;

        public MainPage()
        {
            myList.Add(first);
            myList.Add(second);
            myList.Add(third);
            myList.Add(fourth);
            myList.Add(fifth);
            myList.Add(sixth);
            InitializeComponent();
            
            for (i = 0; i < myList.Count; i++)
            {


                flashcards.Add(new Questioner { DisplayImage = ((Card)myList[i]).imageName, DisplayQuestion = ((Card)myList[i]).Question, DisplayAnswer = ((Card)myList[i]).Answer});
            }
            
            QuestionView.ItemsSource = flashcards;
            ObservableCollection<string> heroes = new ObservableCollection<string>();
            heroes.Add("Batman");
            heroes.Add("Hawkeye");
            heroes.Add("Captain America");
            heroes.Add("Superman");
            heroes.Add("Skylar");
            heroes.Add("Wonder Woman");

            //SelectionView.ItemsSource = heroes;
        }

        public async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            Questioner details = e.Item as Questioner;
            //var mi = ((MenuItem)sender).BindingContext;
            await DisplayAlert("The correct answer is...", details.DisplayAnswer, "OK");
            //await DisplayAlert("Tapped", details.DisplayAnswer.ToString(), "OK");
            //await DisplayAlert("More Context Action", "The correct answer is* ", "OK");
        }


    }
}
