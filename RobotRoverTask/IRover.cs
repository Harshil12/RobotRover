﻿namespace RobotRoverTask
{
    interface IRover
    {
        void StartRoverMoving(Plateau platueCordinates, string movingPosition,ref RoverPostion roverPostion);
    }
}
