using Kryptonite.Domain.Entities;

namespace Kryptonite.Application.Interfaces {
    public interface ITerritoryService {
        public Task<IList<Division>> GetDivisions(bool? isActive = null);
    }
}
