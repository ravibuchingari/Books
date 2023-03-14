using AutoMapper;

namespace Books.Models
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<BookDM, BookVM>().ReverseMap();
        }
    }
}
