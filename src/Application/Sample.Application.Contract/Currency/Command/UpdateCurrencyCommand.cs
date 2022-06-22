using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Currency.Command
{
    public class UpdateCurrencyCommand : CommandBase
    {
        public UpdateCurrencyCommand(Guid id, string name)
        {
            this.Id = id;
            Name = name;
        }
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
