using DominoChainSolver.Models;

namespace DominoChainSolver.Extensions
{
    public static class DominoExtensions
    {
        public static List<List<Domino>> SolveAllCircularChains(this List<Domino> dominos)
        {
            ArgumentNullException.ThrowIfNull(dominos);

            if (dominos.Count == 0)
                return new List<List<Domino>>();

            var allChains = new HashSet<string>(); 
            var uniqueChains = new List<List<Domino>>();
            var visited = new HashSet<Domino>();
            var chain = new List<Domino>();

            // Try each domino as starting point
            foreach (var startDomino in dominos)
            {
                // Try with normal start domino
                visited.Clear();
                chain.Clear();
                chain.Add(startDomino);
                visited.Add(startDomino);

                TryBuildChain(dominos, chain, visited, allChains, uniqueChains);

                // Try with flipped start domino
                visited.Clear();
                chain.Clear();
                var flippedStart = startDomino.Flip();
                chain.Add(flippedStart);
                visited.Add(startDomino); 

                TryBuildChain(dominos, chain, visited, allChains, uniqueChains);
            }

            return uniqueChains;
        }

        private static void TryBuildChain(
            List<Domino> allDominos,
            List<Domino> chain,
            HashSet<Domino> visited,
            HashSet<string> allChains,
            List<List<Domino>> uniqueChains)
        {
            if (visited.Count == allDominos.Count)
            {
                // Check if it forms a circle
                if (chain.First().Left == chain.Last().Right)
                {
                    var chainString = string.Join(",", chain.Select(d => $"{d.Left}|{d.Right}"));
                    if (allChains.Add(chainString))
                    {
                        uniqueChains.Add(new List<Domino>(chain));
                    }
                }
                return;
            }

            var lastNumber = chain.Last().Right;

            foreach (var domino in allDominos)
            {
                if (visited.Contains(domino))
                    continue;

                // Try adding the domino in its normal orientation
                if (domino.Left == lastNumber)
                {
                    chain.Add(domino);
                    visited.Add(domino);
                    TryBuildChain(allDominos, chain, visited, allChains, uniqueChains);
                    chain.RemoveAt(chain.Count - 1);
                    visited.Remove(domino);
                }

                // Try adding the domino in its flipped orientation
                if (domino.Right == lastNumber)
                {
                    var flipped = domino.Flip();
                    if (!visited.Contains(flipped)) 
                    {
                        chain.Add(flipped);
                        visited.Add(domino); 
                        TryBuildChain(allDominos, chain, visited, allChains, uniqueChains);
                        chain.RemoveAt(chain.Count - 1);
                        visited.Remove(domino);
                    }
                }
            }
        }
    }
}