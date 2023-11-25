using WebThamMyVien.Models;
namespace WebThamMyVien.Interfaces
{
    public interface IPostContentRepository
    {
        Task<ICollection<PostContentDto>> GetAllPostContent();
        Task<PostContentDto> GetPostContent(int id);
        Task<bool> CreatePostContent(PostContentDto PostContent);
        Task<bool> UpdatePostContent(PostContentDto PostContent);
        Task<bool> DeletePostContent(PostContentDto PostContent);

    }
}
