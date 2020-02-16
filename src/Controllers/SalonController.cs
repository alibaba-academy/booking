using Booking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;

using System.Linq;

namespace Booking.Controllers
{
    [Route("api/v1/salons")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public const int MaxLength = 10;

        public SalonController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Salon salon)
        {
            if (salon == null)
            {
                return BadRequest();
            }

			if (salon.Id != 0) {
				return BadRequest();
			}

            if (string.IsNullOrEmpty(salon.Name) || salon.SeatHeight <= 0 || salon.SeatWidth <= 0)
            {
                return BadRequest();
            }

            if (salon.Name.Length > MaxLength) {
                return BadRequest();
            }

            if (salon.SeatHeight < 0 || salon.SeatWidth < 0) {
                return BadRequest();
            }

            _appDbContext.Salons.Add(salon);
            _appDbContext.SaveChanges();
            return Created("salon created", salon);
        }

		[HttpGet]
		public IActionResult GetSalons() {
			var salons = _appDbContext.Salons.Select(x => x).ToList();
			return Ok(salons);
		}

		[HttpGet("{id}")]
		public IActionResult GetSalon(int id) {
			var salon = _appDbContext.Salons.Find(id);
			if (salon == null) {
				return NotFound();
			}

			return Ok(salon);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateSalon(int id, [FromBody] Salon newSalon) {
			var salon = _appDbContext.Salons.Find(id);
			if (salon == null) {
				return NotFound();
			}

			if (newSalon.Id != 0) {
				if (newSalon.Id != id) {
					return BadRequest();
				}
			}

			if (string.IsNullOrEmpty(salon.Name) || salon.SeatHeight <= 0 || salon.SeatWidth <= 0)
            {
                return BadRequest();
            }

            if (salon.Name.Length > MaxLength) {
                return BadRequest();
            }

            if (salon.SeatHeight < 0 || salon.SeatWidth < 0) {
                return BadRequest();
            }

			if (!string.IsNullOrEmpty(newSalon.Name)) {
				salon.Name = newSalon.Name;
			}

			if (newSalon.SeatWidth != 0) {
				salon.SeatWidth = newSalon.SeatWidth;
			}

			if (newSalon.SeatHeight != 0) {
				salon.SeatHeight = newSalon.SeatHeight;
			}

			_appDbContext.SaveChanges();

			return Ok(salon);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteSalon(int id) {
			var salon = _appDbContext.Salons.Find(id);
			if (salon == null) {
				return NotFound();
			}
			_appDbContext.Entry(salon).State = EntityState.Deleted;
			_appDbContext.SaveChanges();

			return Ok();
		}
    }
}
