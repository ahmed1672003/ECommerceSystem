﻿global using System.Linq.Expressions;

global using ECommerce.Domain.Entities;
global using ECommerce.Domain.Entities.IdentityEntities;
global using ECommerce.Domain.Enums.Identity.Role;
global using ECommerce.Domain.IRepositories;
global using ECommerce.Domain.IRepositories.IIdentityRepositories;
global using ECommerce.Infrastructure.Context;
global using ECommerce.Infrastructure.Repositories;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
namespace ECommerce.Infrastructure;

