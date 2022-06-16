namespace EmployeeManagement.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string NameOffice { get; set; } = string.Empty;
        public List<Employee>? Employees { get; set; }
    }
}
