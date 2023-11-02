using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IPostImageRepository
    {
        ICollection<PostImage> GetAllPostImage();
        ICollection<PostImage> GetAllPostImageByPost(int id);
        PostImage GetPostImage(int id);
        bool PostImageExists(int id);
        bool CreatePostImage(PostImage postImage);
        bool UpdatePostImage(PostImage postImage);
        bool DeletePostImage(PostImage postImage);
        bool Save();
    }
}
