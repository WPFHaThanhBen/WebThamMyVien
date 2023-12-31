﻿using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPostImageRepository
    {
        Task<ICollection<PostImageDto>> GetAllPostImage();
        Task<PostImageDto> GetPostImage(int id);
        Task<bool> CreatePostImage(PostImageDto PostImage);
        Task<bool> UpdatePostImage(PostImageDto PostImage);
        Task<bool> DeletePostImage(PostImageDto PostImage);

    }
}
