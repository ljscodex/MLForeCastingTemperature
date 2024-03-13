using Microsoft.ML.Data;
using System;

namespace MLModel_WebApi1.Models
{
    public class WeatherTemperatureModel
    {

        public class WeatherByDate
        {
            [LoadColumn(0)]
            public float Date { get; set; }

            [LoadColumn(1)]
            public float TempMax { get; set; }

            [LoadColumn(2)]
            public float TempMin { get; set; }

            [LoadColumn(3)]
            public string Station { get; set; }

        }

        public class WeatherByDateForeCast
        {

            public float[] ForecastTemp { get; set; }
         //   public float[] ForeCastLowestValue { get; set; }
         //   public float[] ForeCastUpperValue { get; set; }
        }
    }
}