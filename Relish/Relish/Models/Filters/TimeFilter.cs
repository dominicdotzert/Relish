namespace Relish.Models.Filters
{
    public class TimeFilter : Filter
    {
        private readonly int _maxTime;

        public TimeFilter(Enums.FilterTypes filterType, int maxTime) : base(filterType)
        {
            _maxTime = maxTime;
        }

        public override string ReturnQueryElement()
        {
            ////return $"\"{FilterType.ToString()}\" : {_maxTime}";
            return $"{FilterType}={_maxTime}";
        }
    }
}
