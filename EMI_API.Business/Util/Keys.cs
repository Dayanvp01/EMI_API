using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EMI_API.Business.Util
{
    public static class Keys
    {
        public const string IssuerPropio = "emi-api";
        private const string KeySection = "Authentication:Schemes:Bearer:SigningKeys";
        private const string KeysSection_Issuer = "Issuer";
        private const string KeysSetion_Value = "Value";

        /// <summary>
        /// Obtiene la llave de emisor configurado para la aplicacion
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IEnumerable<SecurityKey> GetKey(IConfiguration configuration)
            => GetKey(configuration, IssuerPropio);

        /// <summary>
        /// Otiene la key por emisor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="issuer">nombre del emisor</param>
        /// <returns>retorna la llave del proveedor</returns>
        public static IEnumerable<SecurityKey> GetKey(IConfiguration configuration, string issuer)
        {
            var signingKey = configuration.GetSection(KeySection)
                .GetChildren()
                .SingleOrDefault(k => k[KeysSection_Issuer] == issuer);
            if (signingKey is not null && signingKey[KeysSetion_Value] is string keyValue)
            {
                yield return new SymmetricSecurityKey(Convert.FromBase64String(keyValue));
            }
        }


        /// <summary>
        /// Obtiene todas las llaves
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IEnumerable<SecurityKey> GetAllKeys(IConfiguration configuration)
        {
            var signingKeys = configuration.GetSection(KeySection)
                .GetChildren();

            foreach (var signingKey in signingKeys)
            {
                if (signingKey[KeysSetion_Value] is string keyValue)
                {
                    yield return new SymmetricSecurityKey(Convert.FromBase64String(keyValue));
                }
            }
        }

    }

}
