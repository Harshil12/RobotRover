using System;

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
        L, // Left
        R
    }

    public class RoverPostion : IRover
    {
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
        public Directions Direction { get; set; }

        void TakeTurn(RotateDirection rotateDirection)
        {
            switch (this.Direction)
            {
                case Directions.N:
                    Direction = rotateDirection == RotateDirection.L ? Directions.W : Directions.E;
                    break;
                case Directions.S:
                    Direction = rotateDirection == RotateDirection.L ? Directions.E : Directions.W;
                    break;
                case Directions.W:
                    Direction = rotateDirection == RotateDirection.L ? Directions.S : Directions.N;
                    break;
                case Directions.E:
                    Direction = rotateDirection == RotateDirection.L ? Directions.N : Directions.S;
                    break;
            }
        }
        void MovingStraight(int NumberOfSteps)
        {
            switch (this.Direction)
            {
                case Directions.N:
                    Y += NumberOfSteps;
                    break;
                case Directions.S:
                    Y -= NumberOfSteps;
                    break;
                case Directions.W:
                    X -= NumberOfSteps;
                    break;
                case Directions.E:
                    X += NumberOfSteps;
                    break;
            }
        }

        public void StartRoverMoving(Plateau platueCordinates, string movingCommand)
        {
            string sOutOfBoundryMessage = string.Format("Rover can not be moved out of boundries {0},{1} and {2},{3}", 0, 0, platueCordinates.Wiidth, platueCordinates.Height);
            foreach (char nextPostion in movingCommand)
            {
                if ((X < 0 || X > platueCordinates.Wiidth) || (Y < 0 || Y > platueCordinates.Height))
                    throw new Exception(sOutOfBoundryMessage);

                if (nextPostion == 'L')
                    TakeTurn(RotateDirection.L);
                else if (nextPostion == 'R')
                    TakeTurn(RotateDirection.R);
                else if (char.IsDigit(nextPostion) && int.Parse(nextPostion.ToString()) > 0)
                    MovingStraight(int.Parse(nextPostion.ToString()));
                else
                    Console.WriteLine($"Invalid Move {nextPostion}. It should be either L or R or positive number");
            }
        }
    }
}
