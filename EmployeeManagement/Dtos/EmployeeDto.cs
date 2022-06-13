namespace EmployeeManagement.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public float Salary { get; set; }
        public int OfficeId { get; set; }
        public Office? Office { get; set; }
    }
}
