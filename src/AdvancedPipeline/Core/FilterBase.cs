using System;

namespace AdvancedPipeline.Core
{
    /// <summary>
    ///     Represents an abstract filter that provides basic validation.
    /// </summary>
    /// <typeparam name="TInput">The input type of the filter.</typeparam>
    /// <typeparam name="TOutput">The output type of the filter.</typeparam>
    public abstract class FilterBase<TInput, TOutput> : IFilter<TInput, TOutput>
    {
        /// <summary>
        ///     Executes the current filter with the specified input.
        /// </summary>
        /// <param name="input">The input to execute in the current filter.</param>
        /// <returns>The output of the execution.</returns>
        public abstract TOutput Execute(TInput input);

        /// <summary>
        ///     Gets the input type that the current filter supports.
        /// </summary>
        public Type InputType { get; } = typeof(TInput);

        /// <summary>
        ///     Gets the output type that the current filter supports.
        /// </summary>
        public Type OutputType { get; } = typeof(TOutput);

        Object IFilter.Execute(Object input)
        {
            if (input != null && !(input is TInput)) // Checks if the input is not null and if it is an instance of the desired type.
                throw new ArgumentException($"The specified input is not an instance of {InputType}.", nameof(input));
            if (input == null && InputType.IsValueType) // A value type can't be null.
                throw new ArgumentNullException(nameof(input), $"The specified input can't be null since the input type ({InputType}) is a value type.");
            return Execute((TInput)input);
        }
    }
}