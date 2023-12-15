namespace _15._2
{
    internal class Box
    {
        public int Id { get; init; }
        public List<Lens> Lenses { get; } = new List<Lens>();
        public int GetFocusingPower()
        {
            int power = 0;
            for (int i = 0; i < Lenses.Count; i++)
                power += (Id + 1) * (i + 1) * Lenses[i].FocalLength;
            return power;
        }
    }
}
