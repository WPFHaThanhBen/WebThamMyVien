using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IActionRepository
    {
        ICollection<ActionDto> GetAllAction();
        ActionDto GetAction(int id);
        bool CreateAction(ActionDto action);
        bool UpdateAction(ActionDto action);
        bool DeleteAction(ActionDto action);
 
    }
}
