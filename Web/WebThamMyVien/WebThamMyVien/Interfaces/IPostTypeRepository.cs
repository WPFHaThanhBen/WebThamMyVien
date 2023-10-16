using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostTypeRepository
    {
        ICollection<PostTypeDto> GetAllPostType();
        PostTypeDto GetPostType(int id);
        bool CreatePostType(PostTypeDto postType);
        bool UpdatePostType(PostTypeDto postType);
        bool DeletePostType(PostTypeDto postType);
 
    }
}
