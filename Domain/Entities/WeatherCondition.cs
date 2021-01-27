using System;

namespace Domain.Entities
{
    public class WeatherCondition : BaseEntity
    {
        public Guid EventId { get; set; }
        public string CurrentCondition { get; set; }
        public DateTimeOffset ObservationDate { get; set; }
        public double Temperature { get; set; }
        public double RealFeelTemp { get; set; }
        public int RelativeHumidity { get; set; }
        public int Precipitation { get; set; }
        public int WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public int Icon { get; set; }
    }
}