using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WeatherForecast.Models;
using WeatherForecastData.Models;
using WeatherForecastService;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ForecastController : Controller
    {
        private readonly IWeatherService service;
        private readonly IMapper _mapper;

        public ForecastController(IWeatherService service, IMapper mapper)
        {
            this.service = service;
            this._mapper = mapper;
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
            var cityTemp = service.GetAll();

            var dto= _mapper.Map<IEnumerable<CityDto>>(cityTemp);

            return Json(dto);
        }

        public IActionResult GetCount()
        {
            return Json(new { count = service.GetCount() });
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