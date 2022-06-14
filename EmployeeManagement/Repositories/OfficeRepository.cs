using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly DataBaseDBContext _context;
        private readonly ILogger _logger;

        public OfficeRepository(DataBaseDBContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OfficeDto> CreateOffice(OfficeDto officeDto)
        {
            var officeItem = new Office 
            {
                Id = officeDto.Id,
                NameOffice = officeDto.NameOffice
            };

            _context.Offices.Add(officeItem);
            await _context.SaveChangesAsync();

            return OfficeToDto(officeItem);
        }

        public async Task<OfficeDto> DeleteOffice(int id)
        {
            var officeItem = await _context.Offices.FindAsync(id);

            if (officeItem == null)
            {
                return null;
            }

            _context.Offices.Remove(officeItem);
            await _context.SaveChangesAsync();

            return OfficeToDto(officeItem);
        }

        public async Task<OfficeDto> GetOffice(int id)
        {
            var officeItem = await _context.Offices.FindAsync(id);

            return OfficeToDto(officeItem);
        }

        public async Task<IEnumerable<OfficeDto>> GetOffices()
        {
            return await _context.Offices.Select(o => OfficeToDto(o)).ToListAsync();
        }

        public async Task<OfficeDto> UpdateOffice(int id, OfficeDto officeDto)
        {
            if (id != officeDto.Id)
            {
                return null;
            }

            var officeItem = await _context.Offices.FindAsync(id);
            
            if (officeItem == null)
            {
                return null;
            }

            officeItem.Id = officeDto.Id;
            officeItem.NameOffice = officeDto.NameOffice;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(OfficeRepository));
                return new OfficeDto();
            }

            return new OfficeDto();
        }

        private static OfficeDto OfficeToDto(Office office) =>
            new OfficeDto
            {
                Id = office.Id,
                NameOffice = office.NameOffice,
            };
    }
}
