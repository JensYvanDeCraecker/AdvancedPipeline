using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedPipeline
{
    /// <summary>
    ///     Represents a sequence of filters which can be executed in sequence.
    /// </summary>
    public class Pipeline : PipelineBase
    {
        private readonly IEnumerable<IFilter> filters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pipeline" /> class with the specified filters.
        /// </summary>
        /// <param name="filters">The filters to use in this new pipeline.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filters" /> is <see langword="null" />.</exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="filters" /> contains <see langword="null" />. -or-
        ///     <paramref name="filters" /> contains filters which cannot be executed in sequence.
        /// </exception>
        public Pipeline(IEnumerable<IFilter> filters)
        {
            if (filters == null)
                throw new ArgumentNullException(nameof(filters));
            filters = filters.ToArray();
            IFilter previousFilter = null;
            foreach (IFilter filter in filters)
            {
                if (filter == null)
                    throw new InvalidOperationException("The specified sequence contains null.");
                if (previousFilter != null && !filter.InputType.IsAssignableFrom(previousFilter.OutputType))
                    throw new InvalidOperationException($"{filter.InputType} in {filter} is not assignable from {previousFilter.OutputType} in {previousFilter}.");
                previousFilter = filter;
            }
            InputType = filters.FirstOrDefault()?.InputType ?? typeof(Object);
            OutputType = filters.LastOrDefault()?.OutputType ?? typeof(Object);
            this.filters = filters;
        }

        /// <summary>
        ///     Gets the input type of the current pipeline.
        /// </summary>
        public sealed override Type InputType { get; }

        /// <summary>
        ///     Gets the output type of the current pipeline.
        /// </summary>
        public sealed override Type OutputType { get; }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public sealed override IEnumerator<IFilter> GetEnumerator()
        {
            return filters.GetEnumerator();
        }
    }
}