using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GateProjectBackend.Common.Test
{
    [TestFixture]
    public class TokenHelperTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GenerateToken_Should_ReturnNotEmptyString_When_Called()
        {
            byte[] testData = new byte[] { 56, 128, 66 };
            var token = TokenHelper.GenerateToken(testData);
            var result = TokenHelper.IsValidToken(token, testData);
            Assert.IsTrue(result);
        }

        [TestCase(new byte[] { 56, 128, 66 })]
        public void Encrypt_Should_ReturnSameValueAsInput_When_Called(byte[] input)
        {
            var encryptedData = TokenHelper.Encrypt<byte[]>(input);
            var decryptedData = TokenHelper.Decrypt<byte[]>(encryptedData);
            Assert.AreEqual(input, decryptedData);
        }

        [TestCase("")]
        [TestCase("asd")]
        [TestCase("asdodjhfiwueghf2893572905r782r0u2hfinwkvjbn3oi4njuvo3i9y3ibnvg")]
        [TestCase("soma.makai@gmail.com")]
        public void Encrypt_Should_ReturnSameValueAsInput_When_Called(string input)
        {
            var encryptedData = TokenHelper.Encrypt<string>(input);
            var decryptedData = TokenHelper.Decrypt<string>(encryptedData);
            Assert.AreEqual(input, decryptedData);
        }
    }
}
