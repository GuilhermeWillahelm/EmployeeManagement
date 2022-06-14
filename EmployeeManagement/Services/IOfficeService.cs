using EmployeeManagement.Dtos;

namespace EmployeeManagement.Services
{
    public interface IOfficeService
    {
        Task<IEnumerable<OfficeDto>> GetOffices();
        Task<OfficeDto> GetOffice(int id);
        Task<OfficeDto> CreateOffice(OfficeDto office);
        Task<OfficeDto> UpdateOffice(int id, OfficeDto office);
        Task<OfficeDto> DeleteOffice(int id);
    }
}
