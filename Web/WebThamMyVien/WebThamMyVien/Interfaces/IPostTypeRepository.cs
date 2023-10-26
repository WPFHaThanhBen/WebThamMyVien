using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostTypeRepository
    {
        Task<ICollection<PostTypeDto>> GetAllPostType();
        Task<PostTypeDto> GetPostType(int id);
        Task<bool> CreatePostType(PostTypeDto PostType);
        Task<bool> UpdatePostType(PostTypeDto PostType);
        Task<bool> DeletePostType(PostTypeDto PostType);

    }
}
