using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FinalProject
{
    

    public class FinalProjectDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FinalProjectDatabase(string dbPath)
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.Execute("delete from FinalProjectItem");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<FinalProjectItem>().Wait();

            SaveItemAsync(new FinalProjectItem { Question = "Who was the first female to anchor an evening news broadcast in 1976?", Answer = "Barbara Walters", imageName = "barbarawalters.jpg" });
            SaveItemAsync(new FinalProjectItem { Question = "Whose reporting of the JFK assassination landed him a job at CBS News?", Answer = "Dan Rather", imageName = "danrather.jpg" });
            SaveItemAsync(new FinalProjectItem { Question = "This Canadian-American journalist served as the only anchor of ABC World News Tonight starting 1983", Answer = "Peter Jennings", imageName = "peterjennings.jpg" });
            SaveItemAsync(new FinalProjectItem { Question = "This news anchor was once nicknamed the most trusted man in America?", Answer = "Walter Cronkite", imageName = "waltercronkite.jpg" });
            SaveItemAsync(new FinalProjectItem { Question = "Who became the second female to co-anchor a network newscast as part of CBS Evening News?", Answer = "Connie Chung", imageName = "conniechung.jpg" });
            SaveItemAsync(new FinalProjectItem { Question = "This person reported on human suffering in war zones and natural disasters before anchoring the Today Show", Answer = "Ann Curry", imageName = "anncurry.jpg" });
        }

  

        internal async Task<List<FinalProjectItem>> GetItemsAsync()
        {
            return await database.Table<FinalProjectItem>().ToListAsync();
        }

        internal Task<int> SaveItemAsync(FinalProjectItem item)
        {

            return database.InsertAsync(item);

        }
    }
}
