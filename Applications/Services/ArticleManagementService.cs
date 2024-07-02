using AspNetApiApp.Applications.Dtos; // Add the appropriate namespace for ArticleDto
using AspNetApiApp.Domain.Interfaces; // Add the appropriate namespace for IArticleRepository
using AspNetApiApp.Domain.Entities; // Add the appropriate namespace for Article

namespace AspNetApiApp.Applications.Services
{
    public class ArticleManagementService
    {
        private readonly IArticleRepository _articleRepository;

        private readonly ILogger<ArticleManagementService> _logger;

        public ArticleManagementService(IArticleRepository articleRepository, ILogger<ArticleManagementService> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public async Task<ArticleDto> GetArticleAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null) return null;
            return new ArticleDto
            {
                Title = article.Title,
                Content = article.Content,
                PublicationDate = article.PublicationDate
            };
        }
        

        public async Task<ArticleDto> CreateArticleAsync(ArticleDto articleDto)
        {
            
            var new_id = _articleRepository.countOfArticles() + 1;
            _logger.LogInformation("Creating new article with ID: {Id}", new_id);
            var article = new Article(new_id, articleDto.Title, articleDto.Content, articleDto.PublicationDate);
            await _articleRepository.SaveAsync(article);
            return new ArticleDto
            {
                Title = article.Title,
                Content = article.Content,
                PublicationDate = article.PublicationDate
            };
        }


        public async Task<List<ArticleDto>> GetAllArticlesAsync()
        {
            var articles = await _articleRepository.GetAllAsync();
            return articles.Select(article => new ArticleDto
            {
                Title = article.Title,
                Content = article.Content,
                PublicationDate = article.PublicationDate
            }).ToList();
        }
    }
}