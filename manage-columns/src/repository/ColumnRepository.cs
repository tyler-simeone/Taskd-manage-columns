using manage_columns.src.clients;
using manage_columns.src.dataservice;
using manage_columns.src.models;

namespace manage_columns.src.repository
{
    public class ColumnRepository : IColumnRepository
    {
        IColumnDataservice _columnDataservice;
        ITasksClient _tasksClient;

        public ColumnRepository(IColumnDataservice columnDataservice, ITasksClient tasksClient)
        {
            _columnDataservice = columnDataservice;
            _tasksClient = tasksClient;
        }

        public async Task<ColumnList> GetColumnsWithTasks(int boardId, int userId)
        {
            try
            {
                ColumnList columnList = await _columnDataservice.GetColumns(boardId, userId);
                columnList.Columns.ForEach(async col => 
                {
                    var taskList = await _tasksClient.GetTasks(boardId, userId);
                    taskList.Tasks.ForEach(task => col.Tasks.Add(task));
                });
                return columnList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public void CreateColumn(CreateColumn createColumnRequest)
        {
            try
            {
                _columnDataservice.CreateColumn(createColumnRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public void UpdateColumn(UpdateColumn updateColumnRequest)
        {
            try
            {
                _columnDataservice.UpdateColumn(updateColumnRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public void DeleteColumn(int columnId, int userId)
        {
            try
            {
                _columnDataservice.DeleteColumn(columnId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}