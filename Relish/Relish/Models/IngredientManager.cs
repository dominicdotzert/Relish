using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Relish.Models
{
    public class IngredientManager
    {
        private readonly SQLiteAsyncConnection _database;

        public IngredientManager()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Ingredients.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<Ingredient>().Wait();

            #region debug

            // TODO remove regeneration of Ingredients list code (eventually)
            // If empty db, load test data
            var list = GetIngredients().Result;
            if (list.Count == 0)
            {
                var ingredients = new List<Ingredient>();
                ingredients.AddRange(MockIngredientList.SampleMeatList);
                ingredients.AddRange(MockIngredientList.SampleDairyList);
                ingredients.AddRange(MockIngredientList.SampleProduceList);

                foreach (var i in ingredients)
                {
                    SaveIngredient(i);
                }
            }

            #endregion
        }

        public event EventHandler DatabaseUpdated;

        public Task<int> SaveIngredient(Ingredient ingredient)
        {
            DatabaseUpdated?.Invoke(ingredient, null);

            if (ingredient.Id != 0)
            {
                return _database.UpdateAsync(ingredient);
            }
            else
            {
                return _database.InsertAsync(ingredient);
            }
        }

        public Task<List<Ingredient>> GetIngredients()
        {
           return _database.Table<Ingredient>().ToListAsync();
        }

        public Task<int> RemoveIngredient(Ingredient ingredient)
        {
            return _database.DeleteAsync(ingredient);
        }
    }
}
