namespace manage_columns.src.models
{
    public class Column : ResponseBase
    {
        public Column()
        {

        }

        public int ColumnId { get; set; }

        public int BoardId { get; set; }

        public int UserId { get; set; }

        public string ColumnName { get; set; }
        
        public int TaskCount { get; set; }

        public string ColumnDescription { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDatetime { get; set; }

        public int UpdateUserId { get; set; }

        public DateTime UpdateDatetime { get; set; }
    }
}