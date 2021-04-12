using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.BasketModule.Domain.Exceptions
{
    public class InvalidStatusTransitionException : ValidationException
    {
        public InvalidStatusTransitionException(BasketStatuses currentState, BasketStatuses targetState)
            : base($"It is not possible to switch from {currentState} status to {targetState} status")
        {
        }
    }
}