using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public class MovieDB
    {
            List <MovieDB> the_movies = new List<MovieDB> {
            new MovieDB() {"A Hard Day's Night (1964)", Rating.G, Genre.MUSICALS },
            new Movie("All Quiet on the Western Front (1930)", Rating.NR, Genre.WAR),
            new Movie("Argo (2012)", Rating.R, Genre.WAR),
            new Movie("Baby Driver (2017)", Rating.R, Genre.ACTION),
            new Movie("Black Panther (2018)", Rating.PG13, Genre.SCIFI),
            new Movie("Captain America: Civil War (2016)", Rating.PG13, Genre.SCIFI),
            new Movie("Casablanca (1942)", Rating.PG, Genre.ROMANCE),
            new Movie("Coco (2017)", Rating.PG, Genre.CHILDCARTOON),
            new Movie("Dr. Strangelove Or How I Learned to Stop Worrying and Love the Bomb (1964)", Rating.PG, Genre.WAR),
            new Movie("Dunkirk (2017)", Rating.R, Genre.WAR),
            new Movie("E.T. The Extra-Terrestrial (1982)", Rating.PG, Genre.SCIFI),
            new Movie("Finding Dory (2016)", Rating.G, Genre.CHILDCARTOON),
            new Movie("Inside Out (2015)", Rating.PG, Genre.CHILDCARTOON),
            new Movie("King Kong (1933)", Rating.NR, Genre.ACTION),
            new Movie("Logan (2017)", Rating.R, Genre.ACTION),
            new Movie("Mad Max: Fury Road (2015)", Rating.R, Genre.ACTION),
            new Movie("Metropolis (1927)", Rating.NR, Genre.SCIFI),
            new Movie("North by Northwest (1959)", Rating.PG, Genre.ACTION),
            new Movie("Pinocchio (1940)", Rating.G, Genre.CHILDCARTOON),
            new Movie("Psycho (1960)", Rating.R, Genre.HORROR),
            new Movie("Rosemary's Baby (1968)", Rating.R, Genre.HORROR),
            new Movie("Seven Samurai (Shichinin no Samurai) (1956)", Rating.NR, Genre.ACTION),
            new Movie("Singin' in the Rain (1952)", Rating.G, Genre.MUSICALS),
            new Movie("Snow White and the Seven Dwarfs (1937)", Rating.G, Genre.CHILDCARTOON),
            new Movie("Spider-Man: Homecoming (2017)", Rating.PG13, Genre.SCIFI),
            new Movie("Star Wars: Episode VII - The Force Awakens (2015)", Rating.PG13, Genre.SCIFI),
            new Movie("Star Wars: The Last Jedi (2017)", Rating.PG13, Genre.SCIFI),
            new Movie("Steve Jobs (2015)", Rating.PG13, Genre.DRAMA),
            new Movie("The Dark Knight (2008)", Rating.PG13, Genre.SCIFI),
            new Movie("The Godfather (1972)", Rating.R, Genre.DRAMA),
            new Movie("The Maltese Falcon (1941)", Rating.NR, Genre.DRAMA),
            new Movie("The Shawshank Redemption (1994)", Rating.PG13, Genre.DRAMA),
            new Movie("The Third Man (1949)", Rating.NR, Genre.DRAMA),
            new Movie("The Wizard of Oz (1939)", Rating.PG, Genre.EPICS),
            new Movie("Thor: Ragnarok (2017)", Rating.PG13, Genre.ACTION),
            new Movie("Wonder Woman (2017)", Rating.PG13, Genre.ACTION)
    };

            List<Movie> movies = new ArrayList<Movie>(Arrays.asList(the_movies));

            List<Movie> getAllMovies()
            {
                return movies;
            }

            List<Movie> getAllMoviesByRating(Rating r)
            {
                List<Movie> moviesByRating = new ArrayList<Movie>();
                foreach (Movie m in movies)
                {
                    if (m.getRating().equals(r))
                    {
                        moviesByRating.add(m);
                    }
                }
                return moviesByRating;
            }

            List<Movie> getAllMoviesByGenre(Genre g)
            {
                List<Movie> moviesByGenre = new ArrayList<Movie>();
                foreach (Movie m in movies)
                {
                    if (m.getGenre().equals(g))
                    {
                        moviesByGenre.add(m);
                    }
                }
                return moviesByGenre;
            }

            String deleteMovieByIndex(int i)
            {
                String message = "";
                if (i >= 0 && i < movies.size())
                {
                    Movie m = movies.get(i);
                    message = m.getName() + " has been removed successfully.";
                    movies.remove(i);
                }
                else
                {
                    message = "The index " + i + " does not refer to a movie in the database.";
                }
                return message;
            }

        }

    }
}
