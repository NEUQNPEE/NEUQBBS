namespace WebApiDemo.Entities.ESection
{
    public class Section
    {
        public int Id { get; set; }
        public required string Area { get; set; }
        public required string Icon { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string TableName { get; set; }
    }
}
