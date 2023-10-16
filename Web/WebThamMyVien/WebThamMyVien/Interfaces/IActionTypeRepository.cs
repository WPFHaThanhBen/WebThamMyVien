using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IActionTypeRepository
    {
        ICollection<ActionTypeDto> GetAllActionType();
        ActionTypeDto GetActionType(int id);
        bool CreateActionType(ActionTypeDto actionType);
        bool UpdateActionType(ActionTypeDto actionType);
        bool DeleteActionType(ActionTypeDto actionType);
 
    }
}
