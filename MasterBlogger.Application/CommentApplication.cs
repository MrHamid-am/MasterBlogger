﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.Comment;
using MB.Domain.CommentAgg;

namespace MB.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddComment command)
        {
            _unitOfWork.BeginTransaction();

            var comment = new Comment(command.Name, command.Email, command.Message, command.ArticleId);
            _commentRepository.Create(comment);

            _unitOfWork.CommitTransaction();
        }

        public List<CommentViewModel> GetList()
        {
            return _commentRepository.GetList();
        }

        public void Confirm(long id)
        {
            _unitOfWork.BeginTransaction();

            var comment = _commentRepository.Get(id);
            comment.Confirm();

            _unitOfWork.CommitTransaction();
        }

        public void Cancel(long id)
        {
            _unitOfWork.BeginTransaction();

            var comment = _commentRepository.Get(id);
            comment.Cancel();

            _unitOfWork.CommitTransaction();
        }
    }
}
