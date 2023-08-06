//using ECommerce.Application.Features.Users.Commands.UserCommands;

//namespace ECommerce.Application.Features.Users.Commands.UserCommandsHandlers;
//public class RevokeTokenCommandHandler :
//    ResponseHandler,
//    IRequestHandler<RevokeTokenCommand, Response<RevokeTokenModel>>
//{
//    public RevokeTokenCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper)
//    {
//    }

//    public Task<Response<RevokeTokenModel>>
//        Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
//    {
//        //if (string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrWhiteSpace(request.RefreshToken))
//        //    return BadRequest(new RevokeTokenModel());

//        //var revokeTokenModel = new RevokeTokenModel()
//        //{
//        //    IsRevoked = await _authService.RevokeTokenAsync(request.RefreshToken)
//        //};

//        //if (!revokeTokenModel.IsRevoked)
//        //    return BadRequest(new RevokeTokenModel());

//        //return Success(revokeTokenModel);

//        throw new NotImplementedException();
//    }
//}
