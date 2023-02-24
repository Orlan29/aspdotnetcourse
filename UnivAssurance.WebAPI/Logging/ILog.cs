namespace UnivAssurance.WebAPI.Logging;

public interface ILog
{
    void Information(string msg);
    void Warning(string msg);
    void Error(string msg);
    void Debug(string msg);
}