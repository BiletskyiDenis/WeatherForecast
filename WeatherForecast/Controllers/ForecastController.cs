using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastService;
using WeatherForecastData.Models;
using WeatherForecastService.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace WeatherForecast.Controllers
{

    public class ForecastController : Controller
    {
        private readonly IWeatherService service;

        public ForecastController(IWeatherService service)
        {
            this.service = service;
        }
        [HttpPost]
        public IActionResult Search(string name)
        {
            if (name == null || name.Length > 20 || name.Length < 2)
            {
                return BadRequest();
            }
            var data = service.Search(name);
            if (data != null)
                return Json(data);

            return NotFound();
        }

        [HttpPost]
        public IActionResult AddCity(int id)
        {
            service.AddCity(id);
            return Ok();
        }

        public IActionResult GetAll()
        {
            return Json(service.GetAll());
        }

        public IActionResult GetCount()
        {
            return Json(new { count = service.GetCount() });
        }

        public IActionResult GetFullData()
        {
            return Json(service.GetAll());
        }

        public IActionResult CheckForUpdate()
        {
            var updated = service.CheckForUpdate();
            return Json(new { dataUpdate = updated });
        }

        public IActionResult GetSelectedId()
        {
            return Json(service.GetSelectedId());
        }
        [HttpPost]
        public IActionResult SetSelectedId(int id)
        {
            service.SetSelectedId(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteCity(int id)
        {
            service.DeleteCity(id);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
        }
    }
}