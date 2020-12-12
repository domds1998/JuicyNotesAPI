﻿using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.DTOs.Responses;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JuicyNotesAPI.Services
{
    public class SQLUserDbService : IUserDbService
    {
        private readonly JuicyDBContext _context;
        private readonly JWTSettings _jwtSettings;

        public SQLUserDbService(JuicyDBContext  context, IOptions<JWTSettings> jwtSettings) {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public AuthenticateResponse authenticate(AuthenticateRequest request)
        {
            var username = request.Username;
            
             var user = _context.Users.Where(
                        u => u.Email == username || u.Username == username
                    ).FirstOrDefault();
            

            if (user == null) return null;

            var password = encodePassword(request.Password, user.Salt).hash;

            if (password != user.Password) return new AuthenticateResponse(user, null);
            return new AuthenticateResponse(user, generateJWTtoken(user));
        }

        public User register(RegistrationRequest request)
        {
            EncodedPassword password = encodePassword(request.Password, generateRandomSalt32());

            User newUser = new User {
                Username = request.Username,
                Email = request.Email,
                Password = password.hash,
                Salt = password.salt
            };

            _context.Users.Add(newUser);

            _context.SaveChanges();

            return newUser;

        }

        public bool deleteUser(int id)
        {
            User delete = _context.Users.Where(
                    u => u.IdUser == id
                ).FirstOrDefault();

            if (delete == null) return false;

            _context.Users.Remove(delete);

            _context.SaveChanges();

            return true;
        }

        public User getUser(int id)
        {

            
            User user = _context.Users.Where(
                    u => u.IdUser == id
                ).FirstOrDefault();

            return user;
        }

        public User getUserMail(string mail)
        {
            User user = _context.Users.Where(
                    u => u.Email == mail
                ).FirstOrDefault();

            return user;
        }

        public IEnumerable<User> getUsers()
        {
            return _context.Users.ToList<User>();
        }

        public User getuserUsername(string username)
        {
            User user = _context.Users.Where(
                    u => u.Username == username
                ).FirstOrDefault();

            return user;
        }

        

        /*public User updateUser(User user)
        {
            
        }*/

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

        private string generateJWTtoken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("IdUser", user.IdUser.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
