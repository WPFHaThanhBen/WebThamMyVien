using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class AppointmentStatusRepository : IAppointmentStatusRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public AppointmentStatusRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool AppointmentStatusExists(int id)
        {
            return _context.AppointmentStatuses.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateAppointmentStatus(AppointmentStatus appointmentStatus)
        {
            _context.Add(appointmentStatus);
            return Save();
        }

        public bool DeleteAppointmentStatus(AppointmentStatus appointmentStatus)
        {
            appointmentStatus.IsDelete = true;
            appointmentStatus.DateDelete = sp.GetCurrentDate();
            _context.Update(appointmentStatus);
            return Save();
        }

        public ICollection<AppointmentStatus> GetAllAppointmentStatus()
        {
            return _context.AppointmentStatuses.Where(c => c.IsDelete != true).ToList();
        }

        public AppointmentStatus GetAppointmentStatus(int id)
        {
            return _context.AppointmentStatuses.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAppointmentStatus(AppointmentStatus appointmentStatus)
        {
            _context.Update(appointmentStatus);
            return Save(); ;
        }
    }
}
