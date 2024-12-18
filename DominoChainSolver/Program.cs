using DominoChainSolver.Extensions;
using DominoChainSolver.Models;

namespace DominoChainSolver;

class Program
{
    public static void Main(string[] args)
    {
        List<Domino> dominos = new List<Domino>
        {
            new Domino(2, 1),
            new Domino(2, 3),
            new Domino(1, 3)
        };

        var result = dominos.SolveAllCircularChains();

        if (result.Count > 0)
        {
            Console.WriteLine("Circular chain found:");
            foreach (var chains in result)
            {
                foreach (var chain in chains)
                {
                    Console.Write($"{chain}");
                    
                }
                
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No circular chain possible.");
        }
    }
}