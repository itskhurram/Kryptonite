using Kryptonite.Application.Interfaces;
using Kryptonite.Domain.Entities;
using Kryptonite.Domain.Interfaces;

namespace Kryptonite.Application.Services {
    public class TerritoryService : ITerritoryService {
        private readonly ITerritoryRepository _territoryRepository;
        public TerritoryService(ITerritoryRepository territoryRepository) {
            _territoryRepository = territoryRepository;
        }
        public async Task<IList<Division>> GetDivisions(bool? isActive = null) {
            try {
                return await _territoryRepository.GetDivisions(isActive);
            }
            catch {
                throw;
            }
        }
    }
}
