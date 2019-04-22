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
        Card first = new Card("Batman wages war against criminals to avenge his daughter", "False", "batman1.jpg");
        Card second = new Card("Hawkeye is really Clint Barton from Waverly, Iowa.", "True", "hawkeye.jpg");
        Card third = new Card("Captain America was a frail young man who was biomedically enhanced with shock therapy.", "False", "captamer.png");
        Card fourth = new Card("Superman was born on Kryponite.", "False", "supermanLogo.jpg");
        Card fifth = new Card("Sylar can shape-shift.", "True", "sylar.jpg");
        Card sixth = new Card("Wonder Woman was sculpted from clay and given a life as an Amazon with superhuman powers.", "True", "ww.png");
        ArrayList myList = new ArrayList();
        int i = 0;
        int countCorrect = 0;
        

        public MainPage()
        {
            InitializeComponent();

            myList.Add(first);
            myList.Add(second);
            myList.Add(third);
            myList.Add(fourth);
            myList.Add(fifth);
            myList.Add(sixth);

            questionAnswer.Text = ((Card)myList[0]).Question;
            MyImage.Source = ((Card)myList[0]).imageName;
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
                    

                    if (((Card)myList[i]).Answer == "True")
                    {
                        ((Card)myList[i]).isCorrect = true;
                        questionAnswer.Text = "You are correct!";
                    } else
                    {
                        ((Card)myList[i]).isCorrect = false;
                        questionAnswer.Text = "You are incorrect.";
                        string msg = "This statement ( " + ((Card)myList[i]).Question + ") is " + ((Card)myList[i]).Answer;
                        DisplayAlert("Correction:", msg, "ok");
                    }
                    break;
            
                case SwipeDirection.Right:
                    //DisplayAlert("Swipe", "You swiped right", "ok");

                    if (((Card)myList[i]).Answer == "False")
                    {
                        ((Card)myList[i]).isCorrect = true;
                        questionAnswer.Text = "You are correct!";
                    }
                    else
                    {
                        ((Card)myList[i]).isCorrect = false;
                        questionAnswer.Text = "You are incorrect.";
                        string msg = "This statement (" + ((Card)myList[i]).Question + ") is " + ((Card)myList[i]).Answer;
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
                questionAnswer.Text = ((Card)myList[i]).Question;
                MyImage.Source = ((Card)myList[i]).imageName;
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
                    if(((Card)myList[item]).isCorrect == true)
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
            questionAnswer.Text = first.Question;
            MyImage.Source = first.imageName;
            newGame.IsVisible = false;
            MyImage.IsVisible = true;
            directions.IsVisible = true;

            countTries ++;

        }
    }
}
