using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAppointmentStatusRepository
    {
        Task<ICollection<AppointmentStatusDto>> GetAllAppointmentStatus();
        Task<AppointmentStatusDto> GetAppointmentStatus(int id);
        Task<bool> CreateAppointmentStatus(AppointmentStatusDto AppointmentStatus);
        Task<bool> UpdateAppointmentStatus(AppointmentStatusDto AppointmentStatus);
        Task<bool> DeleteAppointmentStatus(AppointmentStatusDto AppointmentStatus);

    }
}
