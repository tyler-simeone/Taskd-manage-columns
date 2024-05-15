using System;
using manage_columns.src.models;
using MySql.Data.MySqlClient;

namespace manage_columns.src.dataservice
{
    public class ColumnsDataservice : IColumnsDataservice
    { 
        private IConfiguration _configuration;

        public ColumnsDataservice(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<ColumnList> GetColumns(int boardId, int userId)
        {
            var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"CALL ProjectB.ColumnGetAllByBoardAndUserId(@paramBoardId, @paramUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paramBoardId", boardId);
                    command.Parameters.AddWithValue("@paramUserId", userId);

                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            var columnList = new ColumnList();

                            while (reader.Read())
                            {
                                Column column = ExtractColumnFromReader(reader);
                                columnList.Columns.Add(column);
                            }

                            return columnList;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async void CreateColumn(CreateColumn createColumnRequest)
        {
            var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"CALL ProjectB.ColumnPersist(@paramBoardId, @paramColumnName, @paramColumnDescription, @paramCreateUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paramBoardId", createColumnRequest.BoardId);
                    command.Parameters.AddWithValue("@paramColumnName", createColumnRequest.ColumnName);
                    command.Parameters.AddWithValue("@paramColumnDescription", createColumnRequest.ColumnDescription);
                    command.Parameters.AddWithValue("@paramCreateUserId", createColumnRequest.UserId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async void UpdateColumn(UpdateColumn updateColumnRequest)
        {

            var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"CALL ProjectB.ColumnUpdate(@paramColumnId, @paramColumnName, @paramColumnDescription, @paramUpdateUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paramColumnId", updateColumnRequest.ColumnId);
                    command.Parameters.AddWithValue("@paramColumnName", updateColumnRequest.ColumnName);
                    command.Parameters.AddWithValue("@paramColumnDescription", updateColumnRequest.ColumnDescription);
                    command.Parameters.AddWithValue("@paramUpdateUserId", updateColumnRequest.UserId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async void DeleteColumn(int columnId, int userId)
        {
            var connectionString = _configuration.GetConnectionString("ProjectBLocalConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"CALL ProjectB.ColumnDelete(@paramColumnId, @paramUpdateUserId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paramColumnId", columnId);
                    command.Parameters.AddWithValue("@paramUpdateUserId", userId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        #region HELPERS
        
        private Column ExtractColumnFromReader(MySqlDataReader reader)
        {
            int columnId = reader.GetInt32("ColumnId");
            int boardId = reader.GetInt32("BoardId");
            int userId = reader.GetInt32("UserId");
            string name = reader.GetString("ColumnName");
            string description = reader.GetString("ColumnDescription");
            DateTime createDatetime = reader.GetDateTime("CreateDatetime");
            int createUserId = reader.GetInt32("CreateUserId");
            DateTime updateDatetime = reader.GetDateTime("UpdateDatetime");
            int updateUserId = reader.GetInt32("UpdateUserId");

            return new Column()
            {
                ColumnId = columnId,
                BoardId = boardId,
                UserId = userId,
                ColumnName = name,
                ColumnDescription = description,
                CreateDatetime = createDatetime,
                CreateUserId = createUserId,
                UpdateDatetime = updateDatetime,
                UpdateUserId = updateUserId
            };
        }

        #endregion
    }
}