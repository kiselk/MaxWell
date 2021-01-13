using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using SQLite;


namespace MaxWell.Databases
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            
          // SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
         
            database = new SQLiteAsyncConnection(dbPath);
         //   database.DropTableAsync<Cat>().Wait();
            database.CreateTableAsync<Cat>().Wait();
            database.CreateTableAsync<Vyazka>().Wait();
            database.CreateTableAsync<Pregnancy>().Wait();
            database.CreateTableAsync<Pomet>().Wait();
            database.CreateTableAsync<RemoteImage>().Wait();
            database.CreateTableAsync<Person>().Wait();
            database.CreateTableAsync<Pride>().Wait();
            database.CreateTableAsync<Comment>().Wait();

        }

        public Task<List<Cat>> GetItemsAsync()
        {
            return database.Table<Cat>().ToListAsync();
        }
        public Task<List<Vyazka>> GetVyazkasAsync()
        {
            return database.Table<Vyazka>().ToListAsync();
        }

        public Task<List<Pomet>> GetPometsAsync()
        {
            return database.Table<Pomet>().ToListAsync();
        }

        public Task<List<Pregnancy>> GetPregnanciesAsync()
        {
            return database.Table<Pregnancy>().ToListAsync();
        }

        public Task<List<Pride>> GetPridesAsync()
        {
            return database.Table<Pride>().ToListAsync();
        }
        public Task<List<Person>> GetPersonsAsync()
        {
            return database.Table<Person>().ToListAsync();
        }
        public Task<List<Comment>> GetCommentsAsync()
        {
            return database.Table<Comment>().ToListAsync();
        }
        public Task<List<Cat>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Cat>("SELECT * FROM [Cat] WHERE [Done] = 0");
        }
        public Task<List<Cat>> GetFemalesAsync()
        {
            return database.QueryAsync<Cat>(string.Format("SELECT * FROM [Cat] WHERE [isKitten] = \"{0}\" AND [Gender] = \"{1}\"", "Нет", "Девочка"));
        }
        public Task<List<Cat>> GetMalesAsync()
        {
            return database.QueryAsync<Cat>(string.Format("SELECT * FROM [Cat] WHERE [isKitten] = \"{0}\" AND [Gender] = \"{1}\"", "Нет", "Мальчик"));
        }
        public Task<List<Cat>> GetKittensAsync()
        {
            return database.QueryAsync<Cat>(string.Format("SELECT * FROM [Cat] WHERE [isKitten] = \"{0}\"", "Да"));
        }
        public Task<Cat> GetItemAsync(int id)
        {
            return database.Table<Cat>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        
        public Task<List<Pride>> GetPridesForPersonAsync(int id)
        {
            return database.Table<Pride>().Where(i => i.PersonId == id).ToListAsync();
        }
        
        public Task<List<Cat>> GetKittensForPometAsync(int id)
        {
            return database.Table<Cat>().Where(i => i.PometId == id).ToListAsync();
        }

        public Task<List<Cat>> GetCatsForPersonAsync(int id)
        {
            return database.Table<Cat>().Where(i => i.PersonId == id).Where(i => i.isKitten == "Нет").ToListAsync();
        }

        public Task<List<Cat>> GetFemalesForPersonAsync(int id)
        {
            return database.Table<Cat>().Where(i => i.PersonId == id).Where(i => i.isKitten == "Нет").Where(i => i.Gender == "Девочка").ToListAsync();
        }
        public Task<List<Cat>> GetMalesForPersonAsync(int id)
        {
            return database.Table<Cat>().Where(i => i.PersonId == id).Where(i => i.isKitten == "Нет").Where(i => i.Gender == "Мальчик").ToListAsync();
        }
        public Task<List<Cat>> GetKittensForPersonAsync(int id)
        {
            return database.Table<Cat>().Where(i => i.PersonId == id).Where(i=>i.isKitten=="Да").ToListAsync();
        }


        public Task<List<Comment>> GetCommentsByPersonAsync(int id)
        {
            return database.Table<Comment>().Where(i => i.PersonId == id).ToListAsync();

        }
        public Task<List<Comment>> GetCommentsForPersonAsync(int id)
        {
            return database.Table<Comment>().Where(i => i.ObjectId == id).Where(i=>i.ObjectType==((int)Comment.CommentType.Person)).ToListAsync();
        }

      

        public Task<List<Comment>> GetCommentsForObjectTypeAsync(int id,Comment.CommentType type)
        {
            return database.Table<Comment>().Where(i => i.ObjectId == id).Where(i => i.ObjectType == ((int)type)).ToListAsync();
        }


        public Task<Person> GetPersonAsync(int id)
        {
            return database.Table<Person>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<Person> GetPersonByVKAsync(string VKUserId)
        {
            return database.Table<Person>().Where(i => i.VKUserId == VKUserId).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Cat cat)
        {
            if (cat.Id != 0) { return database.UpdateAsync(cat);}
            else{return database.InsertAsync(cat);}
        }


        public Task<int> SaveItemAsync(Person item)
        {
            if (item.Id != 0) { return database.UpdateAsync(item); }
            else { return database.InsertAsync(item); }
        }
        public Task<int> SaveItemAsync(Pride item)
        {
            if (item.Id != 0) { return database.UpdateAsync(item); }
            else { return database.InsertAsync(item); }
        }
        public Task<int> SaveItemAsync(Comment item)
        {
            if (item.Id != 0) { return database.UpdateAsync(item); }
            else { return database.InsertAsync(item); }
        }
        public Task<int> SaveVyazkaAsync(Vyazka item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> SavePregnancyAsync(Pregnancy item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> SavePometAsync(Pomet item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }



        public Task<int> DeleteItemAsync(Cat cat)
        {
            return database.DeleteAsync(cat);
        }
        public Task<int> DeleteItemAsync(Person Person)
        {
            return database.DeleteAsync(Person);
        }
        public Task<int> DeleteItemAsync(Pride Pride)
        {
            return database.DeleteAsync(Pride);
        }
        public Task<int> DeleteItemAsync(Comment Comment)
        {
            return database.DeleteAsync(Comment);
        }
        public Task<int> DeleteVyazkaAsync(Vyazka Vyazka)
        {
            return database.DeleteAsync(Vyazka);
        }

        public Task<int> DeletePregnancyAsync(Pregnancy Pregnancy)
        {
            return database.DeleteAsync(Pregnancy);
        }
        public Task<int> DeletePometAsync(Pomet Pomet)
        {
            return database.DeleteAsync(Pomet);
        }
        public async Task<List<RemoteImage>> GetAllDownloadedImagesAsync()
        {

            return await database.Table<RemoteImage>().ToListAsync();
        }

        public  async Task<RemoteImage> GetDownloadedImageAsync(string imageUrl)
        {

            return await database.Table<RemoteImage>().Where(x => x.Id.Equals(1)).FirstOrDefaultAsync();
        }
        public async Task<RemoteImage> GetDownloadedImageAsync(int Id)
        {

            return await database.Table<RemoteImage>().Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();
        }


        public async Task<int> GetDownloadedImageCounterAsync()
        {

            return await database.ExecuteAsync("SELECT Id from DownloadedImageModel order by Id DESC limit 1");
           
        }


        public async Task<byte[]> GetDownloadedImageSourceAsync(int Id)
        {
            var image = await database.Table<RemoteImage>().Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();
            return  image.DownloadedImageBlob;
        }
        public async Task SaveDownloadedImage(RemoteImage downloadedImage)
        {

             await database.InsertOrReplaceAsync(downloadedImage);
        }




    }
}

