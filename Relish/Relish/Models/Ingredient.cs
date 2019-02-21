using Relish.Resources;
using SQLite;
using static Relish.Models.Enums;
using static Relish.Utilities.UnitConversions;

namespace Relish.Models
{
    public class Ingredient
    {
        private float _quantity;

        public Ingredient()
        {
        }

        public Ingredient(string name, IngredientCategories category) : this(name, category, -1f, Units.Common)
        {
        }

        public Ingredient(string name, IngredientCategories category, float quantity, Units unit)
        {
            Name = name;
            Category = category;
            Quantity = quantity;
            Unit = unit;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public IngredientCategories Category { get; set; }

        public float Quantity
        {
            get => _quantity;

            set
            {
                _quantity = value;
                QuantityStandardUnit = ConvertToStandardUnit(Unit, value);
            }
        }

        public Units Unit { get; set; }

        public float QuantityStandardUnit { get; private set; }

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

                return $"{_quantity} {ReturnUnitString(Unit)}";
            }
        }

        private string ReturnUnitString(Units unit)
        {
            // TODO add strings for these units
            return unit.ToString();
        }
    }
}
