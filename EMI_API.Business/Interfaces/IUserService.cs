using EMI_API.Commons.DTOs.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_API.Business.Interfaces
{
    public interface IUserService
    {
        Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<IEnumerable<IdentityError>>>> Register(UserCredentialsDTO userCredentials);
        Task<Results<Ok<AuthenticationResponseDTO>, BadRequest<string>>> Login(UserCredentialsDTO userCredentials);
        Task<Results<NoContent, NotFound>> AddAdmin(EditClaimDTO editClaim);
        Task<Results<NoContent, NotFound>> RemoveAdmin(EditClaimDTO editClaim);
        Task<Results<Ok<AuthenticationResponseDTO>, NotFound>> RefreshToken();
        Task<IdentityUser?> GetUser();
    }
}
