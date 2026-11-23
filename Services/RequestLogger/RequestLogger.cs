using System;
using p48_lessons_asp.Models;
namespace p48_lessons_asp.Services.RequestLogger;

public class RequestLogger : IRequestLogger
{
    public List<Request> Requests { get; set; } = new List<Request>();

    public void LogRequest(string RequestType, string pageLink, int StatusCode)
    {
        Console.WriteLine($"{RequestType} Request to {pageLink}\nStatusCode: {StatusCode}");
        Requests.Add(new Request
        {
            Method = RequestType,
            PageLink = pageLink,
            StatusCode = StatusCode
        });
    }
}
