
using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<Item> addItem(AddingItemRequest request) {
            
            //TODO check if it there is note with the same title

            var item = new Item
            {
                Title = request.Title,
                Content = request.Content,
                CreationDate = DateTime.Now,
                KnowledgeRating = request.KnowledgeRating,
                Type = request.Type
            };

            _context.Items.AddAsync(item);
            _context.SaveChangesAsync();

            _context.CollectionItems.AddAsync(new CollectionItem{ 
                IdItem = item.IdItem,
                IdCollection = request.IdCollection
            });;

            _context.SaveChangesAsync();

            return item;
        }

        public Item deleteItem(int id)
        {
            var item = _context.Items.Where(i => i.IdItem == id).FirstOrDefault();

            if (item == null) return item;

            _context.Items.Remove(item);
            _context.SaveChanges();

            return item;
        }

        public Item updateItem(UpdateItemRequest request)
        {
            var item = _context.Items.Where(i => i.IdItem == request.IdItem).FirstOrDefault();

            if (item == null) return item;

            var newItem = new Item
            {
                Title = request.Title,
                Content = request.Content,
                CreationDate = item.CreationDate,
                KnowledgeRating = request.KnowledgeRating,
                Type = item.Type
            };
            _context.Items.Attach(newItem);
            
            _context.SaveChanges();

            return newItem;
        }
    }
}
