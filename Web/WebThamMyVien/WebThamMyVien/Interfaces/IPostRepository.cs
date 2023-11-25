using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostRepository
    {
        Task<ICollection<PostDto>> GetAllPost();
        Task<ICollection<PostDto>> GetAllPostSkip(int start, int skip);
        Task<ICollection<PostDto>> GetPostByPostType(int id);
        Task<PostDto> GetPost(int id);
        Task<PostDto> GetPostFinal();
        Task<bool> CreatePost(PostDto Post);
        Task<bool> UpdatePost(PostDto Post);
        Task<bool> DeletePost(PostDto Post);
    }
}
