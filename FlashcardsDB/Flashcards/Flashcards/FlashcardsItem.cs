namespace Flashcards
{
    class FlashcardsItem
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool isCorrect { get; set; }
        public string imageName { get; set; }

        /*
        public FlashcardsItem(string Question, string Answer, bool isCorrect, string imageName)
        {
            this.Question = Question;
            this.Answer = Answer;
            this.isCorrect = isCorrect;
            this.imageName = imageName;
        }*/
    }
}