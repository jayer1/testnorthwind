using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public class ConsoleUI
    {
        private static ConsoleUI UI = new ConsoleUI();
        private ConsoleUI() { }
        public static ConsoleUI getUI()
        {
            return UI;
        }

        public void outputMovies(List<Movie> m)
        {
            for (int i = 0; i < m.Count(); i++)
            {
                Console.WriteLine(i + ":" + m.get(i));
            }
        }

        public Choices getChoice()
        {
            bool moreChoices = true;
            String input = null;
            do
            {
                Console.Write("'1' List Movies\n'2' Select Rating\n'3' Select Genre\n'4' Delete\n'5' Exit\nPlease enter choice: ");
                input = Console.ReadLine();
                switch (input.Trim())
                {
                    case "1":
                        return Choices.LIST;
                    case "2":
                        return Choices.BYRATING;
                    case "3":
                        return Choices.BYGENRE;
                    case "4":
                        return Choices.DELETE;
                    case "5":
                        moreChoices = false;
                        return Choices.EXIT;
                    default:
                        Console.WriteLine("Please enter a number between 1 and 5.");
                }
            } while (moreChoices);
            return null;
        }

        public int getMovieToDelete(List<Movie> m)
        {
            bool notANumber = true;
            String input = null;
            int currentNumber = 0;
            outputMovies(m);
            do
            {
                Console.WriteLine("Please enter number to Delete: ");
                input = Console.ReadLine();
                try
                {
                    if (input != null && input.Trim().Length > 0)
                    {
                        currentNumber = Int32.Parse(input);
                        notANumber = false;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Not a valid Number.");
                }
            } while (notANumber);
            return currentNumber;
        }

        public Rating getRating()
        {
            String input = null;
            do
            {
                Console.WriteLine("'1' G\n'2' PG\n'3' PG-13\n'4' R\n'5' NR\nPlease enter rating: ");
                input = Console.ReadLine();
                switch (input.Trim())
                {
                    case "1": return Rating.G;
                    case "2": return Rating.PG;
                    case "3": return Rating.PG13;
                    case "4": return Rating.R;
                    case "5": return Rating.NR;
                    default:
                        Console.WriteLine("Please enter a number between 1 and 5.");
                }
            } while (true);
        }

        public Genre getGenre()
        {
            String input = null;
            do
            {
                Console.WriteLine("'1' Action\n'2' Child's Cartoon\n'3' Comedy\n'4' Drama\n'5' Epics\n'6' Horror\n'7' Musicals\n'8' Romance\n'9' SciFi\n'10' War\nPlease enter Genre : ");
                input = Console.ReadLine();
                switch (input.Trim())
                {
                    case "1": return Genre.ACTION; break;
                    case "2": return Genre.CHILDCARTOON; break;
                    case "3": return Genre.COMEDY; break;
                    case "4": return Genre.DRAMA; break;
                    case "5": return Genre.EPICS; break;
                    case "6": return Genre.HORROR; break;
                    case "7": return Genre.MUSICALS; break;
                    case "8": return Genre.ROMANCE; break;
                    case "9": return Genre.SCIFI; break;
                    case "10": return Genre.WAR; break;
                    default:
                        Console.WriteLine("Please enter a number between 1 and 10.");
                }
            } while (true);
        }
    }
}
