namespace AuditPocConsumer.AspCore.sample.Model
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string Code { get; set; }
        public string BranchName { get; set; }
        public Address BranchAddress { get; set; }
        public bool Status { get; set; }
    }
}
