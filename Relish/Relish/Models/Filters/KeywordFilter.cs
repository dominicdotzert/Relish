using Relish.Utilities;

namespace Relish.Models.Filters
{
    /// <summary>
    /// Model class to represent a keyword filter.
    /// </summary>
    public class KeywordFilter : Filter
    {
        private readonly string _keyword;

        /// <summary>
        /// Initializes the keyword filter object.
        /// </summary>
        /// <param name="filterType">The type of the filter.</param>
        /// <param name="keyword">The string to be searched for.</param>
        public KeywordFilter(Enums.FilterTypes filterType, string keyword) : base(filterType)
        {
            _keyword = keyword;
        }

        /// <summary>
        /// Gets the HTTP Get request element for the filter.
        /// </summary>
        /// <returns>Part of the HTTP GET query.</returns>
        public override string ReturnQueryElement()
        {
            return $"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={_keyword}";
        }
    }
}
