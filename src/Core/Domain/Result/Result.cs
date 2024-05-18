﻿namespace Domain.Result;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && error != string.Empty)
        {
            throw new InvalidOperationException();
        }
        if (!isSuccess && error == string.Empty)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result Fail(string error)
    {
        return new Result(false, error);
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(T value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Ok(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    public static new Result<T> Fail(string error)
    {
        return new Result<T>(default(T), false, error);
    }
}