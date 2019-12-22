using System.Security.Cryptography;
#pragma warning disable CS0234 // Тип или имя пространства имен "IdentityModel" не существует в пространстве имен "Microsoft" (возможно, отсутствует ссылка на сборку).
using Microsoft.IdentityModel.Tokens;
#pragma warning restore CS0234 // Тип или имя пространства имен "IdentityModel" не существует в пространстве имен "Microsoft" (возможно, отсутствует ссылка на сборку).

    public class SigningConfigurations
    {
#pragma warning disable CS0246 // Не удалось найти тип или имя пространства имен "SecurityKey" (возможно, отсутствует директива using или ссылка на сборку).
        public SecurityKey Key { get; }
#pragma warning restore CS0246 // Не удалось найти тип или имя пространства имен "SecurityKey" (возможно, отсутствует директива using или ссылка на сборку).
#pragma warning disable CS0246 // Не удалось найти тип или имя пространства имен "SigningCredentials" (возможно, отсутствует директива using или ссылка на сборку).
        public SigningCredentials SigningCredentials { get; }
#pragma warning restore CS0246 // Не удалось найти тип или имя пространства имен "SigningCredentials" (возможно, отсутствует директива using или ссылка на сборку).
 
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
 
            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
