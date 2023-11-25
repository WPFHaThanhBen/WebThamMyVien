using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostRepository
    {
        Task<ICollection<PostDto>> GetAllPost();
        Task<PostDto> GetPost(int id);
        Task<bool> CreatePost(PostDto Post);
        Task<bool> UpdatePost(PostDto Post);
        Task<bool> DeletePost(PostDto Post);
    }
}
