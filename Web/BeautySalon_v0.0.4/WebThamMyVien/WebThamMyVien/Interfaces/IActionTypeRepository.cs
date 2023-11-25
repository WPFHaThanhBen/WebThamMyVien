using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IActionTypeRepository
    {
        Task<ICollection<ActionTypeDto>> GetAllActionType();
        Task<ActionTypeDto> GetActionType(int id);
        Task<bool> CreateActionType(ActionTypeDto ActionType);
        Task<bool> UpdateActionType(ActionTypeDto ActionType);
        Task<bool> DeleteActionType(ActionTypeDto ActionType);

    }
}
