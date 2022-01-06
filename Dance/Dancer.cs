namespace Dance
{
    public class Dancer
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public Dancer(string name, int points)
        {
            Name = name;
            Points = points;
        }

        public Dancer(string name)
        {
            Name = name;
            Points = 0;
        }

        public static Dancer operator +(Dancer a, Dancer b)
        {
            var name = $"{a.Name} & {b.Name}";
            var points = a.Points + b.Points;

            return new Dancer(name, points);
        }

        public override string ToString()
        {
            return $"{Name} {Points}";
        }
    }
}