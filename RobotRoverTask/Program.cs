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
                Plateau plateau = SetPlateauBoundries();

                Console.WriteLine("Do you want to set initial poistion ? Y or N");
                string UsetwantsToSetIntialPosition = Console.ReadLine();
                RoverPostion rover = new RoverPostion();

                if (UsetwantsToSetIntialPosition.ToUpper() == "Y")
                    rover = SetInitialPosition(ref rover);

                MovingRover(plateau, rover);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static Plateau SetPlateauBoundries()
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
                catch(Exception)
                {
                    Console.WriteLine("Please provide input in corrrect format.<width,height> e.g. 2,5");
                }
            }
        }

        private static RoverPostion SetInitialPosition(ref RoverPostion rover)
        {
            Console.WriteLine("Enter start position in <x,y,Directio> format where direction can be N, S, E, W");
            while (true)
            {
                try
                {
                    var intialPosition = Console.ReadLine().Trim().Split(',');
                    if (intialPosition.Count() == 3)
                    {
                        rover = new RoverPostion(Convert.ToInt32(intialPosition[0]),
                                                 Convert.ToInt32(intialPosition[1]),
                                                 (Directions)Enum.Parse(typeof(Directions), intialPosition[2]));
                    }
                    else
                        throw new Exception();

                    return rover;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide input in corrrect formatin <x,y,Directio> format where direction can be N, S, E, W");
                }
            }
        }

        private static void MovingRover(Plateau plateau, RoverPostion rover)
        {
            while (true)
            {
                Console.WriteLine("Please enter moving command or enter 'exit' for closing application.");
                var command = Console.ReadLine();
                if (command.ToUpper() == "EXIT")
                    Environment.Exit(0);

                rover.StartRoverMoving(plateau, command);
                Console.WriteLine(String.Format("Rover current position is {0}{1}{2}", rover.X, rover.Y, rover.Direction));
            }
        }
    }
}
