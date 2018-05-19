using System;
using System.Linq;
using JetBrains.Annotations;
using Xunit;

namespace AdvancedPipeline.Tests
{
    public class PipelineTest
    {
        [Fact]
        [AssertionMethod]
        public void NewPipelineInstanceArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Pipeline(null));
        }

        [Fact]
        [AssertionMethod]
        public void NewPipelineInstanceEmptySequenceTest()
        {
            Pipeline pipeline = new Pipeline(Enumerable.Empty<IFilter>());
            Assert.Equal(typeof(Object), pipeline.InputType);
            Assert.Equal(typeof(Object), pipeline.OutputType);
            Assert.Empty(pipeline);
        }

        [Fact]
        [AssertionMethod]
        public void NewPipelineInstanceNullInSequenceInvalidOperationExceptionTest()
        {
            Assert.Throws<InvalidOperationException>(() => new Pipeline(new IFilter[] { null }));
        }

        [Fact]
        [AssertionMethod]
        public void NewPipelineInstanceTest()
        {
            IFilter[] filters = { new ObjectToStringFilter(), new StringLengthFilter() };
            Pipeline pipeline = new Pipeline(filters);
            Assert.Equal(typeof(Object), pipeline.InputType);
            Assert.Equal(typeof(Int32), pipeline.OutputType);
            Assert.Equal(2, pipeline.Count());
            Assert.True(filters.SequenceEqual(pipeline));
        }

        [Fact]
        [AssertionMethod]
        public void NewPipelineInstanceUnchainableSequenceInvalidOperationExceptionTest()
        {
            Assert.Throws<InvalidOperationException>(() => new Pipeline(new IFilter[] { new ObjectFilter<String>(), new StringLengthFilter() }));
        }

        private class ObjectToStringFilter : FilterBase<Object, String>
        {
            public override String Execute(Object input)
            {
                return input.ToString();
            }
        }

        private class ObjectFilter<T> : FilterBase<T, Object>
        {
            public override Object Execute(T input)
            {
                return input;
            }
        }

        private class StringLengthFilter : FilterBase<String, Int32>
        {
            public override Int32 Execute(String input)
            {
                return input.Length;
            }
        }
    }
}