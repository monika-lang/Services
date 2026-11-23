using System;
using p48_lessons_asp.Models;

namespace p48_lessons_asp.Services.RequestLogger;

public interface IRequestLogger
{
    List<Request> Requests {get;set;}

    void LogRequest(string RequestType, string pageLink, int StatusCode);
}
