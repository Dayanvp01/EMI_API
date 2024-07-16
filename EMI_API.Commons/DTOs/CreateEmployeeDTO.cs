namespace EMI_API.Commons.DTOs
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; } = null!;
        public decimal Salary { get; set; }
        /// <summary>
        /// 1 For Regular, 2 For Manager
        /// </summary>
        public int CurrentPosition { get; set; }
        public int DepartmentId { get; set; }
    }
}
