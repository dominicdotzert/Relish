using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Relish.Models.Filters
{
    public class IngredientFilter : Filter
    {
        public IngredientFilter(Enums.FilterAttribute filterAttribute, LocalDataManager localDataManager)
            : base(filterAttribute)
        {


            Task.Run(async () => AllIngredients = await localDataManager.GetIngredients());
        }

        public List<Ingredient> AllIngredients { get; private set; }
    }
}
