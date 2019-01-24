using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    class Movie
    {
        private String name;
        private Rating rating;
        private Genre genre;

        public object Objects { get; private set; }

        public Movie(String name, Rating rating, Genre genre)
        {
            this.name = name;
            this.rating = rating;
            this.genre = genre;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public Rating getRating()
        {
            return rating;
        }

        public void setRating(Rating rating)
        {
            this.rating = rating;
        }

        public Genre getGenre()
        {
            return genre;
        }

        public void setGenre(Genre genre)
        {
            this.genre = genre;
        }


    public override Boolean Equals(Object o)
        {
            if (this == o) {
                return true;
            }
            if (!instanceOf(o)) {
                return false;
            }
            Movie movie = (Movie)o;
            return Objects.Equals(getName(), movie.getName()) &&
                    getRating() == movie.getRating() &&
                    getGenre() == movie.getGenre();
        }

        private bool instanceOf(object o)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return Objects.GetHashCode(getName(), getRating(), getGenre());
        }

      
        public override string ToString()
        {
            return "Movie{" +
                    "name='" + name + '\'' +
                    ", rating=" + rating +
                    ", genre=" + genre +
                    '}';
        }

    }
}
