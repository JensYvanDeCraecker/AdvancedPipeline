using System;
using System.Collections.Generic;
using Xunit;

namespace AdvancedPipeline.Tests
{
    public class FilterTest
    {
        private class ConcreteFilter<T> : Filter<T,T>
        {
            public override T Execute(T input)
            {
                return input;
            }
        }

        [Fact]
        public void ExecuteStructNullArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ConcreteFilter<Int32>().Execute(null));
        }

        [Fact]
        public void ExecuteStructTest()
        {
           Assert.Equal(0, new ConcreteFilter<Int32>().Execute(0));
        }

        [Fact]
        public void ExecuteClassTest()
        {
            Assert.Equal("", new ConcreteFilter<String>().Execute(""));
        }

        [Fact]
        public void ExecuteClassNullTest()
        {
            Assert.Null(new ConcreteFilter<String>().Execute(null));
        }

        [Fact]
        public void ExecuteClassDifferentTypeArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new ConcreteFilter<String>().Execute(new List<Char>()));
        }

        [Fact]
        public void ExecuteStructDifferentTypeArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new ConcreteFilter<Int32>().Execute(true));
        }
    }
}