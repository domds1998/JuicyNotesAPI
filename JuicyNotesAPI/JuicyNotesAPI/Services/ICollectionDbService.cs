using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface ICollectionDbService
    {
        public Task<IActionResult> getAllCollections();
        public Task<IActionResult> getUserCollections(User user);
        public Task<IActionResult> getCollection(int idCollection);
        public Task<IActionResult> getCollection(string name, User user);
        public Task<IActionResult> updateCollection(CollectionUpdateRequest request, User user);
        public Task<IActionResult> deleteCollection(int idCollection);
        public Task<IActionResult> deleteCollection(string name, User user);
        public Task<IActionResult> addCollection(CollectionAddRequest request, User user);
    }
}
