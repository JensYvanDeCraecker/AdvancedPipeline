using System;

namespace AdvancedPipeline
{
    /// <summary>
    ///     Represents a filter.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        ///     Gets the input type that the current filter supports.
        /// </summary>
        Type InputType { get; }

        /// <summary>
        ///     Gets the output type that the current filter supports.
        /// </summary>
        Type OutputType { get; }

        /// <summary>
        ///     Executes the current filter with the specified input.
        /// </summary>
        /// <param name="input">The input to execute in the current filter.</param>
        /// <returns>The output of the execution.</returns>
        Object Execute(Object input);
    }

    /// <summary>
    ///     Represents a generic filter.
    /// </summary>
    /// <typeparam name="TInput">The input type of the filter.</typeparam>
    /// <typeparam name="TOutput">The output type of the filter.</typeparam>
    public interface IFilter<in TInput, out TOutput> : IFilter
    {
        /// <summary>
        ///     Executes the current filter with the specified input.
        /// </summary>
        /// <param name="input">The input to execute in the current filter.</param>
        /// <returns>The output of the execution.</returns>
        TOutput Execute(TInput input);
    }
}