namespace Relish.Models.Filters
{
    public class CategoryFilter : Filter
    {
        private readonly string _category;

        public CategoryFilter(Enums.FilterTypes filterType, string category) : base(filterType)
        {
            _category = category;
        }

        public override string ReturnQueryElement()
        {
            return $"{FilterType} : {_category}";
        }
    }
}
