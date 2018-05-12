using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedPipeline
{
    /// <summary>
    ///     Represents a base pipeline that provides the most functionalities out of the box.
    /// </summary>
    public abstract class PipelineBase : IPipeline
    {
        private Tuple<Exception> error;
        private Tuple<Object> output;

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public abstract IEnumerator<IFilter> GetEnumerator();

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Gets the input type of the current pipeline.
        /// </summary>
        public abstract Type InputType { get; }

        /// <summary>
        ///     Gets the output type of the current pipeline.
        /// </summary>
        public abstract Type OutputType { get; }

        /// <summary>
        ///     Gets the state of the current pipeline.
        /// </summary>
        public PipelineState State { get; private set; } = PipelineState.None;

        /// <summary>
        ///     Gets the output of the last execution of the current pipeline.
        /// </summary>
        /// <exception cref="InvalidOperationException">The last execution resulted in an error.</exception>
        public Object Output
        {
            get
            {
                return output != null ? output.Item1 : throw new InvalidOperationException("The last execution resulted in an error.");
            }
        }

        /// <summary>
        ///     Gets the error of the last execution of the current pipeline.
        /// </summary>
        /// <exception cref="InvalidOperationException">The last execution was successful.</exception>
        public Exception Error
        {
            get
            {
                return error != null ? error.Item1 : throw new InvalidOperationException("The last execution was successful.");
            }
        }

        /// <summary>
        ///     Executes the current pipeline with the specified input.
        /// </summary>
        /// <param name="input">The input to execute in the current pipeline.</param>
        /// <returns><c>true</c> if the execution was successful; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">The current pipeline is busy.</exception>
        public Boolean Execute(Object input)
        {
            if (State == PipelineState.Busy)
                throw new InvalidOperationException("The current pipeline is busy.");
            Reset();
            State = PipelineState.Busy;
            try
            {
                output = Tuple.Create(this.Aggregate(input, (item, filter) => filter.Execute(item)));
                State = PipelineState.Success;
                return true;
            }
            catch (Exception e)
            {
                error = Tuple.Create(e);
                State = PipelineState.Error;
                return false;
            }
        }

        /// <summary>
        ///     Resets the current pipeline.
        /// </summary>
        /// <exception cref="InvalidOperationException">The current pipeline is busy.</exception>
        public void Reset()
        {
            switch (State)
            {
                case PipelineState.None:
                    return;
                case PipelineState.Busy:
                    throw new InvalidOperationException("The current pipeline is busy.");
                case PipelineState.Success:
                case PipelineState.Error:
                    InternalReset();
                    State = PipelineState.None;
                    break;
                default:

                    // Should not get thrown.
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Resets the current pipeline.
        /// </summary>
        protected virtual void InternalReset()
        {
            output = null;
            error = null;
        }
    }
}