using APICosmeticClinic.Models;
using Action = APICosmeticClinic.Models.Action;

namespace APICosmeticClinic.Interfaces
{
    public interface IActionRepository
    {
        ICollection<Action> GetAllAction();
        Action GetAction(int id);
        bool ActionExists(int id);
        bool CreateAction(Action action);
        bool UpdateAction(Action action);
        bool DeleteAction(Action action);
        bool Save();
    }
}
