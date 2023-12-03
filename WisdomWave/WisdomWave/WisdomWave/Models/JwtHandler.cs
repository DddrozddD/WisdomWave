using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WisdomWave.Models
{
    public class JwtHandler
    {

        public static string GenerateJwtToken(WwUser user)
        {
            var claims = new List<Claim>
          {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName)
                
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"+user.Id));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(7); 

            var token = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GenerateJwtToken(Course course)
        {
            var claims = new List<Claim>
          {
        new Claim("course_id", course.Id.ToString())

            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("creatingCourseId"+course.Id));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static List<Claim> DecodeJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenParts = jwtToken.Split('.');

            if (jwtTokenParts.Length != 3)
            {
                throw new ArgumentException("Invalid JWT token format");
            }

            var base64Payload = jwtTokenParts[1];
            var payload = JwtHandler.Base64UrlDecode(base64Payload);

            var token = tokenHandler.ReadJwtToken(jwtToken);

            return token.Claims.ToList();

        }

        private static string Base64UrlDecode(string input)
        {
            var paddingNeeded = input.Length % 4;
            if (paddingNeeded > 0)
            {
                input += new string('=', 4 - paddingNeeded);
            }

            input = input.Replace('-', '+').Replace('_', '/');
            var byteArray = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }
    }
}
