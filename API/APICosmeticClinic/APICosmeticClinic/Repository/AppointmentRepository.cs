using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public AppointmentRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateAppointment(Appointment appointment)
        {
            _context.Add(appointment);
            return Save();
        }

        public bool DeleteAppointment(Appointment appointment)
        {
            appointment.IsDelete = true;
            appointment.DateDelete = sp.GetCurrentDate();
            _context.Update(appointment);
            return Save();
        }

        public ICollection<Appointment> GetAllAppointment()
        {
            return _context.Appointments.Where(c => c.IsDelete != true).ToList();
        }

        public Appointment GetAppointment(int id)
        {
            return _context.Appointments.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            _context.Update(appointment);
            return Save();
        }
    }
}
