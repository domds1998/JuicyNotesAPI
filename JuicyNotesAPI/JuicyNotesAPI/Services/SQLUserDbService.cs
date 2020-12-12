using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.DTOs.Responses;
using JuicyNotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JuicyNotesAPI.Services
{
    public class SQLUserDbService : IUserDbService
    {
        public AuthenticateResponse authenticate(AuthenticateRequest request)
        {
            var emailString = request.Username;
            bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            //var user;


            return null;
        }

        public bool deleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User getUser(int id)
        {
            throw new NotImplementedException();
        }

        public User getUserMail(string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> getUsers()
        {
            throw new NotImplementedException();
        }

        public User getuserUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public User register(User user)
        {
            throw new NotImplementedException();
        }

        public User updateUser(int id)
        {
            throw new NotImplementedException();
        }

        private EncodedPassword encodePassword(string password, string salt)
        {
            var encodedPassword = $"{password}{salt}";

            var bytes = Encoding.UTF8.GetBytes(encodedPassword);
            using (SHA256 sha = new SHA256Managed())
            {
                var hashedPasswordBytes = sha.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedPasswordBytes.Length; i++)
                {
                    builder.Append(hashedPasswordBytes[i].ToString("x2"));
                }

                EncodedPassword finalPass = new EncodedPassword()
                {
                    salt = salt,
                    hash = builder.ToString()
                };
                return finalPass;
            }

        }




        private string generateRandomSalt32()
        {
            string salt;
            var randomNumbers = new byte[24];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumbers);
                salt = Convert.ToBase64String(randomNumbers);
            }

            return salt;
        }

        private string generateJWTtoken(Person person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("idPerson", person.IdPeople.ToString()) }),
                Expires = DateTime.UtcNow.Add(_JwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
