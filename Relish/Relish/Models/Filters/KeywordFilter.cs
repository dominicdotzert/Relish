using Relish.Utilities;

namespace Relish.Models.Filters
{
    class KeywordFilter : Filter
    {
        private readonly string _keyword;

        public KeywordFilter(Enums.FilterTypes filterType, string keyword) : base(filterType)
        {
            _keyword = keyword;
        }

        public override string ReturnQueryElement()
        {
            return $"{EnumToStringUtility.FilterTypeToQueryKeyDict[FilterType]}={_keyword}";
        }
    }
}
