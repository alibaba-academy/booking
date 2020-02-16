using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Booking.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Booking.Controllers
{
    [Route("api/v1/shows")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ShowController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Show show)
        {
        

            var testShow = _appDbContext.Shows.Find(show.Id);
            if (testShow != null)
            {
                return Conflict();
            }

            if (show == null)
            {
                return BadRequest();
            }

            if (show.EndTime == null || show.StartTime == null || show.Title == null ||
                show.Summary == null || show.Price < 0)
            {
                return BadRequest();
            }

            if (show.StartTime <= DateTime.Now)
            {
                return BadRequest();
            }

            if (show.StartTime >= show.EndTime)
            {
                return BadRequest();
            }

            if (!IsSalonAvailable(show.SalonId))
            {
                return Conflict();
            }

            const int maxSummaryLength = 250;
            const int maxTitleLength = 40;
            if (show.Summary.Length > maxSummaryLength || show.Title.Length > maxTitleLength)
            {
                return BadRequest();
            }

            const int maxPrice = 100;
            if (show.Price > maxPrice)
            {
                return BadRequest();
            }

            const int minShowTime = 30;
            const int maxShowTime = 120;
            int showLenght = ((show.EndTime - show.StartTime).Hours * 60) + (show.EndTime - show.StartTime).Minutes;
            if (showLenght < minShowTime || showLenght > maxShowTime)
            {
                return BadRequest();
            }
            bool hasConflict = DefinedShowHasConflict(show);

            if (hasConflict)
            {
                
                return Conflict();
            }

            _appDbContext.Shows.Add(show);
            _appDbContext.SaveChanges();
            return Created("show created", show);
        }
        public bool DefinedShowHasConflict(Show show)
        {

            IEnumerable<Show> query =
               from Var in _appDbContext.Shows.AsEnumerable()
               where ShowsTimesHaveConflict(Var, show)
               select Var;
            foreach (var VARIABLE in query)
            {
                if (VARIABLE.SalonId == show.SalonId)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ShowsTimesHaveConflict([FromQuery]Show show1, Show show2)
        {
            if (show1.StartTime.Date == show2.StartTime.Date)
            {
                if (show1.StartTime.Hour <= show2.StartTime.Hour && show2.StartTime <= show1.EndTime)
                {
                    return true;
                }
                if (show1.StartTime.Hour <= show2.EndTime.Hour && show2.EndTime.Hour <= show1.EndTime.Hour)
                {
                    return true;
                }
                if (show1.StartTime.Hour <= show2.StartTime.Hour && show1.EndTime.Hour >= show2.EndTime.Hour)
                {
                    return true;
                }
                if (show2.StartTime.Hour <= show1.StartTime.Hour && show2.EndTime.Hour >= show1.EndTime.Hour)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;

            }
        }

        public bool IsSalonAvailable(int salonId)
        {
            var salonIds = _appDbContext.Salons.Select(s => s.Id);
            foreach (int id in salonIds)
            {
                if (salonId == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}