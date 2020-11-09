using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotRoverTask;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotRoverTask.Tests
{
    [TestClass()]
    public class RoverPostionWhen_Moving_Rover
    {
        [TestMethod()]
        public void StartRoverMovingValid_Command_Sequence_Movement_Is_Successful()
        {
            Plateau plateau = new Plateau(50, 50);
            RoverPostion roverPostion = new RoverPostion(5, 5, Directions.N);
            roverPostion.StartRoverMoving(plateau, "111", ref roverPostion);
            Assert.IsTrue(roverPostion.CurrentPosition == "58N", "Moved successfully");
        }

        [TestMethod()]
        public void Impermissible_Command_Sequence_Causes_Fall_Off_Plateau()
        {
            Plateau plateau = new Plateau(5, 5);
            RoverPostion roverPostion = new RoverPostion(4, 4, Directions.N);
            roverPostion.StartRoverMoving(plateau, "111", ref roverPostion);
            Assert.IsFalse(roverPostion.IsMoveSuccess);
        }
    }
}