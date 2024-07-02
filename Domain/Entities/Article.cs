namespace AspNetApiApp.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Article
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime PublicationDate { get; private set; }

        public Article(int id, string title, string content, DateTime publicationDate)
        {
            Id = id;
            Title = title;
            Content = content;
            PublicationDate = publicationDate;
        }
    }
}