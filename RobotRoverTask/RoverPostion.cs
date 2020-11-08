using System;
using System.Linq;

namespace RobotRoverTask
{
    public enum Directions
    {
        N = 1,
        S = 2,
        E = 3,
        W = 4
    }

    //Assumption it in 90 degree turn in provided direction
    public enum RotateDirection
    {
        L,
        R
    }

    public class RoverPostion : IRover
    {
        public RoverPostion SetInitialPosition(Plateau platueCordinates,ref RoverPostion rover)
        {
             
            while (true)
            {
                Console.WriteLine("Enter start position in <x,y,Directio> format where direction can be N, S, E, W");
                try
                {
                    var intialPosition = Console.ReadLine().Trim().Split(',');
                    if (intialPosition.Count() == 3)
                    {
                        int x = Convert.ToInt32(intialPosition[0]);
                        int y = Convert.ToInt32(intialPosition[1]);
                        if ((x < 0 || x > platueCordinates.Wiidth) || (y < 0 || y > platueCordinates.Height))
                        {
                            rover.IsMoveSuccess = false;
                            rover.ErrorMessage = $"Rover can not be moved out of boundries 0,0 and {platueCordinates.Wiidth},{platueCordinates.Height}";
                            break;
                        }
                
                        Directions direction = (Directions)Enum.Parse(typeof(Directions), intialPosition[2]);
                        rover.X = x;
                        rover.Y = y;
                        rover.Direction = direction;
                    }
                    else
                        throw new Exception();

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please provide input in corrrect formatin <x,y,Directio> format where direction can be N, S, E, W");
                }
                
            }
            rover.IsMoveSuccess = true;
            rover.ErrorMessage = "";
            return rover;
        }

        public RoverPostion()
        {
            X = Y = 10;
            Direction = Directions.N;
        }

        public RoverPostion(int x, int y, Directions position)
        {
            X = x;
            Y = y;
            Direction = position;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string CurrentPosition => X.ToString() + Y.ToString() + Direction.ToString();
        public bool IsMoveSuccess = false;
        public string ErrorMessage = "";
        public Directions Direction { get; set; }

        void TakeTurn(RotateDirection rotateDirection,ref RoverPostion rover)
        {
            switch (this.Direction)
            {
                case Directions.N:
                    rover.Direction = rotateDirection == RotateDirection.L ? Directions.W : Directions.E;
                    break;
                case Directions.S:
                    rover.Direction = rotateDirection == RotateDirection.L ? Directions.E : Directions.W;
                    break;
                case Directions.W:
                    rover.Direction = rotateDirection == RotateDirection.L ? Directions.S : Directions.N;
                    break;
                case Directions.E:
                    rover.Direction = rotateDirection == RotateDirection.L ? Directions.N : Directions.S;
                    break;
            }
        }
        void MovingStraight(int NumberOfSteps,ref RoverPostion rover)
        {
            switch (this.Direction)
            {
                case Directions.N:
                    rover.Y += NumberOfSteps;
                    break;
                case Directions.S:
                    rover.Y -= NumberOfSteps;
                    break;
                case Directions.W:
                    rover.X -= NumberOfSteps;
                    break;
                case Directions.E:
                    rover.X += NumberOfSteps;
                    break;
            }
        }

        public void StartRoverMoving(Plateau platueCordinates, string movingCommand,ref RoverPostion rover)
        {
            foreach (char nextPostion in movingCommand)
            {
                if ((rover.X < 0 || rover.X >= platueCordinates.Wiidth) || (rover.Y < 0 || rover.Y >= platueCordinates.Height))
                {
                    rover.IsMoveSuccess = false;
                    rover.ErrorMessage = $"Rover can not be moved out of boundries 0,0 and {platueCordinates.Wiidth},{platueCordinates.Height}";
                    break;
                }
                if (nextPostion == 'L')
                    TakeTurn(RotateDirection.L, ref rover);
                else if (nextPostion == 'R')
                    TakeTurn(RotateDirection.R,ref rover);
                else if (char.IsDigit(nextPostion) && int.Parse(nextPostion.ToString()) > 0)
                    MovingStraight(int.Parse(nextPostion.ToString()),ref rover);
                else
                    Console.WriteLine($"Invalid Move {nextPostion}. It should be either L or R or positive number");
            }
        }

        internal static void MovingRover(Plateau plateau,ref RoverPostion rover)
        {
            while (true)
            {
                Console.WriteLine("Please enter moving command or enter 'exit' for closing application.");
                var command = Console.ReadLine();
                if (command.ToUpper() == "EXIT")
                    Environment.Exit(0);

                rover.StartRoverMoving(plateau, command,ref rover);
                if (!rover.IsMoveSuccess)
                    break;
                Console.WriteLine(String.Format("Rover current position is {0}", rover.CurrentPosition));
            }
        }
    }
}
