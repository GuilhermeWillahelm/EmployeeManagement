using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Dtos;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly DataBaseDBContext _context;
        private readonly IOfficeRepository _officeRepository;

        public OfficesController(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        // GET: api/Offices
        [HttpGet]
        public async Task<IEnumerable<OfficeDto>> GetOffices()
        {
            return await _officeRepository.GetOffices();
        }

        // GET: api/Offices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeDto>> GetOffice(int id)
        {
            var office = await _officeRepository.GetOffice(id);

            if (office == null)
            {
                return NotFound();
            }

            return office;
        }

        // PUT: api/Offices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffice(int id, OfficeDto office)
        {
            if (id != office.Id)
            {
                return BadRequest();
            }

            try
            {
                await _officeRepository.UpdateOffice(id, office);
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
        public async Task<ActionResult<OfficeDto>> PostOffice(OfficeDto office)
        {
            await _officeRepository.CreateOffice(office);

            return CreatedAtAction("GetOffice", new { id = office.Id }, office);
        }

        // DELETE: api/Offices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            var office = await _officeRepository.DeleteOffice(id);
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
