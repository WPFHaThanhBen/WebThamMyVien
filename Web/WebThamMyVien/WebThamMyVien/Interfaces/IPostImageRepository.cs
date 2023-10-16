using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostImageRepository
    {
        ICollection<PostImageDto> GetAllPostImage();
        PostImageDto GetPostImage(int id);
        bool CreatePostImage(PostImageDto postImage);
        bool UpdatePostImage(PostImageDto postImage);
        bool DeletePostImage(PostImageDto postImage);
 
    }
}
