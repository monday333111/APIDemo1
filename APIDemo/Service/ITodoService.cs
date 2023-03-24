using APIDemo.Models;
using APIDemo.Models;
using APIDemo.Respositories;

namespace APIDemo.Service
{
    public class ITodoService
    {
        public Todo Create(Todo todo)
        {
            todo.Id = TodoRepository.Todos.Count +1;
            TodoRepository.Todos.Add(todo);
            return todo;
        }
        public Todo Get(int id)
        {
            var todo = TodoRepository.Todos.FirstOrDefault(o => o.Id == id);
            if (todo is null) return null;
            return todo;
        }
        public List<Todo> List()
        { 
            return TodoRepository.Todos;
        }
        public Todo Update(Todo newTodo)
        {
            var oldTodo = TodoRepository.Todos.FirstOrDefault(o => o.Id == newTodo.Id);
            if (oldTodo == null) return null;
            oldTodo.Id = newTodo.Id;
            oldTodo.Description = newTodo.Description;
            oldTodo.Status = newTodo.Status;
            return oldTodo;
        }
        public bool Delete(int id)
        {
            var oldTodo = TodoRepository.Todos.FirstOrDefault(o => o.Id == id);
            if (oldTodo is null) return false;
            TodoRepository.Todos.Remove(oldTodo);
            return true;
        }
    }
}
