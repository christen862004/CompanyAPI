namespace CompanyAPI.DTOS
{
    public class DeptWithEmpNamesDTO
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }

        public List<string> EmpNames { get; set; } = new ();
    }
}
