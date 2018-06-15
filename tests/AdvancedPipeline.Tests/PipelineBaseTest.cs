using System;
using System.Collections.Generic;
using AdvancedPipeline.Core;
using JetBrains.Annotations;
using Xunit;

namespace AdvancedPipeline.Tests
{
    public class PipelineBaseTest
    {
        private class ConcretePipeline : PipelineBase
        {
            private readonly Filter filter;

            public ConcretePipeline()
            {
                filter = new Filter(this);
            }

            public sealed override Type InputType { get; } = typeof(Object);

            public sealed override Type OutputType { get; } = typeof(Object);

            public sealed override IEnumerator<IFilter> GetEnumerator()
            {
                yield return filter;
            }

            private class Filter : IFilter
            {
                private readonly ConcretePipeline pipeline;

                public Filter(ConcretePipeline pipeline)
                {
                    this.pipeline = pipeline;
                }

                public Type InputType { get; } = typeof(Object);

                public Type OutputType { get; } = typeof(Object);

                public Object Execute(Object input)
                {
                    if (input == null)
                        throw new ArgumentNullException(nameof(input));
                    Assert.Equal(PipelineState.Busy, pipeline.State);
                    Assert.Throws<InvalidOperationException>(() => pipeline.Reset());
                    Assert.Throws<InvalidOperationException>(() => pipeline.Execute(0));
                    Assert.Throws<InvalidOperationException>(() => pipeline.Output);
                    Assert.Throws<InvalidOperationException>(() => pipeline.Error);
                    return input;
                }
            }
        }

        [Fact]
        [AssertionMethod]
        public void ExecutePipelineErrorTest()
        {
            ConcretePipeline pipeline = new ConcretePipeline();
            Assert.False(pipeline.Execute(null));
            Assert.Equal(PipelineState.Error, pipeline.State);
            Assert.IsType<ArgumentNullException>(pipeline.Error);
            Assert.Throws<InvalidOperationException>(() => pipeline.Output);
            pipeline.Reset();
            Assert.Equal(PipelineState.None, pipeline.State);
            Assert.Throws<InvalidOperationException>(() => pipeline.Output);
            Assert.Throws<InvalidOperationException>(() => pipeline.Error);
        }

        [Fact]
        [AssertionMethod]
        public void ExecutePipelineSuccessTest()
        {
            ConcretePipeline pipeline = new ConcretePipeline();
            Assert.True(pipeline.Execute(0));
            Assert.Equal(PipelineState.Success, pipeline.State);
            Assert.Equal(0, pipeline.Output);
            Assert.Throws<InvalidOperationException>(() => pipeline.Error);
            pipeline.Reset();
            Assert.Equal(PipelineState.None, pipeline.State);
            Assert.Throws<InvalidOperationException>(() => pipeline.Output);
            Assert.Throws<InvalidOperationException>(() => pipeline.Error);
        }

        [Fact]
        [AssertionMethod]
        public void NewPipelineBaseInstanceTest()
        {
            ConcretePipeline pipeline = new ConcretePipeline();
            Assert.Equal(PipelineState.None, pipeline.State);
            Assert.Throws<InvalidOperationException>(() => pipeline.Output);
            Assert.Throws<InvalidOperationException>(() => pipeline.Error);
        }
    }
}