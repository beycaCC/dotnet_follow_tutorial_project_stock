using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    // NO DTOs, NO HTTP Logics in Repository level
    public interface IStockRepository
    {
        // Interface allows us to plug in this code and abstract our code away
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id); //FirstOrDefault may returns NULL, so we need ? question mark.
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, Stock stockModel);
        Task<Stock?> DeleteAsync(int id); 
        Task<bool> StockExists(int id);
    }
}