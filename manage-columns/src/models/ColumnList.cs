namespace manage_columns.src.models
{
    public class ColumnList : ResponseBase
    {
        public ColumnList()
        {
            Columns = new List<Column>();
        }

        public List<Column> Columns { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}