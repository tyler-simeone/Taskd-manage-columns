public interface IRequestValidator
{
    bool ValidateGetColumn(int columnId, int userId);

    bool ValidateGetColumns(int userId, int boardId);
    
    bool GetColumnsWithColumns(int boardId, int userId);

    bool ValidateCreateColumn(CreateColumn createColumnRequest);

    bool ValidateUpdateColumn(UpdateColumn updateColumnRequest);

    bool ValidateDeleteColumn(int columnId, int userId);
}

public class RequestValidator : IRequestValidator
{
    public RequestValidator()
    {
        
    }

    public bool ValidateGetColumn(int columnId, int userId)
    {
        return true;
    }

    public bool ValidateGetColumns(int userId, int boardId)
    {
        return true;
    }

    public bool GetColumnsWithColumns(int boardId, int userId)
    {
        return true;
    }

    public bool ValidateCreateColumn(CreateColumn createColumnRequest)
    {
        return true;
    }
    
    public bool ValidateUpdateColumn(UpdateColumn updateColumnRequest)
    {
        return true;
    }
    
    public bool ValidateDeleteColumn(int columnId, int userId)
    {
        return true;
    }
}