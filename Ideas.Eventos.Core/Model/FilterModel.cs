namespace Ideas.Eventos.Hoteis.Core.Model
{
    public class FilterModel
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public double MetricValue { get; set; }
        public int Guests { get; set; }
        public int From { get; set; }
        public int Limit { get; set; }


        //public Expression<Func<HotelBasic, bool>> GetFilterExpression()
        //{
        //    Expression<Func<HotelBasic, bool>> resultExpression = x => true;
        //    Expression<Func<HotelBasic, bool>> cityFilter = x => x.city == City;
        //    Expression<Func<HotelBasic, bool>> metricValueFilter = x => x.largestMeetingSpace.metricValue >= MetricValue;
        //    Expression<Func<HotelBasic, bool>> guestsFilter = x => x.totalSleepingRoom == (Guests / 2);

        //    if (!string.IsNullOrEmpty(City))
        //    {
        //        return Helper.AndAlso<HotelBasic>(metricValueFilter, cityFilter);
        //    }
        //    if (MetricValue != 0)
        //    {
        //        resultExpression = Expression.Lambda<Func<HotelBasic, bool>>(Expression.AndAlso(resultExpression.Body, metricValueFilter.Body), resultExpression.Parameters[0]);
        //    }
        //    if (Guests != 0)
        //    {
        //        resultExpression = Expression.Lambda<Func<HotelBasic, bool>>(Expression.AndAlso(resultExpression.Body, guestsFilter.Body), resultExpression.Parameters[0]);
        //    }

        //    return resultExpression;
        //}

    }
}
