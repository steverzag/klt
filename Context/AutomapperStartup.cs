using AutoMapper;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.ViewModel;

namespace FloraYFaunaAPI.Context
{
    public class AutomapperStartup
    {
        public static void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Metadata, MetadataViewModel>();
            config.CreateMap<Document, DocumentViewModel>();
            config.CreateMap<User, UserViewModel>();
            config.CreateMap<Gallery, GalleryViewModel>();
            config.CreateMap<BlogPost, BlogPostViewModel>();
            config.CreateMap<Category, CategoryViewModel>();
            config.CreateMap<Carousel, CarouselViewModel>();
            config.CreateMap<Newsletter, NewsletterViewModel>();
            config.CreateMap<Contact, ContactViewModel>();
            config.CreateMap<RefreshToken, AuthenticateResponse>();
        }
    }
}
