using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;

namespace APICosmeticClinic.Repository
{
    public class ActionTypeRepository : IActionTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ActionTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool ActionTypeExists(int id)
        {
            return _context.ActionTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool CreateActionType(ActionType actionType)
        {
            _context.Add(actionType);
            return Save();
        }

        public bool DeleteActionType(ActionType actionType)
        {
            actionType.IsDelete = true;
            actionType.DateDelete = sp.GetCurrentDate();
            _context.Update(actionType);
            return Save();
        }

        public ActionType GetActionType(int id)
        {
            return _context.ActionTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public ICollection<ActionType> GetAllActionType()
        {
            return _context.ActionTypes.Where(c => c.IsDelete != true).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateActionType(ActionType actionType)
        {
            _context.Update(actionType);
            return Save();
        }
    }
}
