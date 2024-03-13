using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using System.Diagnostics.Eventing.Reader;
using static MLModel_WebApi1.Models.WeatherTemperatureModel;

namespace MLModel_WebApi1.Services
{
    public class WeatherPredictTemperatureService
    {


        public WeatherByDateForeCast PredictTemperature(string Station = null)
        {
            var context = new MLContext();

            var filename = @$"{AppContext.BaseDirectory}\Data\newDatos temperatura Argentina.csv";
            var data = context.Data.LoadFromTextFile<WeatherByDate>(filename,
                hasHeader: true, separatorChar: ';' );


            int iFilelineCount = File.ReadLines(filename).Count();

            var pipeline = context.Forecasting.ForecastBySsa(
                nameof(WeatherByDateForeCast.ForecastTemp),
                nameof(WeatherByDate.TempMax),
                windowSize: 5,
                seriesLength: iFilelineCount,
                trainSize: iFilelineCount,
                horizon: 5,
                confidenceLevel: 0.99f
                //confidenceLowerBoundColumn: "ForeCastLowestValue",
                //confidenceUpperBoundColumn: "ForeCastUpperValue"
                );
            Microsoft.ML.IDataView filteredData;

            if (Station is not null)
            {
                filteredData = context.Data.FilterByCustomPredicate<WeatherByDate>(data, (x) => x.Station.Trim().ToLower() != Station.ToLower());
            }
            else { filteredData = data; }

            // here you can preview information, it can be removed.
            var preview = filteredData.Preview();

            var model = pipeline.Fit(filteredData);

            var forecastingEngine = model.CreateTimeSeriesEngine<WeatherByDate, WeatherByDateForeCast>(context);


            var forecasts = forecastingEngine.Predict();
                    

            return forecasts;
        }


    }
}
