using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Dtos;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly DataBaseDBContext _context;
        private readonly IOfficeService _officeService;

        public OfficesController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        // GET: api/Offices
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<OfficeDto>> GetOffices()
        {
            return await _officeService.GetOffices();
        }

        // GET: api/Offices/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OfficeDto>> GetOffice(int id)
        {
            var office = await _officeService.GetOffice(id);

            if (office == null)
            {
                return NotFound();
            }

            return office;
        }

        // PUT: api/Offices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutOffice(int id, OfficeDto office)
        {
            if (id != office.Id)
            {
                return BadRequest();
            }

            try
            {
                await _officeService.UpdateOffice(id, office);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(id))
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

        // POST: api/Offices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<OfficeDto>> PostOffice(OfficeDto office)
        {
            await _officeService.CreateOffice(office);

            return CreatedAtAction("GetOffice", new { id = office.Id }, office);
        }

        // DELETE: api/Offices/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            var office = await _officeService.DeleteOffice(id);
            if (office == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool OfficeExists(int id)
        {
            return (_context.Offices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
