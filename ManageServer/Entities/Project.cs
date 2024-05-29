namespace ManageServer.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DataProject { get; set; }
        public Guid TagId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Tag Tag { get; set; } = null!;
    }
}
