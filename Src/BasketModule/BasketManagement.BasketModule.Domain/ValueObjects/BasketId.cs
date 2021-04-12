using System;
using System.Collections.Generic;
using BasketManagement.Shared.Domain;

namespace BasketManagement.BasketModule.Domain.ValueObjects
{
    public class BasketId : ValueObject
    {
        public Guid Value { get; private set; }

        public BasketId(Guid value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}