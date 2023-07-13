using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.CategoryMapper;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        Mapp();
    }
    private void Mapp()
    {
        CreateMap<PostCategoryViewModel, Category>().ReverseMap();
        CreateMap<Category, CategoryViewModel>().ReverseMap();
    }
}
