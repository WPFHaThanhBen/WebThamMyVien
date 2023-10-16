using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
                private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ServiceTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateServiceType(ServiceType serviceType)
        {
            _context.Add(serviceType);
            return Save();
        }

        public bool DeleteServiceType(ServiceType serviceType)
        {
            serviceType.IsDelete = true;
            serviceType.DateDelete = sp.GetCurrentDate();
            _context.Update(serviceType);
            return Save();
        }

        public ICollection<ServiceType> GetAllServiceType()
        {
            return _context.ServiceTypes.Where(c => c.IsDelete != true).ToList();
        }

        public ServiceType GetServiceType(int id)
        {
            return _context.ServiceTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ServiceTypeExists(int id)
        {
            return _context.ServiceTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool UpdateServiceType(ServiceType serviceType)
        {
            _context.Update(serviceType);
            return Save();
        }
    }
}
