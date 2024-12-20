using System;
using Microsoft.IdentityModel.Tokens;
using manage_columns.src.models;
using MySql.Data.MySqlClient;
using System.Data;

namespace manage_columns.src.dataservice
{
    public class ColumnsDataservice : IColumnsDataservice
    { 
        private IConfiguration _configuration;
        private string _connectionString;

        public ColumnsDataservice(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["LocalDBConnection"];
            
            if (_connectionString.IsNullOrEmpty())
                _connectionString = _configuration.GetConnectionString("LocalDBConnection");
        }
        
        public async Task<Column> GetColumn(int columnId, int userId)
        {
            using (MySqlConnection connection = new(_connectionString))
            {
                using (MySqlCommand command = new("ProjectB.ColumnGetByColumnIdAndUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paramColumnId", columnId);
                    command.Parameters.AddWithValue("@paramUserId", userId);

                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Column column = ExtractColumnFromReader(reader);
                                return column;
                            }

                            return new Column();
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

        public async Task<ColumnList> GetColumns(int boardId, int userId)
        {
            using (MySqlConnection connection = new(_connectionString))
            {
                using (MySqlCommand command = new("ProjectB.ColumnGetAllByBoardAndUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
            using (MySqlConnection connection = new(_connectionString))
            {
                using (MySqlCommand command = new("ProjectB.ColumnPersist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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

            using (MySqlConnection connection = new(_connectionString))
            {
                using (MySqlCommand command = new("ProjectB.ColumnUpdate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
            using (MySqlConnection connection = new(_connectionString))
            {
                using (MySqlCommand command = new("ProjectB.ColumnDelete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
        
        // TO DO: See if we can abstract at this level
        private void ExecuteStoredProcedure(string procName, List<MySqlSprocParameter> sprocParams)
        {
            using (MySqlConnection connection = new(_connectionString))
            {
                using (MySqlCommand command = new(procName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlParameter parameter = new();
                    
                    sprocParams.ForEach(p => 
                    {
                        parameter.ParameterName = $"@{p.ParameterName}";
                        parameter.MySqlDbType = p.DbType;
                        parameter.Value = p.InputValue;
                    });
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        
        private Column ExtractColumnFromReader(MySqlDataReader reader)
        {
            int columnId = reader.GetInt32("ColumnId");
            int boardId = reader.GetInt32("BoardId");
            int userId = reader.GetInt32("UserId");
            string name = reader.GetString("ColumnName");
            int taskCount = reader.GetInt32("TaskCount");
            string description = reader.GetString("ColumnDescription");
            DateTime createDatetime = reader.GetDateTime("CreateDatetime");
            int createUserId = reader.GetInt32("CreateUserId");
            DateTime updateDatetime = reader.GetDateTime("UpdateDatetime");
            int updateUserId = reader.GetInt32("UpdateUserId");

            return new Column
            {
                ColumnId = columnId,
                BoardId = boardId,
                UserId = userId,
                ColumnName = name,
                TaskCount = taskCount,
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