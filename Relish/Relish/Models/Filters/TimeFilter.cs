using System;
using static Relish.Models.Enums;

namespace Relish.Models.Filters
{
    public class TimeFilter : Filter
    {
        public TimeFilter(FilterAttribute filterAttribute) : base(filterAttribute)
        {
        }

        public TimeSpan LowerBound { get; set; }

        public TimeSpan UpperBound { get; set; }
    }
}
