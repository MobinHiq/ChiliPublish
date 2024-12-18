using DominoChainSolver.Extensions;
using DominoChainSolver.Models;
using FluentAssertions;
using Xunit.Abstractions;

namespace DominoChainSolver.Tests;

public class DominoChainTests
{
    private readonly ITestOutputHelper _outputHelper;
    
    public DominoChainTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void ValidChain_SimpleCase()
    {
        // Arrange
        var dominos = new List<Domino>
        {
            new Domino(2, 1),
            new Domino(2, 3),
            new Domino(1, 3)
        };

        // Act
        var result = dominos.SolveAllCircularChains();
        
        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();

        // Check each chain for circularity
        foreach (var chain in result)
        {
            chain.First().Left.Should().Be(chain.Last().Right);
            chain.Should().HaveCount(dominos.Count); 
        }
    }

    [Fact]
    public void InvalidChain_NoCirclePossible()
    {
        // Arrange
        var dominos = new List<Domino>
        {
            new Domino(1, 2),
            new Domino(4, 1),
            new Domino(2, 3)
        };

        // Act
        var result = dominos.SolveAllCircularChains();
        
        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void EmptyInput_ReturnsEmptyList()
    {
        // Arrange
        var dominos = new List<Domino> { };
        
        // Act
        var result = dominos.SolveAllCircularChains();
        
        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SingleDomino_WithMatchingEnds_ReturnsValidChain()
    {
        // Arrange
        var dominos = new List<Domino>
        {
            new Domino(1, 1)
        };

        // Act
        var result = dominos.SolveAllCircularChains();
        
        // Assert
        result.Should().NotBeNull()
            .And.HaveCount(1);
        
        var chain = result.First();
        chain.First().Left.Should().Be(chain.First().Right); 
    }
    
    [Fact]
    public void SolveCircularChain_WithComplexInValidChain_ReturnsEmpty()
    {
        // Arrange
        var dominos = new List<Domino>
        {
            new(2, 4),
            new(1, 3),
            new(3, 6),
            new(6, 5),
            new(5, 4),
            new(4, 7),
            new(7, 1),
            new(1, 2) 
        };

        // Act
        var result = dominos.SolveAllCircularChains();

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void SolveCircularChain_WithComplexValidChain_ReturnsCorrectSolution()
    {
        // Arrange
        var dominos = new List<Domino>
        {
            new(2, 4),
            new(1, 3),
            new(3, 6),
            new(6, 5),
            new(5, 4),
            new(4, 7),
            new(7, 1),
            new(1, 2),
            new(4, 1)  
        };

        // Act
        var result = dominos.SolveAllCircularChains();

        // Assert
        result.Should().NotBeEmpty();
        
        _outputHelper.WriteLine("Found chains:");
        foreach (var chain in result)
        {
            _outputHelper.WriteLine("Chain:");
            for (int i = 0; i < chain.Count; i++)
            {
                _outputHelper.WriteLine($"{i}: [{chain[i].Left}|{chain[i].Right}]");
            }
            
            chain.First().Left.Should().Be(chain.Last().Right);

            chain.Should().HaveCount(dominos.Count);
        }
    }
}