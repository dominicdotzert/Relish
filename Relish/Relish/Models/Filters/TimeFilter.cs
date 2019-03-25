using Relish.Utilities;

namespace Relish.Models.Filters
{
    /// <summary>
    /// Model class to represent a time filter.
    /// </summary>
    public class TimeFilter : Filter
    {
        private readonly int _maxTime;

        /// <summary>
        /// Initializes an TimeFilter object.
        /// </summary>
        /// <param name="filterType">The type of filter.</param>
        /// <param name="maxTime">The maximum time value in minutes.</param>
        public TimeFilter(Enums.FilterTypes filterType, int maxTime) : base(filterType)
        {
            _maxTime = maxTime;
        }

        /// <summary>
        /// Gets the HTTP Get request element for the filter.
        /// </summary>
        /// <returns>Part of the HTTP GET query.</returns>
        public override string ReturnQueryElement()
        {
            return $"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={_maxTime}";
        }
    }
}
