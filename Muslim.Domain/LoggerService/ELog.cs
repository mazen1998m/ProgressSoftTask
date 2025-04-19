namespace Muslim.Domain.LoggerService;
using System.Diagnostics;
using System.Text;


public static class ELog
{
    private static readonly object _lock = new();
    private static readonly string _ErrorLogFilePath;
    private static readonly string _warningLogFilePath;

    static ELog()
    {
        _ErrorLogFilePath = Path.Combine(Environment.CurrentDirectory, "log.txt");
        _warningLogFilePath = Path.Combine(Environment.CurrentDirectory, "warning.txt");
        EnsureErrorLogFileExists();
    }

    private static void EnsureErrorLogFileExists()
    {
        lock (_lock)
        {
            if (!File.Exists(_ErrorLogFilePath))
            {
                File.Create(_ErrorLogFilePath).Close();
            }
        }
    }

    private static void EnsureWarningLogFileExists()
    {
        lock (_lock)
        {
            if (!File.Exists(_warningLogFilePath))
            {
                File.Create(_warningLogFilePath).Close();
            }
        }
    }


    public static void Log(Exception ex)
    {
        lock (_lock)
        {
            File.AppendAllText(_ErrorLogFilePath, GetExceptionMessage(ex));
        }
    }


    public static void Log(string message)
    {
        var logMessage = new StringBuilder();
        logMessage.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
        logMessage.AppendLine($"Message: {message}");
        logMessage.AppendLine(new string('-', 50));
        lock (_lock)
        {
            EnsureErrorLogFileExists();
            File.AppendAllText(_ErrorLogFilePath, logMessage.ToString());
        }
    }

    public static void LogWarning(string message)
    {
        var logMessage = new StringBuilder();
        logMessage.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
        logMessage.AppendLine($"Warning: {message}");
        logMessage.AppendLine(new string('-', 50));
        lock (_lock)
        {
            EnsureWarningLogFileExists();
            File.AppendAllText(_warningLogFilePath, logMessage.ToString());
        }
    }


    public static void LogWarning(Exception ex)
    {
        var logMessage = new StringBuilder();
        logMessage.AppendLine(new string('-', 50));
        logMessage.AppendLine("\nWarning Exception:");
        logMessage.AppendLine(new string('-', 50));
        logMessage.AppendLine(GetExceptionMessage(ex));
        lock (_lock)
        {
            EnsureWarningLogFileExists();
            File.AppendAllText(_warningLogFilePath, logMessage.ToString());
        }
    }

    private static string GetExceptionMessage(Exception ex)
    {
        var logMessage = new StringBuilder();

        // Basic error info
        logMessage.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
        logMessage.AppendLine($"Exception Type: {ex.GetType().Name}");
        logMessage.AppendLine($"Message: {ex.Message}");

        // Full stack trace analysis
        logMessage.AppendLine("\nCall Stack Trace:");
        var stackTrace = new StackTrace(ex, true);
        var frames = stackTrace.GetFrames();

        if (frames != null)
        {
            // Reverse to show call hierarchy from origin to crash point
            foreach (var frame in frames.Reverse())
            {
                logMessage.AppendLine(FormatFrame(frame));
            }
        }

        // Inner exception tracking
        var innerEx = ex.InnerException;
        var depth = 1;
        while (innerEx != null)
        {
            logMessage.AppendLine($"\nInner Exception #{depth}: {innerEx.GetType().Name}");
            logMessage.AppendLine($"Message: {innerEx.Message}");
            innerEx = innerEx.InnerException;
            depth++;
        }

        logMessage.AppendLine(new string('-', 50));

        return logMessage.ToString();
    }

    private static string FormatFrame(StackFrame frame)
    {
        var sb = new StringBuilder();
        var method = frame.GetMethod();

        sb.Append("→ ");
        sb.Append(method?.DeclaringType?.FullName ?? "UnknownType");
        sb.Append('.');
        sb.Append(method?.Name ?? "UnknownMethod");

        if (frame.GetFileName() != null)
        {
            sb.Append($" in {Path.GetFileName(frame.GetFileName())}");
            sb.Append(frame.GetFileLineNumber() > 0 ? $":line {frame.GetFileLineNumber()}" : "");
        }

        return sb.ToString();
    }
}