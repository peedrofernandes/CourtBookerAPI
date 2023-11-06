//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Cryptography;
//using System.Text;

//namespace CourtBooker.Middlewares
//{
//    public class JwtAuthenticationMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly string _secret;

//        public JwtAuthenticationMiddleware(RequestDelegate next, string secret)
//        {
//            _next = next;
//            _secret = secret;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//            if (token != null)
//            {
//                try
//                {
//                    var tokenHandler = new JwtSecurityTokenHandler();
//                    var key = Encoding.ASCII.GetBytes(_secret);
//                    tokenHandler.ValidateToken(token, new TokenValidationParameters
//                    {
//                        ValidateIssuerSigningKey = true,
//                        IssuerSigningKey = new SymmetricSecurityKey(key),
//                        ValidateIssuer = false,
//                        ValidateAudience = false,
//                        ClockSkew = TimeSpan.Zero
//                    }, out SecurityToken validatedToken);
//                }
//                catch
//                {
//                    context.Response.StatusCode = 401; // Unauthorized
//                    await context.Response.WriteAsync("Token inválido");
//                    return;
//                }
//            }

//            await _next(context);
//        }
//    }

//}
