using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAppointmentStatusRepository
    {
        ICollection<AppointmentStatusDto> GetAllAppointmentStatus();
        AppointmentStatusDto GetAppointmentStatus(int id);
        bool CreateAppointmentStatus(AppointmentStatusDto appointmentStatus);
        bool UpdateAppointmentStatus(AppointmentStatusDto appointmentStatus);
        bool DeleteAppointmentStatus(AppointmentStatusDto appointmentStatus);
 
    }
}
