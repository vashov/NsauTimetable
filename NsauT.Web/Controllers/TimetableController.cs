using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;

namespace NsauT.Web.Controllers
{
    [Route("api/timetable")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly TimetableContext _context;

        public TimetableController(TimetableContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<string>> List()
        //{
        //    return _context.GetItemsAsync().Result.Select(t => t.Timetable).ToList();
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TimetableEntity> GetTimetable(int id)
        {
            TimetableEntity timetable = _context.Timetables.FirstOrDefault(t => t.Id == id);

            if (timetable == null)
                return NotFound();

            return timetable;
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Timetable> Create([FromBody]Timetable item)
        //{
        //    ItemRepository.Add(item);
        //    return CreatedAtAction(nameof(GetItem), new { item.Id }, item);
        //}

        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult Edit([FromBody] Timetable item)
        //{
        //    try
        //    {
        //        ItemRepository.Update(item);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("Error while editing item");
        //    }
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult Delete(int id)
        //{
        //    Timetable item = ItemRepository.Remove(id);

        //    if (item == null)
        //        return NotFound();

        //    return Ok();
        //}
    }
}
