using manage_columns.src.models;

namespace manage_columns.src.dataservice
{
    public interface IColumnsDataservice
    {

        // Task<Column> GetColumn(int columnId, int userId);

        // Task<ColumnList> GetColumns(int userId);

        Task<ColumnList> GetColumns(int boardId, int userId);

        void CreateColumn(CreateColumn createColumnRequest);

        void UpdateColumn(UpdateColumn updateColumnRequest);

        void DeleteColumn(int columnId, int userId);
    }
}