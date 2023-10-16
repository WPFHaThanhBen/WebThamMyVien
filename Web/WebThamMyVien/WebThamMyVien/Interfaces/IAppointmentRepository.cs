using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAppointmentRepository
    {
        ICollection<AppointmentDto> GetAllAppointment();
        AppointmentDto GetAppointment(int id);
        bool CreateAppointment(AppointmentDto appointment);
        bool UpdateAppointment(AppointmentDto appointment);
        bool DeleteAppointment(AppointmentDto appointment);
 
    }
}
