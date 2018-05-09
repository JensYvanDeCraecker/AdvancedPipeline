using System;
using System.Collections.Generic;

namespace AdvancedPipeline
{
    /// <summary>
    ///     Represents a sequence of filters which can be executed in sequence.
    /// </summary>
    public interface IPipeline : IEnumerable<IFilter>
    {
        /// <summary>
        ///     Gets the input type of the current pipeline.
        /// </summary>
        Type InputType { get; }

        /// <summary>
        ///     Gets the output type of the current pipeline.
        /// </summary>
        Type OutputType { get; }

        /// <summary>
        ///     Gets the state of the current pipeline.
        /// </summary>
        PipelineState State { get; }

        /// <summary>
        ///     Gets the output of the last execution of the current pipeline.
        /// </summary>
        /// <exception cref="InvalidOperationException">The last execution resulted in an error.</exception>
        Object Output { get; }

        /// <summary>
        ///     Gets the error of the last execution of the current pipeline.
        /// </summary>
        /// <exception cref="InvalidOperationException">The last execution was successful.</exception>
        Exception Error { get; }

        /// <summary>
        ///     Executes the current pipeline with the specified input.
        /// </summary>
        /// <param name="input">The input to execute in the current pipeline.</param>
        /// <returns><c>true</c> if the execution was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">The current pipeline is busy.</exception>
        Boolean Execute(Object input);

        /// <summary>
        ///     Resets the current pipeline.
        /// </summary>
        /// <exception cref="InvalidOperationException">The current pipeline is busy.</exception>
        void Reset();
    }
}