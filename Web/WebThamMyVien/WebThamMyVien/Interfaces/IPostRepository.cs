using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostRepository
    {
        ICollection<PostDto> GetAllPost();
        PostDto GetPost(int id);
        bool CreatePost(PostDto post);
        bool UpdatePost(PostDto post);
        bool DeletePost(PostDto post);
 
    }
}
