using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class PostRepository : IPostRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public PostRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreatePost(Post post)
        {
            _context.Add(post);
            return Save();
        }

        public bool DeletePost(Post post)
        {
            post.IsDelete = true;
            post.DateDelete = sp.GetCurrentDate();
            _context.Update(post);
            return Save();
        }

        public ICollection<Post> GetAllPost()
        {
            return _context.Posts.Where(c => c.IsDelete != true).ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePost(Post post)
        {
            _context.Update(post);
            return Save();
        }
    }
}
