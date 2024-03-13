using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using MLModel_WebApi1.Services;
using static MLModel_WebApi1.Models.WeatherTemperatureModel;

namespace MLModel_WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherPredictController : ControllerBase
    {

        private WeatherPredictTemperatureService _MLService = new WeatherPredictTemperatureService();

        public WeatherPredictController()
        {

        }




        [HttpGet]
        [Route("PredictTemperatureALL")]
       public ActionResult<WeatherByDateForeCast> PredictTemperatureALL()
        {
            WeatherByDateForeCast p = _MLService.PredictTemperature();
            return Ok(p);
        }

        [HttpGet]
        [Route("PredictTemperatureByStation")]
        public ActionResult<WeatherByDateForeCast> PredictTemperaturebyStation(string Station)
        {
            WeatherByDateForeCast p = _MLService.PredictTemperature(Station);
            return Ok(p);
        }



    }
}
