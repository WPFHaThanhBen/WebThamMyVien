using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IActionTypeRepository
    {
        ICollection<ActionType> GetAllActionType();
        ActionType GetActionType(int id);
        bool ActionTypeExists(int id);
        bool CreateActionType(ActionType actionType);
        bool UpdateActionType(ActionType actionType);
        bool DeleteActionType(ActionType actionType);
        bool Save();
    }
}
