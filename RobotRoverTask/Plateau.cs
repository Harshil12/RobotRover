using System;
using System.Linq;

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

        internal static Plateau SetPlateauBoundries()
        {
            Console.WriteLine("Please enter boundris for Plateau in <width,height> e.g. 2,5");
            while (true)
            {
                try
                {
                    var boundries = Console.ReadLine().Trim().Split(',').Select(int.Parse).ToList();

                    if (boundries.Count != 2)
                        throw new Exception("Please provide in correct format");

                    // Set max height and max width
                    Plateau plateau = new Plateau(boundries[0], boundries[1]);
                    return plateau;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide input in corrrect format.<width,height> e.g. 2,5");
                }
            }
        }

    }
}
