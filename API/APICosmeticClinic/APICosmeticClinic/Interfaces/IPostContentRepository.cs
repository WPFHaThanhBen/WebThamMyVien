using APICosmeticClinic.Models;
namespace APICosmeticClinic.Interfaces
{
    public interface IPostContentRepository
    {
        ICollection<PostContent> GetAllPostContent();
        PostContent GetPostContent(int id);
        bool PostContentExists(int id);
        bool CreatePostContent(PostContent postContent);
        bool UpdatePostContent(PostContent postContent);
        bool DeletePostContent(PostContent postContent);
        bool Save();
    }
}
