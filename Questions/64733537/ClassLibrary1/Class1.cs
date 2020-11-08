using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace ClassLibrary1
{
    public interface ITracer
    {
        void Add(Tracer.Item toTrace, double? ratio);
    }

    public class Tracer : ITracer
    {
        public enum Item
        {
            x,
            y,
            z
        };

        public Dictionary<Item, string> Items = new Dictionary<Item, string>(Enum.GetValues(typeof(Item)).Length);

        public void Add(Item toTrace, double? ratio)
        {
            Items[toTrace] = ratio?.ToString() ?? "null";
        }
    }

    [TestFixture]
    public class Fixture
    {
        [TestCase(Tracer.Item.x, null, "null")]
        [TestCase(Tracer.Item.x, 11, "11")]
        [TestCase(Tracer.Item.y, 12, "12")]
        [TestCase(Tracer.Item.z, 13, "13")]
        public void Test(Tracer.Item item, double? ratio, string expected)
        {
            // Given
            var tracer = new Tracer();

            // When
            tracer.Add(item, ratio);

            // Then
            Assert.AreEqual(expected, tracer.Items[item], $"Unexpected result for {item} and {ratio}. Expected {expected} but was {tracer.Items[item]}.");
        }

        [Test]
        public void Test2()
        {
            // Given
            var tracerMock = new Mock<ITracer>(MockBehavior.Strict);
            tracerMock.Setup(y => y.Add(Tracer.Item.x, 12.00));
            tracerMock.Setup(y => y.Add(Tracer.Item.y, 13.00));
            tracerMock.Setup(y => y.Add(Tracer.Item.z, 14.00));
            var worker = new Worker(tracerMock.Object);

            // When
            worker.Do();

            // Then
            tracerMock.VerifyAll();

            // or remove VerifyAll and check it exactly
            tracerMock.Verify(tracer => tracer.Add(Tracer.Item.x, 12));
            tracerMock.Verify(tracer => tracer.Add(Tracer.Item.y, 13));
            tracerMock.Verify(tracer => tracer.Add(Tracer.Item.z, 14));
        }

        private class Worker
        {
            private readonly ITracer _tracer;

            public Worker(ITracer tracer)
            {
                _tracer = tracer;
            }

            public void Do()
            {
                _tracer.Add(Tracer.Item.x, 12);
            }
        }
    }
}
