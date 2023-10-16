using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class AppointmentTypeRepository : IAppointmentTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public AppointmentTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool AppointmentTypeExists(int id)
        {
            return _context.AppointmentTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateAppointmentType(AppointmentType appointmentType)
        {
            _context.Add(appointmentType);
            return Save();
        }

        public bool DeleteAppointmentType(AppointmentType appointmentType)
        {
            appointmentType.IsDelete = true;
            appointmentType.DateDelete = sp.GetCurrentDate();
            _context.Update(appointmentType);
            return Save();
        }

        public ICollection<AppointmentType> GetAllAppointmentType()
        {
            return _context.AppointmentTypes.Where(c => c.IsDelete != true).ToList();
        }

        public AppointmentType GetAppointmentType(int id)
        {
            return _context.AppointmentTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAppointmentType(AppointmentType appointmentType)
        {
            _context.Update(appointmentType);
            return Save();
        }
    }
}
