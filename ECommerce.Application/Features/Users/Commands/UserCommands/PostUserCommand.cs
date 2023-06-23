namespace ECommerce.Application.Features.Users.Commands.UserCommands;
public record PostUserCommand(PostUserDTO DTO) : IRequest<Response<UserDTO>>;

