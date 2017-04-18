using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Business.Import
{
    public enum ImportTaskStatus : int
    {
        NotStarted = 0,
        InProgress = 1,
        Finished = 2,
        Failed = 3
    }

    public static class ImportTaskStatusExtensions
    {
        public static string GetStringRepresentation(this ImportTaskStatus status)
        {
            switch(status)
            {
                case ImportTaskStatus.Failed:
                    return "Failed";
                case ImportTaskStatus.Finished:
                    return "Finished";
                case ImportTaskStatus.InProgress:
                    return "InProgress";
                case ImportTaskStatus.NotStarted:
                    return "NotStarted";
                default:
                    return "Undefined";
            }
        }
    }

}
