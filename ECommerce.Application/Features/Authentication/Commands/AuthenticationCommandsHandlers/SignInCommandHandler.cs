namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommandsHandlers;
//public class SignInCommandHandler :
//    ResponseHandler,
//    IRequestHandler<SignInCommand, Response<string>>
//{
//    public SignInCommandHandler(IUnitOfWork context, IMapper mapper) : base(context, mapper) { }
//    public async Task<Response<string>>
//        Handle(SignInCommand request, CancellationToken cancellationToken)
//    {
//        // get user by userName or email
//        var user = await Context.Users.RetrieveAsync(u =>

//        // if emailOrUserName is valid search by email otherWise userName
//        new EmailAddressAttribute().IsValid(request.DTO.Password) ?

//        u.Email.Equals(request.DTO.EmailOrUserName) :

//        u.UserName.Equals(request.DTO.EmailOrUserName));

//        // check sign in success 
//        var result =
//            await Context.Users.SignInManager
//            .CheckPasswordSignInAsync(user, request.DTO.Password, false);

//        // sign in not success
//        if (!result.Succeeded)
//            return BadRequest<string>(message: "password is wrong !");

//        // generate new token


//        // return token
//    }
//}
