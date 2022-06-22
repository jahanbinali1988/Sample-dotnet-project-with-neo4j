using System;

namespace Sample.SharedKernel.Application
{
    public class CommandBase : ICommand
    {
        public Guid CommandId { get; }

        public CommandBase()
        {
            this.CommandId = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.CommandId = id;
        }
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        public Guid CommandId { get; }

        protected CommandBase()
        {
            this.CommandId = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.CommandId = id;
        }
    }
}
