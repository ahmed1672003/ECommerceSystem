namespace ECommerce.Application.Features.Categories.CategoryMapper;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        Mapp();
    }

    private void Mapp()
    {
        CreateMap<PostCategoryDTO, Category>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}
