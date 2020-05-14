using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NsauT.Shared.Models;
using NsauT.Shared.Models.TimetableInfo;
using NsauT.Web.BLL.Services.Timetable;
using NsauT.Web.BLL.Services.Timetable.DTO;
using NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi;

namespace NsauT.Web.Areas.Api.Controllers
{
    [Area("api")]
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : Controller
    {
        private ITimetableService _timetableService;
        private IMapper _mapper;

        public TimetableController(ITimetableService timetableService, IMapper mapper)
        {
            _timetableService = timetableService;
            _mapper = mapper;
        }

        // GET: api/Timetable
        [HttpGet]
        public JsonResult GetApprovedTimetables()
        {
            IReadOnlyCollection<ApprovedTimetableDto> timetables = _timetableService.GetApprovedTimetables().ToList();
            var timetablesKeys = _mapper.Map<IReadOnlyCollection<ApprovedTimetableKey>>(timetables);

            return Json(timetablesKeys);
        }

        // GET: api/Timetable/5
        [HttpGet("{id}")]
        public JsonResult GetApprovedTimetable(int id)
        {
            if (id == 0)
            {
                return Json(new object { });
            }
            TimetableApiDto timetableDto = _timetableService.GetApprovedTimetable(id);
            if (timetableDto == null)
            {
                return Json(new object { });
            }

            var timetable = _mapper.Map<TimetableModel>(timetableDto);
            return Json(timetable);
        }

        //// GET: api/Timetable
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Timetable/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Timetable
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Timetable/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
