using APIDemo.Models;

namespace APIDemo.Respositories
{
    public class TodoRepository
    {
        public static List<Todo> Todos = new()
        {
            new() { Id = 1, Name = "Monday", Description = "just a desc",DueDate=new DateTime(2023, 07, 31),Status="Draft"},
            new() { Id = 2, Name = "Sunday", Description = "just a desc2",DueDate=new DateTime(2023, 05, 31),Status="Pending"},
        };
    }
}
