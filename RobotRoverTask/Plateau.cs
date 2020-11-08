namespace RobotRoverTask
{
    public class Plateau : IPlateau
    {
        public Plateau(int MaxWidth, int MaxHeight)
        {
            Wiidth = MaxWidth;
            Height = MaxHeight;
        }
        public virtual int Wiidth { get; set; }
        public virtual int Height { get; set; }
       
    }
}
