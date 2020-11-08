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
    }
}
