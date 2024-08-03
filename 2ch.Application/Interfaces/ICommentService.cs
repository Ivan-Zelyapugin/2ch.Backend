﻿using _2ch.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllCommentAsync();
        Task<CommentDTO> GetCommentByIdAsync(Guid postId);
        Task AddCommentAsync(Guid id, CommentDTO postDto, Guid userId);
        Task UpdateCommentAsync(Guid id, CommentDTO postDto);
        Task DeleteAsync(Guid postId);
    }
}
