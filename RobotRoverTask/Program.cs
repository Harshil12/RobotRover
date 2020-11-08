using System;
using System.Linq;

namespace RobotRoverTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Plateau plateau = Plateau.SetPlateauBoundries();

                Console.WriteLine("Do you want to set initial poistion ? Y or N");
                string UsetwantsToSetIntialPosition = Console.ReadLine();
                RoverPostion rover = new RoverPostion();

                if (UsetwantsToSetIntialPosition.ToUpper() == "Y")
                    rover = rover.SetInitialPosition(plateau,ref rover);

                if (!rover.IsMoveSuccess)
                {
                    Console.WriteLine(rover.ErrorMessage);
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                RoverPostion.MovingRover(plateau, ref rover);

                if (!rover.IsMoveSuccess)
                    Console.WriteLine(rover.ErrorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
