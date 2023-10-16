using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class FollowUpStatusRepository : IFollowUpStatusRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public FollowUpStatusRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateFollowUpStatus(FollowUpStatus followUpStatus)
        {
            _context.Add(followUpStatus);
            return Save();
        }

        public bool DeleteFollowUpStatus(FollowUpStatus followUpStatus)
        {
            followUpStatus.IsDelete = true;
            followUpStatus.DateDelete = sp.GetCurrentDate();
            _context.Update(followUpStatus);
            return Save();
        }

        public bool FollowUpStatusExists(int id)
        {
            return _context.FollowUpStatuses.Any(c => c.Id == id && c.IsDelete != true);
        }

        public ICollection<FollowUpStatus> GetAllFollowUpStatus()
        {
            return _context.FollowUpStatuses.Where(c => c.IsDelete != true).ToList();
        }

        public FollowUpStatus GetFollowUpStatus(int id)
        {
            return _context.FollowUpStatuses.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFollowUpStatus(FollowUpStatus followUpStatus)
        {
            _context.Update(followUpStatus);
            return Save();
        }
    }
}
