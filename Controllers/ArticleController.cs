namespace AspNetApiApp.Controllers
{
    using System.Threading.Tasks;
    using AspNetApiApp.Applications.Dtos;
    using AspNetApiApp.Applications.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleManagementService _articleService;

        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ArticleManagementService articleService, ILogger<ArticleController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Getting article with ID: {Id}", id);
            var articleDto = await _articleService.GetArticleAsync(id);
            if (articleDto == null) return NotFound();
            return Ok(articleDto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ArticleDto articleDto)
        {
            _logger.LogInformation("Creating new article");
            var createdArticle = await _articleService.CreateArticleAsync(articleDto);
            return Ok(createdArticle);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all articles");
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles);
        }
    }
}