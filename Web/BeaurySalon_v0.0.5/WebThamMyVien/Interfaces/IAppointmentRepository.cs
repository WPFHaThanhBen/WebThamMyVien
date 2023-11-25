using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<ICollection<AppointmentDto>> GetAllAppointment();
        Task<AppointmentDto> GetAppointment(int id);
        Task<bool> CreateAppointment(AppointmentDto Appointment);
        Task<bool> UpdateAppointment(AppointmentDto Appointment);
        Task<bool> DeleteAppointment(AppointmentDto Appointment);

    }
}
