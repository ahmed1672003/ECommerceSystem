global using System.Net;
global using System.Reflection;
global using System.Text.Json;

global using AutoMapper;

global using ECommerce.Application.Features.Categories.CategoryDTOs;
global using ECommerce.Application.Features.Categories.Commands.CategoryCommands;
global using ECommerce.Application.Features.Categories.Queries.CategoryQueries;
global using ECommerce.Application.Responses.Constants;
global using ECommerce.Application.Responses.IResponseServices;
global using ECommerce.Application.Responses.ResponseServices;
global using ECommerce.Domain.Entities;
global using ECommerce.Domain.Enums;
global using ECommerce.Domain.IRepositories;

global using FluentValidation;

global using MediatR;

global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;
