using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NsauT.Web.Controllers
{
    [Route("api/timetable")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly TimetableContext _context;

        public GroupsController(TimetableContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TimetableInfo>> Groups()
        {
            List<TimetableInfo> infos = _context.Timetables
                .Select(t => new TimetableInfo
                { 
                    Id = t.Id, 
                    Key = t.Key 
                })
                .ToList();
            return infos;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<string> GetItem(string id)
        //{
        //    string item = TimetableRepository.GetItemAsync(id).Result?.Timetable;

        //    if (item == null)
        //        return NotFound();

        //    return item;
        //}
    }
}
