using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetAllPost();
        Post GetPost(int id);
        bool PostExists(int id);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool Save();
    }
}
