using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ServiceRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateService(Service service)
        {
            _context.Add(service);
            return Save();
        }

        public bool DeleteService(Service service)
        {
            service.IsDelete = true;
            service.DateDelete = sp.GetCurrentDate();
            _context.Update(service);
            return Save();
        }

        public ICollection<Service> GetAllService()
        {
            return _context.Services.Where(c => c.IsDelete != true).ToList();
        }

		public ICollection<Service> GetAllServiceByType(int id)
		{
			return _context.Services.Where(c => c.ServiceTypeId == id && c.IsDelete != true).ToList();
		}

		public Service GetService(int id)
        {
            return _context.Services.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ServiceExists(int id)
        {
            return _context.Services.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool UpdateService(Service service)
        {
            _context.Update(service);
            return Save();
        }
    }
}
