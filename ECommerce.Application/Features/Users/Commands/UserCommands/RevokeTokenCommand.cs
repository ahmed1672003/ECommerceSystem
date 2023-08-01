using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerce.Models.User.Auth;

namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record RevokeTokenCommand(string RefreshToken):IRequest<Response<RevokeTokenModel>>;
