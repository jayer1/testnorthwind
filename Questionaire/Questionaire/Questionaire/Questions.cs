using System;
using System.Collections.Generic;
using System.Text;

namespace Questionaire
{
    class Questions
    {
        public List<string> QuestionList = new List<string>() { "What your favorite leisure activity?", "What is your super power?", "How many bad guys' plots have you foiled??", "What is your job?", "Where is your home?" };
        public List<string> AnswersForQuestion1 = new List<string>() { "Flying", "Fighting", "Sleeping", "Scheming" };
        public List<string> AnswersForQuestion2 = new List<string>() { "Strength", "Invisibility", "X-Ray Vision", "Shape-shifting" };
        public List<string> AnswersForQuestion3 = new List<string>() { "<10", "20", "40", ">60" };
        public List<string> AnswersForQuestion4 = new List<string>() { "Reporter", "Detective", "Coder", "Carpenter" };
        public List<string> AnswersForQuestion5 = new List<string>() { "Underground", "In a Tree", "On a Mountain", "In a Cave" };
        public List<string> UserAnswers = new List<string>();
        //public List<string> AvailableAnswers1 = new 

        public List<string> getQuestionList()
        {
            return QuestionList;
        }

        public List<string> getAnswersForQuestion1()
        {
            return AnswersForQuestion1;
        }

        public List<string> getAnswersForQuestion2()
        {
            return AnswersForQuestion2;
        }

        public List<string> getAnswersForQuestion3()
        {
            return AnswersForQuestion3;
        }

        public List<string> getAnswersForQuestion4()
        {
            return AnswersForQuestion4;
        }

        public List<string> getAnswersForQuestion5()
        {
            return AnswersForQuestion5;
        }

        public List<string> getUserAnswers()
        {
            return UserAnswers;
        }

        public List<string> setUserAnswers(List<string> UserAnswers)
        {
            this.UserAnswers = UserAnswers;
            this.UserAnswers.Clear();
            return this.UserAnswers;
        }

        public String displayResults(Questions myList, string answersMessage)
        {
            String finalMessage;
            for (int i = 0; i < myList.getUserAnswers().Count; i++)
            {
                answersMessage += myList.getUserAnswers()[i];
            }

            if (answersMessage.Equals("FlyingInvisibility40DetectiveIn a Tree"))
            {
                finalMessage = "You're Batman!!";
            }
            else if (answersMessage.Equals("FightingX-Ray Vision>60CarpenterIn a Cave"))
            {
                finalMessage = "You're Hawkeye!!";
            }
            else if (answersMessage.Equals("SleepingShape-shifting<10ReporterUnderground"))
            {
                finalMessage = "You're Sylar!!";
            }
            else if (answersMessage.Equals("FlyingX-Ray Vision>60ReporterOn a Mountain"))
            {
                finalMessage = "Hey Superman!!";
            }
            else
            {
                finalMessage = "You're no hero, bub!!";
            }

            return finalMessage;
        }

  
    }
}
