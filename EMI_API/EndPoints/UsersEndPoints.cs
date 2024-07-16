
using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs.Identity;
using EMI_API.Commons.Enums;
using EMI_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace EMI_API.EndPoints
{
    public static class UsersEndPoints
    {
        
        public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
        {
           
            group.MapPost("/register", Register)
                .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>();

            group.MapPost("/login", Login)
              .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>();

            group.MapPost("/addAdmin",AddAdmin).AddEndpointFilter<ValidationFilters<EditClaimDTO>>();

            group.MapPost("/removeAdmin", RemoveAdmin).AddEndpointFilter<ValidationFilters<EditClaimDTO>>();

            group.MapPost("/refreshToken", RefreshToken);

            return group;
        }

        private async static Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<IEnumerable<IdentityError>>>>
            Register(UserCredentialsDTO userCredentials, IUserService userService)
        {
            return await userService.Register(userCredentials);
        }

        private async static Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<string>>>
            Login(UserCredentialsDTO userCredentials, IUserService userService)
        {
            return await userService.Login(userCredentials);
        }
        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        private async static Task<Results<NoContent, NotFound>> 
            AddAdmin(EditClaimDTO editClaim, IUserService userService)
        {
            return await userService.AddAdmin(editClaim);
        }

        [Authorize(Roles = Roles.ADMIN)]
        private async static Task<Results<NoContent, NotFound>> 
            RemoveAdmin(EditClaimDTO editClaim, IUserService userService)
        {
            return await userService.RemoveAdmin(editClaim);
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        private async static Task<Results<Ok<AuthenticationResponseDTO>, NotFound>>
            RefreshToken( IUserService userService)
        {
            return await userService.RefreshToken();
        }

    }
}
