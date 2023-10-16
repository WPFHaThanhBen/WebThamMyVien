using WebThamMyVien.Models;
namespace WebThamMyVien.Interfaces
{
    public interface IPostContentRepository
    {
        ICollection<PostContentDto> GetAllPostContent();
        PostContentDto GetPostContent(int id);
        bool CreatePostContent(PostContentDto postContent);
        bool UpdatePostContent(PostContentDto postContent);
        bool DeletePostContent(PostContentDto postContent);
 
    }
}
