namespace TechAssess.SupplierService.Dto
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
