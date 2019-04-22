using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards
{
    class Card
    {
        public string Question{get; set; }
        public string Answer { get; set; }
        public bool isCorrect { get; set; }
        public string imageName { get; set; }

        public Card(string Question, string Answer, string imageName)
        {
            this.Question = Question;
            this.Answer = Answer;
            this.imageName = imageName;
        }

        public string getQuestion()
        {
            return this.Question;
        }

       
    }
}
