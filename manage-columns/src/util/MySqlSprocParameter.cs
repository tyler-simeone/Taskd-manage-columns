using MySql.Data.MySqlClient;

public struct MySqlSprocParameter
{
    public MySqlSprocParameter(string parameterName, MySqlDbType dbType, object inputValue)
    {
        ParameterName = parameterName;
        DbType = dbType;
        InputValue = inputValue;
    }

    public string ParameterName {get; init;}

    public MySqlDbType DbType {get; init;}
    
    public object InputValue {get; init;}
}