using KamaVerification.Services;
using Xunit;
using FluentAssertions;

namespace KamaVerification.Tests.Services
{
    public class UnitTest1
    {
        private readonly IVerificationRepository _repo;

        public UnitTest1()
        {
            _repo = new VerificationRepository(null, null);
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
    }
}