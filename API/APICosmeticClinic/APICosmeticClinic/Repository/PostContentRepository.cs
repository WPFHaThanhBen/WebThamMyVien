using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class PostContentRepository : IPostContentRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public PostContentRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreatePostContent(PostContent postContent)
        {
            _context.Add(postContent);
            return Save();
        }

        public bool DeletePostContent(PostContent postContent)
        {
            postContent.IsDelete = true;
            postContent.DateDelete = sp.GetCurrentDate();
            _context.Update(postContent);
            return Save();
        }

        public ICollection<PostContent> GetAllPostContent()
        {
            return _context.PostContents.Where(c => c.IsDelete != true).ToList();
        }

        public PostContent GetPostContent(int id)
        {
            return _context.PostContents.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool PostContentExists(int id)
        {
            return _context.PostContents.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePostContent(PostContent postContent)
        {
            _context.Update(postContent);
            return Save();
        }
    }
}
