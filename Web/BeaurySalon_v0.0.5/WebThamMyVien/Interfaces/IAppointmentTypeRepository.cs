using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAppointmentTypeRepository
    {
        Task<ICollection<AppointmentTypeDto>> GetAllAppointmentType();
        Task<AppointmentTypeDto> GetAppointmentType(int id);
        Task<bool> CreateAppointmentType(AppointmentTypeDto AppointmentType);
        Task<bool> UpdateAppointmentType(AppointmentTypeDto AppointmentType);
        Task<bool> DeleteAppointmentType(AppointmentTypeDto AppointmentType);

    }
}
