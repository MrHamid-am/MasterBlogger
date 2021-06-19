using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.Domain.ArticleCategoryAgg;

namespace MB.Domain.ArticleCategoryAgg.Services
{
    public interface IArticleCategoryValidatorService
    {
        void CheckArticleAlreadyExists(string title);
    }

    public class ArticleCategoryValidatorService : IArticleCategoryValidatorService
    {
        private readonly IArticleCategoryRepository _repository;

        public ArticleCategoryValidatorService(IArticleCategoryRepository repository)
        {
            _repository = repository;
        }

        public void CheckArticleAlreadyExists(string title)
        {
            if (_repository.Exists(title))
            {
                throw new Exception("Article already exists.");
            }
        }
    }
}
