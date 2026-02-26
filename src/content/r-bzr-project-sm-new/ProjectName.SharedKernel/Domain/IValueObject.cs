namespace ProjectName.SharedKernel.Domain;

public interface IValueObject;

//public sealed record MyAddress : IValueObject
//{
//    public MyAddressLine Line1 { get; init; }
//    public MyAddressLine Line2 { get; init; }

//    private MyAddress(MyAddressLine line1, MyAddressLine line2)
//    {
//        Line1 = line1;
//        Line2 = line2;
//    }

//    public static Result<MyAddress> From(string line1, string line2)
//    {
//        List<ValidationError> validationErrors = [];

//        var line1Result = MyAddressLine.From(line1);
//        if (line1Result.IsInvalid())
//            validationErrors.AddRange(line1Result.ValidationErrors);

//        var line2Result = MyAddressLine.From(line2);
//        if (line2Result.IsInvalid())
//            validationErrors.AddRange(line2Result.ValidationErrors);

//        return Result.Success(new MyAddress(line1Result.Value, line2Result.Value));
//    }
//}
