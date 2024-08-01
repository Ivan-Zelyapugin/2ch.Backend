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
                ThreadId = p.ThreadId,
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
                ThreadId = post.ThreadId,
                Content = post.Content,
                CreatedAt = post.CreatedAt
            };
        }

        public async Task AddCommentAsync(CommentDTO postDto)
        {
            var post = new Comment
            {
                CommentId = Guid.NewGuid(),
                ThreadId = postDto.ThreadId,
                Content = postDto.Content,
                CreatedAt = postDto.CreatedAt
            };
            await _commentRepository.AddCommentAsync(post);
        }

        public async Task UpdateCommentAsync(CommentDTO postDto)
        {
            var post = new Comment
            {
                ThreadId = postDto.ThreadId,
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
