using DominoChainSolver.Models;

namespace DominoChainSolver.Interfaces;

public interface IDominoChainSolver
{
    List<Domino> SolveCircularChain(List<Domino> dominos);
}