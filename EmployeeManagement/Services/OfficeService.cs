using EmployeeManagement.Dtos;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeService(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<OfficeDto> CreateOffice(OfficeDto office)
        {
            return await _officeRepository.CreateOffice(office);
        }

        public async Task<OfficeDto> DeleteOffice(int id)
        {
            return await _officeRepository.DeleteOffice(id);
        }

        public async Task<OfficeDto> GetOffice(int id)
        {
            return await _officeRepository.GetOffice(id);
        }

        public async Task<IEnumerable<OfficeDto>> GetOffices()
        {
            return await _officeRepository.GetOffices();
        }

        public async Task<OfficeDto> UpdateOffice(int id, OfficeDto office)
        {
            return await _officeRepository.UpdateOffice(id, office);
        }
    }
}
