global using System.Text.Json.Serialization;

global using ECommerce.Application;
global using ECommerce.Application.Features.Categories.Commands.CategoryCommands;
global using ECommerce.Application.Features.Categories.Queries.CategoryQueries;
global using ECommerce.Application.Features.Users.Commands.UserCommands;
global using ECommerce.Application.Features.Users.Queries.UserQueries;
global using ECommerce.Domain.Enums;
global using ECommerce.Infrastructure;
global using ECommerce.ViewModels.ViewModels.UserViewModels;

global using MediatR;

global using Microsoft.AspNetCore.Authentication.Negotiate;
global using Microsoft.AspNetCore.Mvc;
namespace ECommerce.API;

