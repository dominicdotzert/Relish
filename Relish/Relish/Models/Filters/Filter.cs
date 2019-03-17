using System;
using Relish.Utilities;
using static Relish.Models.Enums;

namespace Relish.Models.Filters

{
    public abstract class Filter
    {
        protected Filter(FilterTypes filterType)
        {
            FilterType = filterType;
        }
        
        public FilterTypes FilterType { get; }

        public abstract string ReturnQueryElement();
    }
}
