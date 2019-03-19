﻿using System;
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
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName);
            _database = new SQLiteAsyncConnection(databasePath);

            // Initializes table for Ingredient data.
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

        public Task SaveFilterSettings(FilterData data)
        {
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

        public Task<FilterData> GetFilterSettings()
        {
            return _database.GetWithChildrenAsync<FilterData>(1);
        }

        public Task<List<Recipe>> GetRecipes()
        {
            return _database.GetAllWithChildrenAsync<Recipe>(null, true);
        }

        public Task UpdateRecipe(Recipe recipe)
        {
            if (recipe.IsMealPrepped || recipe.IsSaved)
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