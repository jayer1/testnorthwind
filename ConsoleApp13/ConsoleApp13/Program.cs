using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    class Program
    {
        public static void Main(string[] args)
        {
            MovieDB the_movies = new MovieDB();
            ConsoleUI con = ConsoleUI.getUI();
            bool moreTasks = true;

            do
            {
                Choices choice = con.getChoice();
                switch (choice)
                {
                    case Choices.LIST:
                        con.outputMovies(the_movies.getAllMovies());
                        break;
                    case Choices.BYRATING:
                        con.outputMovies(MovieDB.getAllMoviesByRating(con.getRating()));
                        break;
                    case Choices.BYGENRE:
                        con.outputMovies(MovieDB.getAllMoviesByGenre(con.getGenre()));
                        break;
                    case Choices.DELETE:
                        Console.WriteLine(MovieDB.deleteMovieByIndex(con.getMovieToDelete(MovieDB.getAllMovies())));
                        break;
                    case Choices.EXIT:
                        moreTasks = false;
                    default:

                }
            } while (moreTasks);

        }
    }
}
