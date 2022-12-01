using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : 可将指定级别的日志 写入文件

public class Logger : BaseManager<Logger>
{
    public enum LogLevel
    {
        Error,
        Warn,
        Info,
        Debug,
    }

    public enum LogType
    {
        Error,
        Warn,
        Info,
        Debug,
    }

    private delegate void LogDelegate(object message);
    private delegate void LogFormatDelegate(string format, params object[] args);

    private static LogDelegate[] LogFuncs;
    private static LogFormatDelegate[] LogFormatFuncs;

    private static void InitLogFunctions()
    {
        LogFuncs = new LogDelegate[] {
            Debug.LogError,
            Debug.LogWarning,
            Debug.Log,
            Debug.Log,
        };
        LogFormatFuncs = new LogFormatDelegate[] {
            Debug.LogErrorFormat,
            Debug.LogWarningFormat,
            Debug.LogFormat,
            Debug.LogFormat,
        };
    }

    private LogDelegate GetLogFunc(LogType logType)
    {
        return LogFuncs[(int)logType];
    }
    private LogFormatDelegate GetLogFormatFunc(LogType logType)
    {
        return LogFormatFuncs[(int)logType];
    }


    public static void InitLogger()
    {
        Inst.InitManager();
    }


    /*public static void Log(LogType logType, object obj)
    {
        // ...
    }*/


    public static void LogError(object obj, params object[] args)
    {
        Debug.LogError(ObjectsConcat(obj, args));
    }
    public static void LogErrorFormat(string format, params object[] args)
    {
        Debug.LogErrorFormat(format, args);
    }

    public static void LogWarn(object obj, params object[] args)
    {
        Debug.LogWarning(ObjectsConcat(obj, args));
    }
    public static void LogWarnFormat(string format, params object[] args)
    {
        Debug.LogWarningFormat(format, args);
    }

    public static void LogInfo(object obj, params object[] args)
    {
        Debug.Log(ObjectsConcat(obj, args));
    }
    public static void LogInfoFormat(string format, params object[] args)
    {
        Debug.LogFormat(format, args);
    }

    //public static void LogDebug(string message)
    //{
    //    Debug.Log(message);
    //}
    //public static void LogDebug(object obj)
    //{
    //    Debug.Log(obj);
    //}
    public static void LogDebug(object obj, params object[] args)
    {
        //if (args.Length == 0)
        //    Debug.Log(obj);
        //else
        //{
        //    string text = obj?.ToString();
        //    foreach (var each in args)
        //    {
        //        text += '\t' + each?.ToString();
        //    }
        //    Debug.Log(text);
        //}
        Debug.Log(ObjectsConcat(obj, args));
    }
    public static void LogDebugFormat(string format, params object[] args)
    {
        Debug.LogFormat(format, args);
    }

    public static string ObjectsConcat(object obj, params object[] args)
    {
        if (args.Length == 0)
            return obj?.ToString();
        else
        {
            string text = obj?.ToString();
            foreach (var each in args)
                text += '\t' + each?.ToString();
            return text;
        }
    }
}
