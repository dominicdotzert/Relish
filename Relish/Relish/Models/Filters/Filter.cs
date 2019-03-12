using Relish.Utilities;
using static Relish.Models.Enums;

namespace Relish.Models.Filters

{
    public abstract class Filter
    {
        protected Filter(FilterAttribute filterAttribute)
        {
            FriendlyName = EnumToFriendlyStringConverter.FilterAttributeToString(filterAttribute);
            FilterAttribute = filterAttribute;
        }

        /// <summary>
        /// Name of the filter. Descriptive name which will be displayed in UI.
        /// </summary>
        public string FriendlyName { get; }

        public FilterAttribute FilterAttribute { get; }
    }
}
