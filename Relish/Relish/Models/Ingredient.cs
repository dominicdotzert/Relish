using Relish.Resources;
using SQLite;
using static Relish.Models.Enums;
using static Relish.Utilities.UnitConversions;

namespace Relish.Models
{
    /// <summary>
    /// Model class to represent an individual ingredient.
    /// </summary>
    public class Ingredient
    {
        private Units _unit;
        private float _quantity;

        /// <summary>
        /// Parameterless constructor is needed to initialize an Ingredient table in the local SQLite database.
        /// </summary>
        public Ingredient()
        {
        }

        /// <summary>
        /// Initializes an ingredient object of the Common Unit type.
        /// </summary>
        /// <param name="name">The name of the ingredient.</param>
        /// <param name="category">The category of the ingredient.</param>
        public Ingredient(string name, IngredientCategories category) : this(name, category, -1f, Units.Common)
        {
        }

        /// <summary>
        /// Initializes an ingredient object.
        /// </summary>
        /// <param name="name">The name of the ingredient.</param>
        /// <param name="category">The category of the ingredient.</param>
        /// <param name="quantity">The quantity of the ingredient (in specified unit).</param>
        /// <param name="unit">The unit in which the ingredient is measured.</param>
        public Ingredient(string name, IngredientCategories category, float quantity, Units unit)
        {
            Name = name;
            Category = category;
            Quantity = quantity;
            Unit = unit;
        }

        /// <summary>
        /// The unique ID of the ingredient (for storing in the SQLite DB).
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The name of the ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The category of the ingredient.
        /// </summary>
        public IngredientCategories Category { get; set; }

        /// <summary>
        /// The quantity of the ingredient (in the user specified unit).
        /// </summary>
        public float Quantity
        {
            get => _quantity;

            set
            {
                _quantity = value;
                QuantityStandardUnit = ConvertToStandardUnit(Unit, value);
            }
        }

        /// <summary>
        /// The unit in which the ingredient is measured.
        /// </summary>
        public Units Unit
        {
            get => _unit;

            set
            {
                _unit = value;
                StandardUnit = GetStandardUnit(value);
            }
        }

        public Units StandardUnit { get; private set; }

        /// <summary>
        /// The quantity of the ingredient in its standard unit.
        /// Ex: Tbsp will be converted to mL.
        /// </summary>
        public float QuantityStandardUnit { get; private set; }

        /// <summary>
        /// Formatted string for displaying the unit and quantity.
        /// </summary>
        public string UnitDisplayName
        {
            get
            {
                if (Unit == Units.Quantity)
                {
                    return $"{Strings.Ingredients_Quantity}: {_quantity}";
                }

                if (Unit == Units.Common)
                {
                    return string.Empty;
                }

                return $"{_quantity} {Unit.ToString()}";
            }
        }
    }
}
