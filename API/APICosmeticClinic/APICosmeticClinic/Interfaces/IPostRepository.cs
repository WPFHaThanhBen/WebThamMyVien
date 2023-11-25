using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetAllPost();
        ICollection<Post> GetAllPostSkip(int start, int skip);
        Post GetPost(int id);
        Post GetPostFinal();
        ICollection<Post> GetPostByPostType(int id);
        bool PostExists(int id);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool Save();
    }
}
