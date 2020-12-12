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
        public bool deleteItem(int id);
        public Item updateItem(UpdateItemRequest request);

        public Item addItem(AddingItemRequest request);

    }
}
