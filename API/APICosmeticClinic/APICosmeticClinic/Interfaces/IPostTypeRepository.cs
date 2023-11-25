using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IPostTypeRepository
    {
        ICollection<PostType> GetAllPostType();
        PostType GetPostType(int id);
        PostType GetPostTypeByName(string name);
        bool PostTypeExists(int id);
        bool CreatePostType(PostType postType);
        bool UpdatePostType(PostType postType);
        bool DeletePostType(PostType postType);
        bool Save();
    }
}
