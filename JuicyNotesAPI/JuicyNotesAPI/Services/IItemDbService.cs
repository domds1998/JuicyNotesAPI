using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface IItemDbService
    {
        public Item deleteItem(int id);
        public Task<Item> updateItem(UpdateItemRequest request);

        public Task<Item> addItem(AddingItemRequest request);

    }
}
