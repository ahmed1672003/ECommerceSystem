﻿namespace ECommerce.Application.Features.Authentication.Commands.AuthenticationCommandsHandlers;
public class SignInCommandHandler :
    ResponseHandler,
    IRequestHandler<SignInCommand, Response<string>>
{
    private readonly IAuthenticationServices authenticationServices;
    public SignInCommandHandler(IUnitOfWork context, IMapper mapper, IAuthenticationServices authenticationServices) : base(context, mapper)
    {
        this.authenticationServices = authenticationServices;
    }
    public async Task<Response<string>>
        Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        // get user by userName or email
        var user = await Context.Users.RetrieveAsync(u =>

        // if emailOrUserName is valid search by email otherWise userName
        new EmailAddressAttribute().IsValid(request.model.EmailOrUserName) ?

        u.Email.Equals(request.model.EmailOrUserName) :

        u.UserName.Equals(request.model.EmailOrUserName));

        // check sign in success 
        var result =
            await Context.Users.SignInManager
            .CheckPasswordSignInAsync(user, request.model.Password, false);

        // sign in not success
        if (!result.Succeeded)
            return BadRequest<string>(message: "password is wrong !");

        // generate new token
        var token = await authenticationServices.GetJWTTokenAsync(user);

        // return token
        return Success(token);
    }
}
