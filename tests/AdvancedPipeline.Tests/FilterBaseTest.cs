using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace AdvancedPipeline.Tests
{
    public class FilterBaseTest
    {
        private class ConcreteFilter<T> : FilterBase<T,T>
        {
            public override T Execute(T input)
            {
                return input;
            }
        }

        [Fact]
        [AssertionMethod]
        public void ExecuteStructNullArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => ((IFilter)new ConcreteFilter<Int32>()).Execute(null));
        }

        [Fact]
        [AssertionMethod]
        public void ExecuteStructTest()
        {
           Assert.Equal(0, new ConcreteFilter<Int32>().Execute(0));
        }

        [Fact]
        [AssertionMethod]
        public void ExecuteClassTest()
        {
            Assert.Equal("", new ConcreteFilter<String>().Execute(""));
        }

        [Fact]
        [AssertionMethod]
        public void ExecuteClassNullTest()
        {
            Assert.Null(new ConcreteFilter<String>().Execute(null));
        }

        [Fact]
        [AssertionMethod]
        public void ExecuteClassDifferentTypeArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => ((IFilter)new ConcreteFilter<String>()).Execute(new List<Char>()));
        }

        [Fact]
        [AssertionMethod]
        public void ExecuteStructDifferentTypeArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => ((IFilter)new ConcreteFilter<Int32>()).Execute(true));
        }
    }
}