using System;
using CrouseMath.Infrastructure.Persistence;

namespace CrouseMath.Application.UnitTests
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        protected CommandTestBase()
        {
            Context = ApplicationContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }
}