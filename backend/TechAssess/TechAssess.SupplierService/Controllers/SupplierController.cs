using Microsoft.AspNetCore.Mvc;
using TechAssess.SupplierService.Dto;
using TechAssess.SupplierService.Enums;
using TechAssess.SupplierService.Models;
using TechAssess.SupplierService.Services;
using TechAssess.SupplierService.Util;

namespace TechAssess.SupplierService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<Supplier>>> GetSuppliers([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string? legalName = null, [FromQuery] string? tradeName = null, [FromQuery] string? taxIdentificationNumber = null, [FromQuery] int? countryId = 0)
        {
            var suppliers = await _supplierService.GetAsync(pageIndex, pageSize, legalName, tradeName, taxIdentificationNumber, countryId);
            return Ok(suppliers);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Supplier>>> GetSupplierById(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound(new ApiResponse<Supplier>
                {
                    ErrorCode = ErrorCode.NotFound.ToString(),
                    ErrorMessage = "Supplier not found"
                });
            }

            return Ok(new ApiResponse<Supplier>
            {
                Data = supplier
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Supplier>>> CreateSupplier(Supplier supplier)
        {
            try
            {
                var addedSupplier = await _supplierService.CreateAsync(supplier);
                return CreatedAtAction(nameof(GetSupplierById), new { id = addedSupplier.Id }, new ApiResponse<Supplier>
                {
                    Data = addedSupplier
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Supplier>
                {
                    ErrorCode = ErrorCode.ResourceNotCreated.ToString(),
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Supplier>>> UpdateSupplier(Supplier supplier, int id)
        {
            if (id != supplier.Id)
            {
                return BadRequest(new ApiResponse<Supplier>
                {
                    ErrorCode = ErrorCode.InvalidId.ToString(),
                    ErrorMessage = "ID provided in route does not match ID in request body."
                });
            }

            try
            {
                var updatedSupplier = await _supplierService.UpdateAsync(supplier, id);
                return Ok(new ApiResponse<Supplier>
                {
                    Data = updatedSupplier
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Supplier>
                {
                    ErrorCode = ErrorCode.UpdateFailed.ToString(),
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSupplier(int id)
        {
            try
            {
                await _supplierService.DeleteAsync(id);
                return Ok(new ApiResponse<string>
                {
                    Data = "Supplier deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    ErrorCode = ErrorCode.DeleteFailed.ToString(),
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
