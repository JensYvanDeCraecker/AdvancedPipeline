using System;

namespace AdvancedPipeline
{
    public abstract class Filter<TInput, TOutput> : IFilter<TInput, TOutput>
    {
        public abstract TOutput Execute(TInput input);

        public Type InputType { get; } = typeof(TInput);

        public Type OutputType { get; } = typeof(TOutput);

        public Object Execute(Object input)
        {
            if (input != null && !(input is TInput)) // Checks if the input is not null and if it is an instance of the desired type.
                throw new ArgumentException($"The specified input is not an instance of {InputType}.", nameof(input));
            if (input == null && InputType.IsValueType) // A value type can't be null.
                throw new ArgumentNullException(nameof(input), $"The specified input can't be null since the input type ({InputType}) is a value type.");
            return Execute((TInput)input);
        }
    }
}