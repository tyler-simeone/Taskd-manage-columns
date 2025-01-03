using manage_columns.src.clients;
using manage_columns.src.dataservice;
using manage_columns.src.models;

namespace manage_columns.src.repository
{
    public class ColumnsRepository : IColumnsRepository
    {
        IColumnsDataservice _columnsDataservice;
        ITasksClient _tasksClient;

        public ColumnsRepository(IColumnsDataservice columnsDataservice, ITasksClient tasksClient)
        {
            _columnsDataservice = columnsDataservice;
            _tasksClient = tasksClient;
        }

        public async Task<Column> GetColumn(int columnId, int userId)
        {
            try
            {
                return await _columnsDataservice.GetColumn(columnId, userId); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<ColumnList> GetColumnsWithTasks(int boardId, int userId)
        {
            try
            {
                ColumnList columnList = await _columnsDataservice.GetColumns(boardId, userId);
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
                _columnsDataservice.CreateColumn(createColumnRequest);
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
                _columnsDataservice.UpdateColumn(updateColumnRequest);
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
                _columnsDataservice.DeleteColumn(columnId, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}