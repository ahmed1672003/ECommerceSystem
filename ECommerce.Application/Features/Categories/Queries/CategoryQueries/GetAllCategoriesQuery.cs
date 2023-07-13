﻿using ECommerce.ViewModels.ViewModels.CategoryViewModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record GetAllCategoriesQuery() : IRequest<Response<IEnumerable<CategoryViewModel>>>;
