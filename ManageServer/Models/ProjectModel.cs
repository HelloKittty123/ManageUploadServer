namespace ManageServer.Models
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DataProject { get; set; }
        public string? DataProjectType { get; set; }
        public Guid TagId { get; set; }
    }
}
