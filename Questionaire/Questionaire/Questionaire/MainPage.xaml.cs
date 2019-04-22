using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Questionaire
{
    public partial class MainPage : ContentPage
    {
        Questions myList = new Questions();
        int count = 0;
        string answersMessage = "";
        string finalMessage = "";        

        public MainPage()
        {
            InitializeComponent();
            MyImage.Source = ImageSource.FromFile("captamer.png");
            MyImage2.Source = ImageSource.FromFile("supermanLogo.jpg");
            MyImage3.Source = ImageSource.FromFile("hawkeye.jpg");
            Batman.Source = ImageSource.FromFile("batman1.jpg");
            NotAHero.Source = ImageSource.FromFile("fatguy.png");
            Sylar.Source = ImageSource.FromFile("sylar.jpg");
            MyImage2.IsVisible = false;
            MyImage3.IsVisible = false;
            Sylar.IsVisible = false;
            Batman.IsVisible = false;
            NotAHero.IsVisible = false;
            Answer1.IsVisible = false;
            Answer2.IsVisible = false;
            Answer3.IsVisible = false;
            Answer4.IsVisible = false;


            
        }

       

        void OnStartClicked(object sender, EventArgs e)
        {
            Answer1.IsVisible = true;
            Answer2.IsVisible = true;
            Answer3.IsVisible = true;
            Answer4.IsVisible = true;
            count = 0;
            MyImage2.IsVisible = false;
            MyImage3.IsVisible = false;
            Batman.IsVisible = false;
            Sylar.IsVisible = false;
            NotAHero.IsVisible = false;
            Start.IsVisible = false;
            QuestionResult.Text = myList.getQuestionList()[count];
            Answer1.Text = myList.getAnswersForQuestion1()[0];
            Answer2.Text = myList.getAnswersForQuestion1()[1];
            Answer3.Text = myList.getAnswersForQuestion1()[2];
            Answer4.Text = myList.getAnswersForQuestion1()[3];

            count++;
        }


        void OnAnswer1Clicked(object sender, EventArgs e)
        {
            if (count < 5)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
            } else
            {
                QuestionResult.Text = "End of Questions";
            }
            if (count == 1)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion1()[0]);
                Answer1.Text = myList.getAnswersForQuestion2()[0];
                Answer2.Text = myList.getAnswersForQuestion2()[1];
                Answer3.Text = myList.getAnswersForQuestion2()[2];
                Answer4.Text = myList.getAnswersForQuestion2()[3];
                count++;
            }
            else if (count == 2)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion2()[0]);
                Answer1.Text = myList.getAnswersForQuestion3()[0];
                Answer2.Text = myList.getAnswersForQuestion3()[1];
                Answer3.Text = myList.getAnswersForQuestion3()[2];
                Answer4.Text = myList.getAnswersForQuestion3()[3];
                count++;
            }
            else if (count == 3)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion3()[0]);
                Answer1.Text = myList.getAnswersForQuestion4()[0];
                Answer2.Text = myList.getAnswersForQuestion4()[1];
                Answer3.Text = myList.getAnswersForQuestion4()[2];
                Answer4.Text = myList.getAnswersForQuestion4()[3];
                count++;
            }

            else if (count == 4)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion4()[0]);
                Answer1.Text = myList.getAnswersForQuestion5()[0];
                Answer2.Text = myList.getAnswersForQuestion5()[1];
                Answer3.Text = myList.getAnswersForQuestion5()[2];
                Answer4.Text = myList.getAnswersForQuestion5()[3];
                count++;
            }
            else if(count == 5)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion5()[0]);

                finalMessage = myList.displayResults(myList, answersMessage);
                QuestionResult.Text = finalMessage;

                resetVars();

                myList.getUserAnswers().Clear();
            }

        }




        void OnAnswer2Clicked(object sender, EventArgs e)
        {
            if (count < 5)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
            }
            else
            {
                QuestionResult.Text = "End of Questions";
            }
            if (count == 1)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion1()[1]);
                Answer1.Text = myList.getAnswersForQuestion2()[0];
                Answer2.Text = myList.getAnswersForQuestion2()[1];
                Answer3.Text = myList.getAnswersForQuestion2()[2];
                Answer4.Text = myList.getAnswersForQuestion2()[3];
                count++;
            }
            else if (count == 2)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion2()[1]);
                Answer1.Text = myList.getAnswersForQuestion3()[0];
                Answer2.Text = myList.getAnswersForQuestion3()[1];
                Answer3.Text = myList.getAnswersForQuestion3()[2];
                Answer4.Text = myList.getAnswersForQuestion3()[3];
                count++;
            }
            else if (count == 3)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion3()[1]);
                Answer1.Text = myList.getAnswersForQuestion4()[0];
                Answer2.Text = myList.getAnswersForQuestion4()[1];
                Answer3.Text = myList.getAnswersForQuestion4()[2];
                Answer4.Text = myList.getAnswersForQuestion4()[3];
                count++;
            }

            else if (count == 4)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion4()[1]);
                Answer1.Text = myList.getAnswersForQuestion5()[0];
                Answer2.Text = myList.getAnswersForQuestion5()[1];
                Answer3.Text = myList.getAnswersForQuestion5()[2];
                Answer4.Text = myList.getAnswersForQuestion5()[3];
                count++;
            }
            else if (count == 5)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion5()[1]);

                finalMessage = myList.displayResults(myList, answersMessage);
                QuestionResult.Text = finalMessage;

                resetVars();

                myList.getUserAnswers().Clear();
            }
        }

        void OnAnswer3Clicked(object sender, EventArgs e)
        {
            if (count < 5)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
            }
            else
            {
                QuestionResult.Text = "End of Questions";
            }
            if (count == 1)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion1()[2]);
                Answer1.Text = myList.getAnswersForQuestion2()[0];
                Answer2.Text = myList.getAnswersForQuestion2()[1];
                Answer3.Text = myList.getAnswersForQuestion2()[2];
                Answer4.Text = myList.getAnswersForQuestion2()[3];
                count++;
            }
            else if (count == 2)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion2()[2]);
                Answer1.Text = myList.getAnswersForQuestion3()[0];
                Answer2.Text = myList.getAnswersForQuestion3()[1];
                Answer3.Text = myList.getAnswersForQuestion3()[2];
                Answer4.Text = myList.getAnswersForQuestion3()[3];
                count++;
            }
            else if (count == 3)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion3()[2]);
                Answer1.Text = myList.getAnswersForQuestion4()[0];
                Answer2.Text = myList.getAnswersForQuestion4()[1];
                Answer3.Text = myList.getAnswersForQuestion4()[2];
                Answer4.Text = myList.getAnswersForQuestion4()[3];
                count++;
            }

            else if (count == 4)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion4()[2]);
                Answer1.Text = myList.getAnswersForQuestion5()[0];
                Answer2.Text = myList.getAnswersForQuestion5()[1];
                Answer3.Text = myList.getAnswersForQuestion5()[2];
                Answer4.Text = myList.getAnswersForQuestion5()[3];
                count++;
            }
            else if (count == 5)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion5()[2]);

                finalMessage = myList.displayResults(myList, answersMessage);
                QuestionResult.Text = finalMessage;

                resetVars();
                myList.getUserAnswers().Clear();
            }
        }

        void OnAnswer4Clicked(object sender, EventArgs e)
        {
            if (count < 5)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
            }
            else
            {
                QuestionResult.Text = "End of Questions";
            }
            if (count == 1)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion1()[3]);
                Answer1.Text = myList.getAnswersForQuestion2()[0];
                Answer2.Text = myList.getAnswersForQuestion2()[1];
                Answer3.Text = myList.getAnswersForQuestion2()[2];
                Answer4.Text = myList.getAnswersForQuestion2()[3];
                count++;
            }
            else if (count == 2)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion2()[3]);
                Answer1.Text = myList.getAnswersForQuestion3()[0];
                Answer2.Text = myList.getAnswersForQuestion3()[1];
                Answer3.Text = myList.getAnswersForQuestion3()[2];
                Answer4.Text = myList.getAnswersForQuestion3()[3];
                count++;
            }
            else if (count == 3)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion3()[3]);
                Answer1.Text = myList.getAnswersForQuestion4()[0];
                Answer2.Text = myList.getAnswersForQuestion4()[1];
                Answer3.Text = myList.getAnswersForQuestion4()[2];
                Answer4.Text = myList.getAnswersForQuestion4()[3];
                count++;
            }

            else if (count == 4)
            {
                QuestionResult.Text = myList.getQuestionList()[count];
                myList.getUserAnswers().Add(myList.getAnswersForQuestion4()[3]);
                Answer1.Text = myList.getAnswersForQuestion5()[0];
                Answer2.Text = myList.getAnswersForQuestion5()[1];
                Answer3.Text = myList.getAnswersForQuestion5()[2];
                Answer4.Text = myList.getAnswersForQuestion5()[3];
                count++;
            }
            else if (count == 5)
            {
                myList.getUserAnswers().Add(myList.getAnswersForQuestion5()[3]);

                


                finalMessage = myList.displayResults(myList, answersMessage);
                QuestionResult.Text = finalMessage;
                resetVars();

                myList.getUserAnswers().Clear();
            }

        }

        void resetVars()
        {
            Answer1.Text = "";
            Answer2.Text = "";
            Answer3.Text = "";
            Answer4.Text = "";
            
            Start.IsVisible = true;
            Answer1.IsVisible = false;
            Answer2.IsVisible = false;
            Answer3.IsVisible = false;
            Answer4.IsVisible = false;
            //MyImage2.IsVisible = true;

            string msg = QuestionResult.Text;
            //DisplayAlert("Alert", msg, "OK");

            if (QuestionResult.Text.Equals("Hey Superman!!"))
            {
                MyImage2.IsVisible = true;
            } else if (QuestionResult.Text.Equals("You're Hawkeye!!"))
            {
                MyImage3.IsVisible = true;
            } else if (QuestionResult.Text.Equals("You're Sylar!!"))
            {
                Sylar.IsVisible = true;
            } else if (QuestionResult.Text.Equals("You're Batman!!"))
            {
                Batman.IsVisible = true;
            } else if (QuestionResult.Text.Equals("You're no hero, bub!!"))
            {
                NotAHero.IsVisible = true;
            }

            answersMessage = "";
        }










    }
}

