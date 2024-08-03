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

        public async Task<IEnumerable<CommentDTO>> GetAllCommentAsync()
        {
            var posts = await _commentRepository.GetAllCommentsAsync();
            return posts.Select(p => new CommentDTO
            {
                Content = p.Content,
                CreatedAt = p.CreatedAt
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
                CreatedAt = post.CreatedAt
            };
        }

        public async Task AddCommentAsync(Guid id, CommentDTO postDto, Guid userId)
        {
            var post = new Comment
            {
                CommentId = Guid.NewGuid(),
                ThreadId = id,
                UserId = userId,
                Content = postDto.Content,
                CreatedAt = postDto.CreatedAt
            };
            await _commentRepository.AddCommentAsync(post);
        }

        public async Task UpdateCommentAsync(Guid id, CommentDTO postDto)
        {
            var post = new Comment
            {
                CommentId = id,
                Content = postDto.Content,
                CreatedAt = postDto.CreatedAt
            };
            await _commentRepository.UpdateCommentAsync(post);
        }

        public async Task DeleteAsync(Guid postId)
        {
            await _commentRepository.DeleteCommentAsync(postId);
        }
    }
}
