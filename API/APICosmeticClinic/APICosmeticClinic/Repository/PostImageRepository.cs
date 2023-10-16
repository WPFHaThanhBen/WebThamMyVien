using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class PostImageRepository : IPostImageRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public PostImageRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreatePostImage(PostImage postImage)
        {
            _context.Add(postImage);
            return Save();
        }

        public bool DeletePostImage(PostImage postImage)
        {
            postImage.IsDelete = true;
            postImage.DateDelete = sp.GetCurrentDate();
            _context.Update(postImage);
            return Save();
        }

        public ICollection<PostImage> GetAllPostImage()
        {
            return _context.PostImages.Where(c => c.IsDelete != true).ToList();
        }

        public PostImage GetPostImage(int id)
        {
            return _context.PostImages.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool PostImageExists(int id)
        {
            return _context.PostImages.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePostImage(PostImage postImage)
        {
            _context.Update(postImage);
            return Save();
        }
    }
}
