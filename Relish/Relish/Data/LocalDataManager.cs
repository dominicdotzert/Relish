using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Relish.Models;
using Relish.Models.Filters;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Relish.Data
{
    /// <summary>
    /// Class for managing local data stored in a SQLite database.
    /// </summary>
    public class LocalDataManager
    {
        private const string DatabaseName = "LocalData.db3";
        private readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// Initializes LocalDataManager object and opens connection to local database.
        /// </summary>
        public LocalDataManager()
        {
            // Connect to local db3 file.
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName);
            _database = new SQLiteAsyncConnection(databasePath);

            // Initializes tables.
            _database.CreateTableAsync<Ingredient>().Wait();
            _database.CreateTableAsync<FilterData>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();

            #region debug
            // TODO remove regeneration of Ingredients list code (eventually)
            // If empty db, load test data.
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

        /// <summary>
        /// EventHandler for when an ingredient is added to or updated in the Ingredient table.
        /// </summary>
        public event EventHandler IngredientTableUpdated;

        /// <summary>
        /// Task for adding or updating an Ingredient in the Ingredient table.
        /// </summary>
        /// <param name="ingredient">The Ingredient to be saved.</param>
        /// <returns>The ID of the ingredient.</returns>
        public Task<int> SaveIngredient(Ingredient ingredient)
        {
            IngredientTableUpdated?.Invoke(ingredient, null);

            if (ingredient.Id != 0)
            {
                return _database.UpdateAsync(ingredient);
            }
            else
            {
                return _database.InsertAsync(ingredient);
            }
        }

        /// <summary>
        /// Task to get all the ingredients stored in the Ingredient table.
        /// </summary>
        /// <returns>A list of all the saved ingredients.</returns>
        public Task<List<Ingredient>> GetIngredients()
        {
           return _database.Table<Ingredient>().ToListAsync();
        }

        /// <summary>
        /// Task to remove an ingredient from the Ingredient table.
        /// </summary>
        /// <param name="ingredient">The ingredient to be deleted.</param>
        /// <returns>The ID of the ingredient.</returns>
        public Task<int> RemoveIngredient(Ingredient ingredient)
        {
            return _database.DeleteAsync(ingredient);
        }

        /// <summary>
        /// Task to save the current filter settings.
        /// </summary>
        /// <param name="data">The FilterData object which represents the filter settings.</param>
        /// <returns>The SaveFilterSettings task.</returns>
        public Task SaveFilterSettings(FilterData data)
        {
            // Ensure that there is only ever 1 FilterData object saved at a time.
            var countTask = _database.Table<FilterData>().CountAsync();

            if (countTask.Result != 0)
            {
                data.Id = 1;
                return _database.UpdateWithChildrenAsync(data);
            }
            else
            {
                return _database.InsertWithChildrenAsync(data);
            }
        }

        /// <summary>
        /// Task to return the previous filter settings.
        /// </summary>
        /// <returns>Returns the saved FilterData object</returns>
        public Task<FilterData> GetFilterSettings()
        {
            return _database.GetWithChildrenAsync<FilterData>(1);
        }

        /// <summary>
        /// Returns a list of recipes which are saved to the Recipe Book or Meal Prep list.
        /// </summary>
        /// <returns>Returns all the saved recipes.</returns>
        public Task<List<Recipe>> GetRecipes()
        {
            return _database.GetAllWithChildrenAsync<Recipe>(null, true);
        }

        /// <summary>
        /// Updates the saved recipe objects in the database. Main use case is saving and removing
        /// recipes stored locally on the device.
        /// </summary>
        /// <param name="recipe">The recipe item which has changed.</param>
        /// <returns>The UpdateRecipe task.</returns>
        public Task UpdateRecipe(Recipe recipe)
        {
            if (recipe.IsSaved)
            {
                if (recipe.Id != 0)
                {
                    return _database.UpdateWithChildrenAsync(recipe);
                }
                else
                {
                    return _database.InsertWithChildrenAsync(recipe);
                }
            }
            else
            {
                return _database.DeleteAsync(recipe);
            }
        }
    }
}
