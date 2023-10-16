using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IAppointmentStatusRepository
    {
        ICollection<AppointmentStatus> GetAllAppointmentStatus();
        AppointmentStatus GetAppointmentStatus(int id);
        bool AppointmentStatusExists(int id);
        bool CreateAppointmentStatus(AppointmentStatus appointmentStatus);
        bool UpdateAppointmentStatus(AppointmentStatus appointmentStatus);
        bool DeleteAppointmentStatus(AppointmentStatus appointmentStatus);
        bool Save();
    }
}
