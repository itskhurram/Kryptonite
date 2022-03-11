﻿using Kryptonite.Application.Interfaces;
using Kryptonite.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptonite.API.Controllers {
    public class TerritoryController : BaseController {
        private readonly ITerritoryService _territoryService;
        public TerritoryController(ITerritoryService territoryService) {
            _territoryService = territoryService;
        }
        [HttpGet]
        public async Task<IEnumerable<Division>> GetDivisions(bool? isActive) {
            return await _territoryService.GetDivisions(isActive);
        }
    }
}