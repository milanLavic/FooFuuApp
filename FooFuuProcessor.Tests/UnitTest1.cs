using FooFuu.Core.Helper;
using FooFuu.Core.Errors;
using FooFuu.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FooFuu.Core;
using Xunit;

public class NumberProcessorBaseTests
{
    private class TestNumberProcessor : NumberProcessorBase
    {

        protected override string ProcessNumber(int number)
        {
            return number.ToString();
        }
    }
    
    [Fact]
    public void IsFooFuu_NumberDivisibleBy4_ReturnsTrue()
    {
        // Arrange
        var processor = new FooFuuProcessor();

        // Act
        var result = processor.IsFooFuu(8);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsFooFuu_NumberNotDivisibleBy4_ReturnsFalse()
    {
        // Arrange
        var processor = new FooFuuProcessor();

        // Act
        var result = processor.IsFooFuu(5);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void SetInputRange_ValidRange_SetsInputNumbers()
    {
        // Arrange
        var processor = new TestNumberProcessor();

        // Act
        processor.SetInputRange(1, 5);

        // Assert
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, processor.InputNumbers);
    }

    [Fact]
    public void SetInputRange_InvalidRange_ThrowsValidationException()
    {
        // Arrange
        var processor = new TestNumberProcessor();

        // Act & Assert
        Assert.Throws<ValidationException>(() => processor.SetInputRange(5, 1));
    }

    [Fact]
    public async Task ProcessNumbersAsync_ValidInput_ProcessesNumbers()
    {
        // Arrange
        var processor = new TestNumberProcessor();
        var mockOutputDevice = new Mock<IOutputDevice>();
        mockOutputDevice.Setup(od => od.WriteAsync(It.IsAny<IEnumerable<string>>())).Returns(Task.CompletedTask);

        // Act
        await processor.ProcessNumbersAsync(1, 3, mockOutputDevice.Object);

        // Assert
        mockOutputDevice.Verify(od => od.WriteAsync(It.Is<IEnumerable<string>>(results =>
            results.Contains("1") && results.Contains("2") && results.Contains("3"))), Times.Once);
    }

    [Fact]
    public async Task ProcessNumbersAsync_NullOutputDevice_ThrowsValidationException()
    {
        // Arrange
        var processor = new TestNumberProcessor();

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => processor.ProcessNumbersAsync(1, 3, null));
    }
    
    [Fact]
    public async Task ProcessNumbersAsync_ValidInput_ProcessesNumbersCorrectly()
    {
        // Arrange
        var processor = new FooFuuProcessor();
        var mockOutputDevice = new Mock<IOutputDevice>();
        mockOutputDevice.Setup(od => od.WriteAsync(It.IsAny<IEnumerable<string>>())).Returns(Task.CompletedTask);

        // Act
        await processor.ProcessNumbersAsync(1, 5, mockOutputDevice.Object);

        // Assert
        mockOutputDevice.Verify(od => od.WriteAsync(It.Is<IEnumerable<string>>(results =>
            results.SequenceEqual(new List<string> { "1", "foo", "3", "foofuu", "5" }))), Times.Once);
    }
}