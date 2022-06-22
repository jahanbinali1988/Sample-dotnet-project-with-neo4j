using Sample.SharedKernel.Application;

namespace Sample.Application.Contract.Currency.Command
{
    public class CreateCurrencyCommand : CommandBase<CurrencyResponseDto>
    {
        public CreateCurrencyCommand(string name)
        {
            Name = name;
        }
        public string Name { get; init; }
    }
}
