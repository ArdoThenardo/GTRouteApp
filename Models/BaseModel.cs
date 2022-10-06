namespace GTRouteApp.Data;

public class BaseModel<T>
{
    public int NumberOfData { get; set; }
    public T? Data { get; set; }
}