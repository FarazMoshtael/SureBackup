

namespace SureBackup.Domain.Pattern;

public class Result<T> where T : class
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public Result(string error)
    {
        Success = false;
        Message = error;
    }
    public Result(T model)
    {
        Success = true;
        Data = model;
    }

    public static Result<T> Successful(T model) => new Result<T>(model);
    public static Result<T> Failure(string error) => new Result<T>(error);

}

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Result(bool success,string? message)
    {
        Success = success;
        Message = message;
    }
   
    public static Result Successful(string? message=null) => new Result(true,message);
    public static Result Failure(string error) => new Result(false,error);

}