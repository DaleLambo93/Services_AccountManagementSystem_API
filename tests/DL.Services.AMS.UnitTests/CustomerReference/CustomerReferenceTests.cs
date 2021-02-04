using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace DL.Services.AMS.UnitTests.CustomerReference
{
    public class CustomerReferenceTests
    {
        [Theory,
         InlineData(0),
         InlineData(1),
         InlineData(2),
         InlineData(3),
         InlineData(4)]
        public void CustomerReferenceHelper_ContainsCorrectCode(int number)
        {
            //Arrange
            var reference = (Reference)number;

            //Act
            var customerReference = CustomerReferenceHelper.GetCustomerReference(reference);
            var dictionary = GetConstantValues(typeof(ReferenceCode));

            //Assert
            Assert.Contains(dictionary[reference.ToString()], customerReference);      
        }

        [Fact]
        public void CustomerReferenceHelper_ThrowsException()
        {
            //Arrange 
            int number = 5;
            var reference = (Reference)number;
            string expectedMessage = $"Reference {reference} not found.";
            //Act
            Action act = () => CustomerReferenceHelper.GetCustomerReference(reference);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }

        private static Dictionary<string, string> GetConstantValues(Type type)
        {
            var dictionary = new Dictionary<string, string>();
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach(FieldInfo fieldInfo in fieldInfos)
            {
                dictionary.Add(fieldInfo.Name, fieldInfo.GetValue(null).ToString());
            }

            return dictionary;
        }
    }
}
