using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Flashcards
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;
        //int swipeCount = 0;
        int countTries = 0;
       
        ArrayList myList = new ArrayList();
        int i = 0;
        int countCorrect = 0;
        FlashcardsDatabase Database = App.Database;


        public MainPage()
        {
            InitializeComponent();
            //myList = (FlashcardsItem)Database.GetItemsAsync();
            myList = await Database.GetItemsAsync();



            questionAnswer.Text = ((FlashcardsItem)myList[0]).Question;
            MyImage.Source = ((FlashcardsItem)myList[0]).imageName;
            //questionAnswer.Text = first.Question;
            //MyImage.Source = first.imageName;
            newGame.IsVisible = false;
            MyImage.IsVisible = true;
            nextQuestion.IsVisible = false;
            directions.IsVisible = true;
            countTries = 1;
            

        }

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            directions.IsVisible = false;
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    

                    if (((FlashcardsItem)myList[i]).Answer == "True")
                    {
                        ((FlashcardsItem)myList[i]).isCorrect = true;
                        questionAnswer.Text = "You are correct!";
                    } else
                    {
                        ((FlashcardsItem)myList[i]).isCorrect = false;
                        questionAnswer.Text = "You are incorrect.";
                        string msg = "This statement ( " + ((FlashcardsItem)myList[i]).Question + ") is " + ((FlashcardsItem)myList[i]).Answer;
                        DisplayAlert("Correction:", msg, "ok");
                    }
                    break;
            
                case SwipeDirection.Right:
                    //DisplayAlert("Swipe", "You swiped right", "ok");

                    if (((FlashcardsItem)myList[i]).Answer == "False")
                    {
                        ((FlashcardsItem)myList[i]).isCorrect = true;
                        questionAnswer.Text = "You are correct!";
                    }
                    else
                    {
                        ((FlashcardsItem)myList[i]).isCorrect = false;
                        questionAnswer.Text = "You are incorrect.";
                        string msg = "This statement (" + ((FlashcardsItem)myList[i]).Question + ") is " + ((FlashcardsItem)myList[i]).Answer;
                        DisplayAlert("Correction:", msg, "ok");
                    }

                    break;
            }
            //swipeCount++;
            nextQuestion.IsVisible = true;

        }

        void OnNextQuestionClicked(object sender, SwipedEventArgs e)
        {
            nextQuestion.IsVisible = false;

            if (i < myList.Count - 1)
            {
                i++;
                questionAnswer.Text = ((FlashcardsItem)myList[i]).Question;
                MyImage.Source = ((FlashcardsItem)myList[i]).imageName;
            }
            else
            {
                questionAnswer.TextColor = Color.White;
                layout1.BackgroundColor = Color.Black;
                newGame.IsVisible = true;
                nextQuestion.IsVisible = false;
                MyImage.IsVisible = false;
            
                countCorrect = 0;
                for(int item= 0; item<myList.Count; item++)
                {
                    if(((FlashcardsItem)myList[item]).isCorrect == true)
                    {
                        countCorrect++;
                    }
                }

                questionAnswer.Text = "You had " + countCorrect + " correct out of " + myList.Count + " questions.";
                if (countCorrect == myList.Count)
                {
                    if (countTries == 1)
                    {
                        questionAnswer.Text += "\nGood job! \nIt took you " + countTries + " try to get them all correct.";
                    }
                    else
                    {
                        questionAnswer.Text += "\nGood job! \nIt took you " + countTries + " tries to get them all correct.";
                    }
                    countTries = 0;
                }
                else
                {
                    questionAnswer.Text += "\nTry again.";
                }
                i = 0;
            }
            

        }
        void OnNewGameClicked(object sender, SwipedEventArgs e)
        {
            layout1.BackgroundColor = Color.White;
            questionAnswer.TextColor = Color.DarkBlue;
            questionAnswer.Text = ((FlashcardsItem)myList[0]).Question;
            MyImage.Source = ((FlashcardsItem)myList[0]).imageName;
            newGame.IsVisible = false;
            MyImage.IsVisible = true;
            directions.IsVisible = true;

            countTries ++;

        }
    }
}
