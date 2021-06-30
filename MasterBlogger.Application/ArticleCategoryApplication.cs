﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _01_Framework.Infrastructure;
using MB.Application.Contracts.ArticleCategory;
using MB.Domain.ArticleCategoryAgg;
using MB.Domain.ArticleCategoryAgg.Services;

namespace MB.Application
{
    public class ArticleCategoryApplication:IArticleCategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleCategoryValidatorService _validatorService;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IArticleCategoryValidatorService validatorService, IUnitOfWork unitOfWork)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _validatorService = validatorService;
            _unitOfWork = unitOfWork;
        }

        public List<ArticleCategoryViewModel> List()
        {
            var articleCategories = _articleCategoryRepository.GetAll();

            return articleCategories.Select(articleCategory => new ArticleCategoryViewModel
            {
                Id = articleCategory.Id,
                Title = articleCategory.Title,
                IsDeleted = articleCategory.IsDeleted,
                CreationDate = articleCategory.CreationDate.ToString(CultureInfo.InvariantCulture)
            }).OrderByDescending(x=>x.Id).ToList();
        }

        public void Create(CreateArticleCategory command)
        {
            _unitOfWork.BeginTransaction();

            var articleCategory = new ArticleCategory(command.Title,_validatorService);
            _articleCategoryRepository.Create(articleCategory);

            _unitOfWork.CommitTransaction();
        }

        public void Rename(RenameArticleCategory command)
        {
            _unitOfWork.BeginTransaction();

            var articleCategory = _articleCategoryRepository.Get(command.Id); 
            articleCategory.Rename(command.Title);

            _unitOfWork.CommitTransaction();
        }

        public RenameArticleCategory Get(long id)
        {
            var articleCategory = _articleCategoryRepository.Get(id);
            return new RenameArticleCategory()
            {
                Id = articleCategory.Id,
                Title = articleCategory.Title,
            };
        }

        public void Remove(long id)
        {
            _unitOfWork.BeginTransaction();

            var articleCategory = _articleCategoryRepository.Get(id);
            articleCategory.Remove();

            _unitOfWork.CommitTransaction();
        }

        public void Activate(long id)
        {
            _unitOfWork.BeginTransaction();
            
            var articleCategory = _articleCategoryRepository.Get(id);
            articleCategory.Activate();

            _unitOfWork.CommitTransaction();
        }
    }
}
