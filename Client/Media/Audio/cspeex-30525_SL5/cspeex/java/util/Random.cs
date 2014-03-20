namespace java.util
{
    public class Random
    {
        private System.Random innerRandom;

        public Random()
        {
            this.innerRandom = new System.Random();
        }

        public int nextInt()
        {
            return innerRandom.Next();
        }

        public int nextInt(int max)
        {
            return innerRandom.Next(max);
        }

        public float nextFloat()
        {
            return (float)innerRandom.NextDouble();
        }

        public static double random()
        {
            return new System.Random((int)System.DateTime.Now.Ticks).NextDouble();
        }
    }
}
