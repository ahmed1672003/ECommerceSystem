using ECommerce.Application.Features.Users.Commands.UserCommands;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class PostUserCommandHandler :
    ResponseHandler,
    IRequestHandler<PostUserCommand, Response<UserDTO>>
{
    public PostUserCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<UserDTO>>
        Handle(PostUserCommand request, CancellationToken cancellationToken)
    {

        var model = Mapper.Map<User>(request.DTO);
        var state = await Context.Users.Manager.CreateAsync(model, request.DTO.Password);

        if (!state.Succeeded)
            return BadRequest<UserDTO>(errors: state.Errors);


        return Success<UserDTO>();
    }
}
