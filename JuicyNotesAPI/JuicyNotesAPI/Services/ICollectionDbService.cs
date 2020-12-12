using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface ICollectionDbService
    {
        public Task<IActionResult> getAllCollections();
        public Task<IActionResult> getUserCollections(User user);
        public Collection getCollection(int idCollection);
        public Collection getCollection(string name, User user);
        public Collection updateCollection(CollectionUpdateRequest request, User user);
        public bool deleteCollection(int idCollection);
        public bool deleteCollection(string name, User user);
        public Collection addCollection(CollectionAddRequest request, User user);
    }
}
