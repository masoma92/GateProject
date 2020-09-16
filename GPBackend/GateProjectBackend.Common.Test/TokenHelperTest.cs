﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    }
}