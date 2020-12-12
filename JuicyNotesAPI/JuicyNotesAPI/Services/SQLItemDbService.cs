
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public class SQLItemDbService : IItemDbService
    {
        private readonly JuicyDBContext _context;

        public SQLItemDbService(JuicyDBContext context)
        {
            _context = context;
        }

        public Item addItem(AddingItemRequest request) {

            var item = new Item
            {
                Title = request.Title,
                Content = request.Content,
                CreationDate = DateTime.Now,
                KnowledgeRating = request.KnowledgeRating,
                Type = request.Type
            };

            _context.Items.Add(item);
            _context.SaveChanges();

            return item;
        }

        public bool deleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public Item updateItem(UpdateItemRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
