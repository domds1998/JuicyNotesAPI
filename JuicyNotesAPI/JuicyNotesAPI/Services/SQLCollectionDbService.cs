using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public class SQLCollectionDbService : ICollectionDbService
    {
        private readonly JuicyDBContext _context;
        

        public SQLCollectionDbService(JuicyDBContext  context) {
            _context = context;
        }

        public Collection addCollection(CollectionAddRequest request, User user)
        {
            if (getCollection(request.Name, user) != null) return null;
            Collection newCollection = new Collection
            {
                Name = request.Name,
                CreationDate = DateTime.Now,
                Color = request.Color
            };

            _context.Collections.AddAsync(newCollection);

            _context.SaveChangesAsync();

            UserCollection newUserCollection = new UserCollection
            {
                IdUser = user.IdUser,
                IdCollection = newCollection.IdCollection
            };

            _context.UserCollections.AddAsync(newUserCollection);

            _context.SaveChangesAsync();

            return newCollection;
        }

        public bool deleteCollection(int idCollection)
        {
            Collection delete = _context.Collections.Where(
                c => c.IdCollection == idCollection
                ).FirstOrDefault();

            if (delete == null) return false;

            _context.Collections.Remove(delete);

            _context.SaveChangesAsync();

            return true;
        }

        public bool deleteCollection(string name, User user)
        {
            Collection delete = _context.Collections.Where(
                c => c.Name == name
                ).FirstOrDefault();

            if (delete == null) return false;

            _context.Collections.Remove(delete);

            _context.SaveChangesAsync();

            return true;
        }

        public async Task<IActionResult> getAllCollections()
        {
            return new OkObjectResult(_context.Collections.ToList());
        }

        public Collection getCollection(int idCollection)
        {
            Collection collection = _context.Collections.Where(
                c => c.IdCollection == idCollection
                ).FirstOrDefault();

            return collection;
        }

        public Collection getCollection(string name, User user)
        {
            var queryCollection = _context.UserCollections.Join(
                _context.Collections,
                userCollection => userCollection.IdCollection,
                collection => collection.IdCollection,
                (userCollection, collection) => new
                {
                    idUser = userCollection.IdUser,
                    idCollection = userCollection.IdCollection,
                    collectionName = collection.Name
                }
                ).Where(
                    x => x.collectionName == name && x.idUser == user.IdUser
                ).FirstOrDefault();

            if (queryCollection == null) return null;

            var collection = _context.Collections.Where(
                    c => c.IdCollection == queryCollection.idCollection
                ).FirstOrDefault();
        
            return collection;
        }

        public async Task<IActionResult> getUserCollections(User user)
        {
            IEnumerable<UserCollection> userCollections = _context.UserCollections.Where(
                    uc => uc.IdUser == user.IdUser
                ).ToList();

            IEnumerable<Collection> collections = new List<Collection>();

            foreach (UserCollection uc in userCollections){
                var collection = getCollection(uc.IdCollection);
                if (collection != null) collections.Append(collection);
            }

            return new OkObjectResult(collections);
        }

        public Collection updateCollection(CollectionUpdateRequest request, User user)
        {
            var collection = getCollection(request.Name, user);

            if (collection == null) return null;

            collection.Name = request.NewName;
            collection.Color = request.Color;

            _context.SaveChangesAsync();

            return collection;
        }
    }
}
