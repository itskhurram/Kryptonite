using Kryptonite.Domain.Entities;

namespace Kryptonite.Domain.Interfaces {
    public interface ITerritoryRepository {
        public Task<IList<Division>> GetDivisions(bool? isActive=null);
    }

}
