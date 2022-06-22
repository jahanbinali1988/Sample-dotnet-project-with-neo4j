namespace Sample.Api.Extensions
{
    public static class ErrorCodeExtension
    {
        public static string GetDisplayMessage(this ErrorCode code)
        {
            switch (code)
            {
                default: return code.ToString("G");
            }
        }
    }
}
