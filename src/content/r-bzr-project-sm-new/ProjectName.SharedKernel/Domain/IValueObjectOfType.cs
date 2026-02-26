using Ardalis.Result;

namespace ProjectName.SharedKernel.Domain;

public interface IValueObject<TSelf, TValue> : IValueObject
    where TSelf : struct, IValueObject<TSelf, TValue>
    where TValue : notnull
{
    TValue Value { get; }

    static abstract Result<TSelf> From(TValue? value);

    static abstract implicit operator TSelf(TValue value);

    static abstract implicit operator TValue(TSelf value);
}

//public readonly record struct MyIban : IValueObject<MyIban, string>
//{
//    public string Value { get; }

//    private MyIban(string value)
//    {
//        ArgumentNullException.ThrowIfNull(value);

//        Value = value;
//    }

//    public static Result<MyIban> From(string? value)
//    {
//        //Check if the value is a valid IBAN (this is just a simple check, you can implement a more robust validation)

//        return Result.Success(new MyIban(value!));
//    }

//    public static implicit operator MyIban(string value)
//        => From(value).Value;

//    public static implicit operator string(MyIban value)
//        => value.Value;
//}

//public readonly record struct MyAddressLine : IValueObject<MyAddressLine, string>
//{
//    public string Value { get; }

//    private MyAddressLine(string value)
//    {
//        ArgumentNullException.ThrowIfNull(value);

//        Value = value;
//    }

//    public static Result<MyAddressLine> From(string? value)
//    {
//        return Result.Success(new MyAddressLine(value!));
//    }

//    public static implicit operator MyAddressLine(string value)
//        => From(value).Value;

//    public static implicit operator string(MyAddressLine value)
//        => value.Value;
//}
