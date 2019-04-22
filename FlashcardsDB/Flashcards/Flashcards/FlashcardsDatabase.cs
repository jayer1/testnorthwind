using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Flashcards
{
    public class FlashcardsDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FlashcardsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<FlashcardsItem>().Wait();

            SaveItemAsync(new FlashcardsItem() { Question = "Batman wages war against criminals to avenge his daughter", Answer = "False", isCorrect = false, imageName = "batman1.jpg" });
            SaveItemAsync(new FlashcardsItem() { Question = "Hawkeye is really Clint Barton from Waverly, Iowa.", Answer = "False", isCorrect = false, imageName = "hawkeye.jpg" });
            SaveItemAsync(new FlashcardsItem() { Question = "Captain America was a frail young man who was biomedically enhanced with shock therapy.", Answer = "False", isCorrect = false, imageName = "captamer.png" });
            SaveItemAsync(new FlashcardsItem() { Question = "Superman was born on Kryponite.", Answer = "False", isCorrect = false, imageName = "supermanLogo.jpg" });
            SaveItemAsync(new FlashcardsItem() { Question = "Sylar can shape-shift.", Answer = "True", isCorrect = false, imageName = "sylar.jpg" });
            SaveItemAsync(new FlashcardsItem() { Question = "Wonder Woman was sculpted from clay and given a life as an Amazon with superhuman powers.", Answer = "True", isCorrect = false, imageName = "ww.png" });
        }

        internal async Task<List<FlashcardsItem>> GetItemsAsync()
        {
            return await database.Table<FlashcardsItem>().ToListAsync();
        }

        /*public Task<List<FlashcardsItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<FlashcardsItem>("SELECT * FROM [FlashcardsItem]");
        }*/

        

        internal Task<int> SaveItemAsync(FlashcardsItem item)
        {
           
                return database.InsertAsync(item);
            
        }

        /*public Task<int> DeleteItemAsync(FlashcardsItem item)
        {
            return database.DeleteAsync(item);
        }*/
    }
}