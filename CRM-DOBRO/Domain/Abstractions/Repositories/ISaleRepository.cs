using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<List<Sale>> GetSalesBySalerIdAsync(int salerId);
    Task<Lead?> FoundLeadAsync(int leadId);
}
