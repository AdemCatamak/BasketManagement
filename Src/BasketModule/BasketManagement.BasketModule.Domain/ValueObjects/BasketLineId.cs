using System;
using System.Collections.Generic;
using BasketManagement.Shared.Domain;

namespace BasketManagement.BasketModule.Domain.ValueObjects
{
    public class BasketLineId : ValueObject
    {
        public Guid Value { get; private set; }

        public BasketLineId(Guid value)
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