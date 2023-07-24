using ECommerce.Models.Category;

namespace ECommerce.Application.Features.Categories.CategoryMapper;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        Mapp();
    }
    private void Mapp()
    {
        CreateMap<PostCategoryModel, Category>().ReverseMap();
        CreateMap<Category, CategoryModel>().ReverseMap();
    }
}
