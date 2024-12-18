namespace DominoChainSolver.Models
{
    public class Domino
    {
        public int Left { get; }
        public int Right { get; }

        public Domino(int left, int right)
        {
            if (left < 0 || right < 0)
                throw new ArgumentException("Domino values must be non-negative");

            Left = left;
            Right = right;
        }

        public Domino Flip() => new Domino(Right, Left);

        public override string ToString() => $"[{Left}|{Right}]";

        public override bool Equals(object obj)
        {
            if (obj is Domino other)
                return (Left == other.Left && Right == other.Right) ||
                       (Left == other.Right && Right == other.Left);
            return false;
        }

        public override int GetHashCode() => Left.GetHashCode() ^ Right.GetHashCode();
    }
}