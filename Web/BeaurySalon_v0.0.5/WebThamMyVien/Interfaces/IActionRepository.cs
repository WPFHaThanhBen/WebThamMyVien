using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IActionRepository
    {
        Task<ICollection<ActionDto>> GetAllAction();
        Task<ActionDto> GetAction(int id);
        Task<bool> CreateAction(ActionDto Action);
        Task<bool> UpdateAction(ActionDto Action);
        Task<bool> DeleteAction(ActionDto Action);
        Task<bool> CreateAction(int _IdUser, int _ActionTypeId, string _ContentOfChange);


    }
}
