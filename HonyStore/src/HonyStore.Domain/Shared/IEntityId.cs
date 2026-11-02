using CSharpFunctionalExtensions;
using HonyStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace HonyStore.Domain.Shared
{
    public interface IEntityId<TSelf> where TSelf : IEntityId<TSelf>
    {
        public Guid Value { get; }

        public static abstract TSelf NewId();

        public static abstract TSelf Empty();
    }
}
