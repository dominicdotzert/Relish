using Relish.Utilities;

namespace Relish.Models.Filters
{
    /// <summary>
    /// Model class to represent a category filter.
    /// </summary>
    public class CategoryFilter : Filter
    {
        private readonly string _category;

        /// <summary>
        /// Initializes the Category Filter object.
        /// </summary>
        /// <param name="filterType">The type of filter.</param>
        /// <param name="category">The category type to be searched for.</param>
        public CategoryFilter(Enums.FilterTypes filterType, string category) : base(filterType)
        {
            _category = category;
        }

        /// <summary>
        /// Gets the HTTP Get request element for the filter.
        /// </summary>
        /// <returns>Part of the HTTP GET query.</returns>
        public override string ReturnQueryElement()
        {
            return $"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={_category}";
        }
    }
}
