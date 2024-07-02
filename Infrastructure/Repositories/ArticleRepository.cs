namespace AspNetApiApp.Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;
    using AspNetApiApp.Domain.Entities;
    using AspNetApiApp.Domain.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    public class ArticleRepository : IArticleRepository
    {
        
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "articles.json");
        private List<Article> _articles;
        // private List<Article> _articles = new List<Article>();

        public int countOfArticles()
        {
            return _articles.Count;
        }

        public ArticleRepository()
        {
            _articles = LoadArticles();
        }

        private List<Article> LoadArticles()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Article>();
            }

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Article>>(jsonString) ?? new List<Article>();
        }

        public Task<Article?> GetByIdAsync(int id)
        {
            return Task.FromResult<Article?>(_articles.FirstOrDefault(x => x.Id == id));
        }

        public Task SaveAsync(Article article)
        {
            _articles.Add(article);
            SaveChanges();
            return Task.CompletedTask;
        }

        public Task<List<Article>> GetAllAsync()
        {
            return Task.FromResult(_articles);
        }

        private void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_articles, options);

            Console.WriteLine(jsonString);

            try
            {
                File.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred while saving changes: {ex.Message}");
                // You can choose to rethrow the exception or handle it in a different way
                throw;
            }
        }
    }
}