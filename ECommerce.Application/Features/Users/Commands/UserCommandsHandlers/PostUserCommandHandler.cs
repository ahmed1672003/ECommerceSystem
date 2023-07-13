using ECommerce.Application.Features.Users.Commands.UserCommands;
using ECommerce.ViewModels.ViewModels.UserViewModels;

namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
public class PostUserCommandHandler :
    ResponseHandler,
    IRequestHandler<PostUserCommand, Response<UserViewModel>>
{
    public PostUserCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }

    public async Task<Response<UserViewModel>>
        Handle(PostUserCommand request, CancellationToken cancellationToken)
    {

        var model = Mapper.Map<User>(request.model);
        var state = await Context.Users.Manager.CreateAsync(model, request.model.Password);

        if (!state.Succeeded)
            return BadRequest<UserViewModel>(errors: state.Errors);


        return Success<UserViewModel>();
    }
}
