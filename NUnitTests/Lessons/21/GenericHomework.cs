﻿namespace Lesson21
{
    internal class GenericHomework
    {
        private static string GetParameterType<T>(T parameter)
        {
            if (parameter == null)
            {
                return "Parameter is null";
            }

            return $"Data type: {parameter.GetType()}";
        }

        [Test]
        public void GenericFunction()
        {
            var intType = GetParameterType(123);
            Assert.That(intType, Is.EqualTo("Data type: System.Int32"));

            var stringType = GetParameterType("some string");
            Assert.That(stringType, Is.EqualTo("Data type: System.String"));

            var doubleType = GetParameterType(new List<double>() { 1.23 });
            Assert.That(doubleType, Is.EqualTo("Data type: System.Collections.Generic.List`1[System.Double]"));
        }
    }
}
