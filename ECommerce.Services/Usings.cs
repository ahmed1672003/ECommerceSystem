global using System.Collections.Concurrent;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;

global using ECommerce.Domain.Entities.IdentityEntities;
global using ECommerce.Services.Helpers.AuthenticationHelpers;
global using ECommerce.Services.Implementations;
global using ECommerce.Services.Interfaces;
global using ECommerce.ViewModels.ViewModels.AuthenticationViewModels;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Services;
