namespace Sample.Api.Extensions
{
    public enum ErrorCode
    {
        None = 0,
        NotFound = 4040,
        BadRequest = 4001,
        RequestSchemaValidation = 4002,
        BusinessRuleValidation = 4003,
        InternalServerError = 5000
    }
}
