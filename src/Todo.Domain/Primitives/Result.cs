﻿using System.Diagnostics.CodeAnalysis;

namespace Todo.Domain.Primitives;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error.", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue Value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = Value;
    }

    [NotNull]
    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("There is no value for failure.");

}

