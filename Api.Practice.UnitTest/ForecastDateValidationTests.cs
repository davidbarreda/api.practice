namespace Api.Practice.UnitTest;

using Api.Practice.Dtos;
using Api.Practice.Validations;
using AwesomeAssertions;

public class ForecastDateValidationTests
{
    private ForecastDateValidation forecastDateValidation;

    public ForecastDateValidationTests()
    {
        this.forecastDateValidation = new ForecastDateValidation();
    }

    [Theory]
    [InlineData("08001", true)]
    [InlineData("00001", false)]
    public void Validate_postal_codes(string value, bool expected)
    {
        // Arrange
        this.forecastDateValidation = new ForecastDateValidation();
        var request = new ForecastDto
        {
            PostalCode = value
        };

        // Act
        var result = this.forecastDateValidation.IsValid(request);

        // Assert
        result.Should().Be(expected);
    }

}