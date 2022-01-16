using System;

namespace SpeakerMeet.Core.Interfaces.Logging
{
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message);
        void LogInformation<T0>(string message, T0 arg0);
        void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogInformation<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
        
        void LogWarning(string message, params object[] args);
        
        void LogWarning(Exception ex, string message, params object[] args);
        
        void LogError(Exception ex, string message, params object[] args);
    }
}
