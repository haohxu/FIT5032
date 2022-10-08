using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FIT5032_SimpleAngularApp.Models;
using Microsoft.AspNetCore.Cors;

namespace FIT5032_SimpleAngularApp.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly UnitContext _context;

        public UnitsController(UnitContext context)
        {
            _context = context;
        }

        // GET: api/Units
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        // {
        //     return await _context.Units.ToListAsync();
        // }

        // GET: api/Units
        [EnableCors]
        [HttpGet]
        public ActionResult<IEnumerable<Unit>> GetUnits()
        {
            return new []
            {
                new Unit { Id = "001", UnitCode = "FIT5032", UnitName = "Web Application Development"}
            };
        }

        // GET: api/Units/5
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(string id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return new Unit { Id = "001", UnitCode = "FIT5032", UnitName = "Web Application Development"};
            }

            return unit;
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(string id, Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPost]
        public void PostUnit(Unit unit)
        {
            return;
        }

        // DELETE: api/Units/5
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(string id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(string id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
