﻿using ECommerce.Models.ResponsModels;

namespace ECommerce.Application.Features.Categories.Queries.CategoryQueries;
public record GetAllCategoriesQuery() : IRequest<Response<IEnumerable<CategoryModel>>>;
