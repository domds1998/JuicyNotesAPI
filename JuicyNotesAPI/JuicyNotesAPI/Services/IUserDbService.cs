using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.DTOs.Responses;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface IUserDbService
    {
        //public User register(User user);
        public AuthenticateResponse authenticate(AuthenticateRequest request);
        //public IEnumerable<User> getUsers();
        //public User getUser(int id);
        //public User getUserMail(string mail);
        //public User getuserUsername(string userName);
        //public bool deleteUser(int id);
        //public User updateUser(int id);
        

    }
}
