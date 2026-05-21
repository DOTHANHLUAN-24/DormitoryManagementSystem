using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize(Roles = "Admin,ManagerStaff")]
    public class ContractController(IContractService contractService) : BaseController
    {
        private readonly IContractService _contractService = contractService;

        [HttpGet("ByCode/{contractCode}")]
        public async Task<IActionResult> GetByContractCode(string contractCode)
        {
            Contract? contract = await _contractService.GetByContractCodeAsync(contractCode);
            if (contract == null) return NotFound();
            return Ok(contract);
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            IEnumerable<Contract> contracts = await _contractService.GetByUserIdAsync(userId);
            return Ok(contracts);
        }

        [HttpGet("ByBed/{bedId}")]
        public async Task<IActionResult> GetByBedId(Guid bedId)
        {
            Contract contract = await _contractService.GetByBedIdAsync(bedId);
            return Ok(contract);
        }
    }
}
