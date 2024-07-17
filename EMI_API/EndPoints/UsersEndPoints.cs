
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
        private static readonly string entity = "User";
        public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
        {

            group.MapPost("/register", Register)
                .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>()
                .CreateDocumentation(entity);

            group.MapPost("/login", Login)
              .AddEndpointFilter<ValidationFilters<UserCredentialsDTO>>()
               .WithOpenApiDocumentation(
                    summary: $"Login del usuario del aplicativo",
                    description: $"Servicio que permite realizar login en el aplicativo y obtener token",
                    paramDescription: $"",
                    requestBodyDescription: "Credenciales del usuario"
                ); 

            group.MapPost("/addAdmin",AddAdmin).AddEndpointFilter<ValidationFilters<EditClaimDTO>>()
                .WithOpenApiDocumentation(
                    summary: $"Asigna rol Admin",
                    description: $"Asigna el rol de Admin a un usuario del sistema",
                    paramDescription: $"",
                    requestBodyDescription: "email del usuario"
                ); 

            group.MapPost("/removeAdmin", RemoveAdmin).AddEndpointFilter<ValidationFilters<EditClaimDTO>>()
                 .WithOpenApiDocumentation(
                    summary: $"Remover rol Admin",
                    description: $"Remueve el rol de Admin a un usuario del sistema",
                    paramDescription: $"",
                    requestBodyDescription: "email del usuario"
                ); ;

            group.MapPost("/refreshToken", RefreshToken)
                 .WithOpenApiDocumentation(
                    summary: $"Renovar Token",
                    description: $"Renueva el token de un usuario con token no expirado",
                    paramDescription: $"",
                    requestBodyDescription: ""
                ); 

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
