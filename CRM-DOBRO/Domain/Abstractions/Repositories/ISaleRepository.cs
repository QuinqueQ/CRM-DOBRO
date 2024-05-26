using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<List<Sale>> GetSalesBySalerIdAsync(int salerId);
        Task<Lead?> FoundLeadAsync(int leadId);
        Task<List<Sale>> GetSalesAsync();
    }
}
