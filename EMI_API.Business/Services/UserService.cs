using AutoMapper;
using EMI_API.Business.Interfaces;
using EMI_API.Business.Util;
using EMI_API.Commons.DTOs.Identity;
using EMI_API.Commons.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace EMI_API.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
                            IMapper mapper,IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.httpContextAccessor= httpContextAccessor;
            this.mapper = mapper;

        }

        /// <summary>
        /// Servicio para registrar un nuevo usuario
        /// </summary>
        /// <param name="userCredentials">credenciales del nuevo usuario</param>
        /// <returns></returns>
        public async Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<IEnumerable<IdentityError>>>> Register(UserCredentialsDTO userCredentials)
        {
            var user = mapper.Map<IdentityUser>(userCredentials);
            var resultado = await userManager.CreateAsync(user, userCredentials.Password);
            if (resultado.Succeeded)
            {
                await IsCreatedRole(Roles.USER);
                await userManager.AddToRoleAsync(user, Roles.USER);
                var credencialesResult = await BuildToken(userCredentials);
                return TypedResults.Ok(credencialesResult);
            }
            else
            {
                return TypedResults.BadRequest(resultado.Errors);
            }
        }

        private async Task IsCreatedRole(string role)
        {
            // Verifica si el rol existe, si no, lo crea
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        /// <summary>
        /// Servicio de login para un usuario registrado
        /// </summary>
        /// <param name="userCredentials">credenciales del usuario</param>
        /// <returns></returns>
        public async Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<string>>> Login(UserCredentialsDTO userCredentials)
        {
            var usuario = await userManager.FindByEmailAsync(userCredentials.Email);

            if (usuario is null)
            {
                return TypedResults.BadRequest("Login Incorrecto");
            }
            var resultado = await signInManager.CheckPasswordSignInAsync(usuario, userCredentials.Password, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var respuestaAutenticacion = await BuildToken(userCredentials);
                return TypedResults.Ok(respuestaAutenticacion);
            }
            else
            {
                return TypedResults.BadRequest("Login Incorrecto");
            }
        }
       
        /// <summary>
        /// Agrega el rol de admin a un usuario
        /// </summary>
        /// <param name="editClaim">objeto con el email del usuario</param>
        /// <returns></returns>
        public async Task<Results<NoContent, NotFound>> AddAdmin(EditClaimDTO editClaim)
        {
            var user = await userManager.FindByEmailAsync(editClaim.Email);
            if (user is null)
            {
                return TypedResults.NotFound();
            }

            await IsCreatedRole(Roles.ADMIN);
            await userManager.AddToRoleAsync(user, Roles.ADMIN);
            
            await userManager.AddToRoleAsync(user, Roles.ADMIN);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Remueve el rol de admin a un usuario
        /// </summary>
        /// <param name="editClaim">objeto con el email del usuario</param>
        /// <returns></returns>
        public async Task<Results<NoContent, NotFound>> RemoveAdmin(EditClaimDTO editClaim)
        {
            var user = await userManager.FindByEmailAsync(editClaim.Email);
            if (user is null)
            {
                return TypedResults.NotFound();
            }
            await userManager.RemoveFromRoleAsync(user, Roles.ADMIN);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Servicio para refrescar el token cuando este expirando
        /// </summary>
        /// <returns>nuevo token</returns>
        public async Task<Results<Ok<AuthenticationResponseDTO>, NotFound>> RefreshToken()
        {
            var usuario = await GetUser();
            if (usuario is null)
            {
                return TypedResults.NotFound();
            }
            var userCredentials = new UserCredentialsDTO { Email = usuario.Email! };

            var authenticationResponse = await BuildToken(userCredentials);
            return TypedResults.Ok(authenticationResponse);
        }
        
        /// <summary>
        /// Obtiene un usuario apartir de los claims
        /// </summary>
        /// <returns>deveulve el usuario por el claim de email</returns>
        public async Task<IdentityUser?> GetUser()
        {
            var emailClaim = httpContextAccessor.HttpContext!.
                        User.Claims.Where(x => x.Type == "email").FirstOrDefault();
            if (emailClaim is null)
            {
                return null;
            }
            var email = emailClaim.Value;
            return await userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Metodo para construir el token a devolver en los casos de login o register
        /// </summary>
        /// <param name="userCredentials">credenciales del usuario</param>
        /// <returns>objeto con token y tiempo de expiracion</returns>
        private async Task<AuthenticationResponseDTO> BuildToken(UserCredentialsDTO userCredentials)
        {
            var claims = new List<Claim>
            {
                new Claim("email", userCredentials.Email)
            };

            var usuario = await userManager.FindByEmailAsync(userCredentials.Email);
            var roles = await userManager.GetRolesAsync(usuario!);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();


            claims.AddRange(roleClaims);

            var key = Keys.GetKey(configuration);
            var creds = new SigningCredentials(key.First(), SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddHours(2);

            var tokenSeguridad = new JwtSecurityToken(null,  null,  claims, DateTime.Now,
                expires: expiracion, signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);

            return new AuthenticationResponseDTO
            {
                Token = token,  
                Expiration = expiracion
            };
        }
    
    }
}
