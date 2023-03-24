namespace APIDemo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }//title
        public string Description { get; set; }
        public DateTime DueDate { get; set;}
        public string Status { get; set; }
    }
}
