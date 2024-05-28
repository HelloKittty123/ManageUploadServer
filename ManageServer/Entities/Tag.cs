namespace ManageServer.Entities
{
    public class Tag
    {
        public Tag()
        {
            Projects = new HashSet<Project>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        
        public virtual ICollection<Project> Projects { get; set; }
    }
}
