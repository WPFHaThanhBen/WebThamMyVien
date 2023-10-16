using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IAppointmentTypeRepository
    {
        ICollection<AppointmentType> GetAllAppointmentType();
        AppointmentType GetAppointmentType(int id);
        bool AppointmentTypeExists(int id);
        bool CreateAppointmentType(AppointmentType appointmentType);
        bool UpdateAppointmentType(AppointmentType appointmentType);
        bool DeleteAppointmentType(AppointmentType appointmentType);
        bool Save();
    }
}
