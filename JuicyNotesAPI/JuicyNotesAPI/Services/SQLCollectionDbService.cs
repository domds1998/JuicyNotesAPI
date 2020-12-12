using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Collection newCollection = new Collection
            {
                Name = request.Name,
                CreationDate = request.CreationDate,
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

        public bool deleteCollection(string name)
        {
            Collection delete = _context.Collections.Where(
                c => c.Name == name
                ).FirstOrDefault();

            if (delete == null) return false;

            _context.Collections.Remove(delete);

            _context.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Collection> getAllCollections()
        {
            return _context.Collections.ToList();
        }

        public Collection getCollection(int idCollection)
        {
            Collection collection = _context.Collections.Where(
                c => c.IdCollection == idCollection
                ).FirstOrDefault();

            return collection;
        }

        public Collection getCollection(string name)
        {
            Collection collection = _context.Collections.Where(
                c => c.Name == name
                ).FirstOrDefault();

            return collection;
        }

        public IEnumerable<Collection> getUserCollections(int idUser)
        {
            IEnumerable<UserCollection> userCollections = _context.UserCollections.Where(
                    uc => uc.IdUser == idUser
                ).ToList();

            IEnumerable<Collection> collections = new List<Collection>();

            foreach (UserCollection uc in userCollections){
                Collection collection = getCollection(uc.IdCollection);
                if (collection != null) collections.Append(collection);
            }

            return collections;
        }

        //public Collection updateCollection(CollectionUpdateRequest request)
        //{
            
        //}
    }
}
