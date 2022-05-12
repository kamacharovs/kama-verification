using KamaVerification.Services;
using Xunit;
using FluentAssertions;
using NSubstitute;
using Microsoft.Extensions.Logging;

namespace KamaVerification.Tests.Services
{
    public class UnitTest1
    {
        private readonly IVerificationRepository _repo;

        public UnitTest1()
        {
            _repo = new VerificationRepository(Substitute.For<ILogger<VerificationRepository>>());
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void GenerateCode(int length)
        {
            // Arrange & Act
            var code = _repo.GenerateCode(length);

            // Assert
            code.Should().NotBeNull();
            code.Length.Should().Be(length);
        }

        [Theory]
        [InlineData("1234", "1235")]
        [InlineData("8475", "2147")]
        [InlineData("5285", "5793")]
        public void CalculateDifference(string givenCode, string expectedCode)
        {
            // Arrange & Act
            var act = _repo.CalculateDifference(givenCode, expectedCode);

            // Assert
            act.Should().BeGreaterThan(0);
        }
    }
}