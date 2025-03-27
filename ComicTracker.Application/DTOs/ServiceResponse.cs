﻿namespace ComicTracker.Application.DTOs;

public class ServiceResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
