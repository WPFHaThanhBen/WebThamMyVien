using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class PostTypeRepository : IPostTypeRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public PostTypeRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreatePostType(PostType postType)
        {
            _context.Add(postType);
            return Save();
        }

        public bool DeletePostType(PostType postType)
        {
            postType.IsDelete = true;
            postType.DateDelete = sp.GetCurrentDate();
            _context.Update(postType);
            return Save();
        }

        public ICollection<PostType> GetAllPostType()
        {
            return _context.PostTypes.Where(c => c.IsDelete != true).ToList();
        }

        public PostType GetPostType(int id)
        {
            return _context.PostTypes.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public PostType GetPostTypeByName(string name)
        {
            return _context.PostTypes.Where(e => e.TypeName == name && e.IsDelete != true).FirstOrDefault();
        }

        public bool PostTypeExists(int id)
        {
            return _context.PostTypes.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePostType(PostType postType)
        {
            _context.Update(postType);
            return Save();
        }
    }
}
