﻿namespace BlogSystem.Web.ViewModels.Posts
{
    using System;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Infrastructure.Helpers.Url;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        private readonly IUrlGenerator urlGenerator;

        public PostViewModel()
        {
            this.urlGenerator = DependencyResolver.Current.GetService<IUrlGenerator>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreatedOn { get; set; }

        public string Url => this.urlGenerator.ToUrl(this.Id, this.Title, this.CreatedOn);
        
        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostViewModel>()
                .ForMember(m => m.Author, c => c.MapFrom(post => post.Author.UserName));
        }
    }
}