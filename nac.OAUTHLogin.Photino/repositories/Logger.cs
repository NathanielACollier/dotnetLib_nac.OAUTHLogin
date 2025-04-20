using System;
using System.Runtime.CompilerServices;

namespace nac.OAUTHLogin.Photino.repositories;

public class Logger
{
    /*
     Used for internal library logging
     */
    public class LogMessage
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public string CallingMemberName { get; set; }
        public Type CallerType { get; set; }
    }
        
    private Type callerType;

    public static event EventHandler<LogMessage> OnNewMessage;

    public Logger()
    {
        var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        var _class_to_log = mth.ReflectedType;
        this.callerType = _class_to_log;
    }
    
    public void Debug(string message, [CallerMemberName]string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "DEBUG",
            Message = message,
            CallingMemberName = callerMemberName,
            CallerType = callerType
        });
    }

    public void Info(string message, [CallerMemberName]string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "INFO",
            Message = message,
            CallingMemberName = callerMemberName,
            CallerType = callerType
        });
    }


    public void Error(string message, [CallerMemberName]string callerMemberName = "")
    {
        OnNewMessage?.Invoke(this, new LogMessage
        {
            Level = "ERROR",
            Message = message,
            CallingMemberName = callerMemberName,
            CallerType = callerType
        });
    }
}
