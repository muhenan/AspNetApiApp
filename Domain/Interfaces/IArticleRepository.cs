using AspNetApiApp.Domain.Entities;

namespace AspNetApiApp.Domain.Interfaces
{
    using System;
    public interface IArticleRepository
    {
        Task<Article> GetByIdAsync(int id);
        Task SaveAsync(Article article);
        int countOfArticles();

        Task<List<Article>> GetAllAsync();
    }
}
