using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using System.Collections.Generic;


namespace JuicyNotesAPI.Services
{
    public interface ICollectionDbService
    {
        public IEnumerable <Collection> getAllCollections();
        public IEnumerable <Collection> getUserCollections(int idUser);
        public Collection getCollection(int idCollection);
        public Collection getCollection(string name);
        //public Collection updateCollection(CollectionUpdateRequest request);
        public bool deleteCollection(int idCollection);
        public bool deleteCollection(string name);
        public Collection addCollection(CollectionAddRequest request, User user);
    }
}
