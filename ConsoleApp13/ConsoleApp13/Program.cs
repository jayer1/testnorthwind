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
            MovieDB movieDB = new MovieDB();
            ConsoleUI con = ConsoleUI.getUI();
            boolean moreTasks = true;

            do
            {
                Choices choice = con.getChoice();
                switch (choice)
                {
                    case "LIST":
                        con.outputMovies(movieDB.getAllMovies());
                        break;
                    case "BYRATING":
                        con.outputMovies(movieDB.getAllMoviesByRating(con.getRating()));
                        break;
                    case "BYGENRE":
                        con.outputMovies(movieDB.getAllMoviesByGenre(con.getGenre()));
                        break;
                    case "DELETE":
                        Console.WriteLine(movieDB.deleteMovieByIndex(con.getMovieToDelete(movieDB.getAllMovies())));
                        break;
                    case "EXIT":
                        moreTasks = false;
                }
            } while (moreTasks);

        }
    }
}
