using DataAccess;
using DataAccess.CustomModels;
using DataAccess.Repositories;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace LitmusWeb.ApiControllers
{
    public class WeatherApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [ActionName("GetWeatherRecordsByDateRange")]
        [Route("api/WeatherApiController/GetWeatherRecordsByDateRange")]
        public HttpResponseMessage GetWeatherRecordsByDateRange([FromBody] WeatherRecordsViewModel param)
        {
            WeatherRecordsRepository wRepo = new WeatherRecordsRepository();
            List<WeatherRecordsModel> wModel = new List<WeatherRecordsModel>();
            List<WeatherRecord> wr = new List<WeatherRecord>();


            wr = wRepo.weatherRecordsForDateRange(param);
            if (wr == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content!");
            }
            else
            {
                foreach (var w in wr)
                {
                    WeatherRecordsModel temp = new WeatherRecordsModel()
                    {
                        Id = w.Id,
                        UnitCode = w.UnitCode,
                        SeasonCode = w.SeasonCode,
                        RecordDate = w.RecordDate,
                        TemperatureMin = w.TemperatureMin,
                        TemperatureMax = w.TemperatureMax,
                        Humidity = w.Humidity,
                        WindSpeed = w.WindSpeed,
                        RainFallMm = w.RainFallMm,
                        UvIndex = w.UvIndex,
                        WeatherType = w.WeatherType,
                        AllWeatherConditions = w.AllWeatherConditions
                    };
                    wModel.Add(temp);
                }
                var response = new { wModel };

                return Request.CreateResponse(response);
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}