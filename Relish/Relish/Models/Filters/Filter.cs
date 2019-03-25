using static Relish.Models.Enums;

namespace Relish.Models.Filters
{
    /// <summary>
    /// Abstract base class method for Filter objects.
    /// </summary>
    public abstract class Filter
    {
        /// <summary>
        /// Initializes the Filter object.
        /// </summary>
        /// <param name="filterType">The specific type of the filter.</param>
        protected Filter(FilterTypes filterType)
        {
            FilterType = filterType;
        }
        
        /// <summary>
        /// Gets the FilterType for the Filter object.
        /// </summary>
        public FilterTypes FilterType { get; }

        /// <summary>
        /// Method to return the HTTP GET request element for the filter.
        /// </summary>
        /// <returns>Part of the HTTP GET query.</returns>
        public abstract string ReturnQueryElement();
    }
}
