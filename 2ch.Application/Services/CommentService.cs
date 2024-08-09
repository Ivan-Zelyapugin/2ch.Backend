using _2ch.Application.DTOs;
using _2ch.Application.Interfaces;
using _2ch.Application.Repositories;
using _2ch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository) =>
            _commentRepository = commentRepository;

        public async Task<IEnumerable<Comment>> GetAllCommentAsync(Guid threadId)
        {
            var posts = await _commentRepository.GetAllCommentsAsync(threadId);
            return posts.Select(p => new Comment
            {
                CommentId = p.CommentId,
                ThreadId = p.ThreadId,
                UserId = p.UserId,
                FilePath = p.FilePath,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                ParentCommentId = p.ParentCommentId
            });
        }

        public async Task<CommentDTO> GetCommentByIdAsync(Guid postId)
        {
            var post = await _commentRepository.GetCommentByIdAsync(postId);
            if (post == null)
            {
                return null;
            }
            return new CommentDTO
            {
                Content = post.Content,
            };
        }

        public async Task AddCommentAsync(Guid id, CommentDTO postDto, Guid userId)
        {
            var post = new Comment
            {
                CommentId = Guid.NewGuid(),
                ThreadId = id,
                ParentCommentId = postDto.ParentCommentId,
                UserId = userId,
                Content = postDto.Content,
                CreatedAt = DateTime.UtcNow,
                FilePath = postDto.FilePath
            };
            await _commentRepository.AddCommentAsync(post);
        }

        public async Task UpdateCommentAsync(Guid id, CommentDTO postDto)
        {
            var post = new Comment
            {
                CommentId = id,
                Content = postDto.Content,
            };
            await _commentRepository.UpdateCommentAsync(post);
        }

        public async Task DeleteAsync(Guid postId)
        {
            await _commentRepository.DeleteCommentAsync(postId);
        }
    }
}
