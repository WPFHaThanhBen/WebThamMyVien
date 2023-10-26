using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class ActionRepository : IActionRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ActionRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool ActionExists(int id)
        {
            return _context.Actions.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateAction(Models.Action action)
        {
            _context.Add(action);
            return Save();
        }

        public bool DeleteAction(Models.Action action)
        {
            action.IsDelete = true;
            action.DateDelete = sp.GetCurrentDate();
            _context.Update(action);
            return Save();
        }

        public Models.Action GetAction(int id)
        {
            return _context.Actions.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public ICollection<Models.Action> GetAllAction()
        {
            return _context.Actions.Where(c => c.IsDelete != true).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAction(Models.Action action)
        {
            _context.Update(action);
            return Save();
        }
    }
}
