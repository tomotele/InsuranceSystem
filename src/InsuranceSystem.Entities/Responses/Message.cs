using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.Responses
{
    public class Message
    {
        public class ErrorCode
        {
            public static string UNSUCCESSFUL = "99";
            public static string NOT_FOUND = "404";
            public static string BAD_REQUEST = "400";
            public static string EXISTS = "409";
            public static string ERROR = "500";
        }
    }
}
