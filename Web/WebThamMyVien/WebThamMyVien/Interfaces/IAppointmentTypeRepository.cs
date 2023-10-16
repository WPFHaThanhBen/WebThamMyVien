using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IAppointmentTypeRepository
    {
        ICollection<AppointmentTypeDto> GetAllAppointmentType();
        AppointmentTypeDto GetAppointmentType(int id);
        bool CreateAppointmentType(AppointmentTypeDto appointmentType);
        bool UpdateAppointmentType(AppointmentTypeDto appointmentType);
        bool DeleteAppointmentType(AppointmentTypeDto appointmentType);
 
    }
}
