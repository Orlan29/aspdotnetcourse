using NLog;

namespace UnivAssurance.WebAPI.Logging;

public class Log: ILog
{
    private static NLog.ILogger Logger = LogManager.GetCurrentClassLogger();
    public void Information(string msg)
    {
        Logger.Info(msg);
    }

    public void Debug(string msg)
    {
        Logger.Debug(msg);
    }

    public void Warning(string msg)
    {
        Logger.Warn(msg);
    }

    public void Error(string msg)
    {
        Logger.Error(msg);
    }
}