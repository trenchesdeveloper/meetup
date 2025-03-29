using System;

namespace API.Errors;

public class ApiExeption(int statusCode, string message, string? details)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message ?? "An error occurred";
    public string? Details { get; set; } = details;
}

