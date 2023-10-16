using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public BranchRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool BranchExists(int id)
        {
            return _context.Branches.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateBranch(Branch branch)
        {
            _context.Add(branch);
            return Save();
        }

        public bool DeleteBranch(Branch branch)
        {
            branch.IsDelete = true;
            branch.DateDelete = sp.GetCurrentDate();
            _context.Update(branch);
            return Save();
        }

        public ICollection<Branch> GetAllBranch()
        {
            return _context.Branches.Where(c => c.IsDelete != true).ToList();
        }

        public Branch GetBranch(int id)
        {
            return _context.Branches.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBranch(Branch branch)
        {
            _context.Update(branch);
            return Save();
        }
    }
}
