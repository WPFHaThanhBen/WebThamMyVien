using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IAppointmentRepository
    {
        ICollection<Appointment> GetAllAppointment();
        Appointment GetAppointment(int id);
        bool AppointmentExists(int id);
        bool CreateAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
        bool DeleteAppointment(Appointment appointment);
        bool Save();
    }
}
