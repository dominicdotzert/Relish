using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Relish.Models.Filters
{
    public class IngredientFilter : Filter
    {
        public IngredientFilter(Enums.FilterTypes filterType, LocalDataManager localDataManager)
            : base(filterType)
        {
            Task.Run(async () => AllIngredients = await localDataManager.GetIngredients());
        }

        public List<Ingredient> AllIngredients { get; private set; }

        public override string ReturnQueryElement()
        {
            throw new NotImplementedException();
        }
    }
}
