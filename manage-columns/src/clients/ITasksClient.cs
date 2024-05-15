using manage_columns.src.models;

namespace manage_columns.src.clients
{
    public interface ITasksClient
    { 
        public Task<TaskList> GetTasks(int boardId, int userId);
    }
}