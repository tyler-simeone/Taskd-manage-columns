using manage_columns.src.models;

namespace manage_columns.src.repository
{
    public interface IColumnsRepository
    {
        Task<Column> GetColumn(int columnId, int userId);

        // Task<ColumnList> GetColumns(int columnId);

        Task<ColumnList> GetColumnsWithTasks(int boardId, int userId);

        void CreateColumn(CreateColumn createColumnRequest);

        void UpdateColumn(UpdateColumn updateColumnRequest);

        void DeleteColumn(int columnId, int userId);
    }
}