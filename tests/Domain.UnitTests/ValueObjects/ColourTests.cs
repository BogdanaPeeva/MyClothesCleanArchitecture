using MyClothesCA.Domain.Exceptions;
using MyClothesCA.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace MyClothesCA.Domain.UnitTests.ValueObjects;

public class ColourTests
{
    [Test]
    public void ShouldReturnCorrectColourCode()
    {
        var code = "#FFFFFF";

        var colour = Colourr.From(code);

        colour.Code.Should().Be(code);
    }

    [Test]
    public void ToStringReturnsCode()
    {
        var colour = Colourr.White;

        colour.ToString().Should().Be(colour.Code);
    }

    [Test]
    public void ShouldPerformImplicitConversionToColourCodeString()
    {
        string code = Colourr.White;

        code.Should().Be("#FFFFFF");
    }

    [Test]
    public void ShouldPerformExplicitConversionGivenSupportedColourCode()
    {
        var colour = (Colourr)"#FFFFFF";

        colour.Should().Be(Colourr.White);
    }

    [Test]
    public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
    {
        FluentActions.Invoking(() => Colourr.From("##FF33CC"))
            .Should().Throw<UnsupportedColourException>();
    }
}
