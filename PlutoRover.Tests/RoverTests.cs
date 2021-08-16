using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PlutoRover.Enums;
using PlutoRover.Exceptions;
using PlutoRover.Models;

namespace PlutoRover.Tests
{
    public class RoverTests
    {
        private PlanetInformation defaultPlanet;
        private Rover rover;

        [SetUp]
        public void Setup()
        {
            defaultPlanet = new PlanetInformation(100, 100);
            rover = new Rover().LoadPlanetInformation(defaultPlanet);
        }

        [TestCase(0, 0, Direction.N, "0,0,N")]
        [TestCase(3, 4, Direction.S, "3,4,S")]
        public void Should_Stay_At_Initial_Position(int x, int y, Direction d, string newPosition)
        {
            rover.SetInitialPosition(new Position(x, y, d));

            var position = rover.ExecuteNavigationCommands("");

            position.ResultCode.Should().Be(ResultCode.Ok);
            position.Position.Should().Be(newPosition);
        }

        [TestCase(3, 4, Direction.N, "3,5,N")]
        [TestCase(3, 4, Direction.E, "4,4,E")]
        [TestCase(3, 4, Direction.S, "3,3,S")]
        [TestCase(3, 4, Direction.W, "2,4,W")]
        public void Should_Move_Forward(int x, int y, Direction d, string newPosition)
        {
            rover.SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands("F");

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [TestCase(3, 4, Direction.N, "3,3,N")]
        [TestCase(3, 4, Direction.E, "2,4,E")]
        [TestCase(3, 4, Direction.S, "3,5,S")]
        [TestCase(3, 4, Direction.W, "4,4,W")]
        public void Should_Move_Back(int x, int y, Direction d, string newPosition)
        {
            rover.SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands("B");

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [TestCase(3, 4, Direction.N, "3,4,W")]
        [TestCase(3, 4, Direction.W, "3,4,S")]
        [TestCase(3, 4, Direction.S, "3,4,E")]
        [TestCase(3, 4, Direction.E, "3,4,N")]
        public void Should_Turn_Left(int x, int y, Direction d, string newPosition)
        {
            rover.SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands("L");

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [TestCase(3, 4, Direction.N, "3,4,E")]
        [TestCase(3, 4, Direction.E, "3,4,S")]
        [TestCase(3, 4, Direction.S, "3,4,W")]
        [TestCase(3, 4, Direction.W, "3,4,N")]
        public void Should_Turn_Right(int x, int y, Direction d, string newPosition)
        {
            rover.SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands("R");

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [Test]
        public void Should_Pass_Example_Test()
        {
            rover.SetInitialPosition(new Position(0, 0, Direction.N));

            var result = rover.ExecuteNavigationCommands("FFRFF");

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be("2,2,E");
        }
        
        [TestCase("f", "0,0,N", "f")]
        [TestCase("FFZFF", "0,2,N", "Z")]
        public void Should_Not_Execute_Unknown_Commands(string command, string newPosition, string unknownCommand)
        {
            rover.SetInitialPosition(new Position(0, 0, Direction.N));

            var result = rover.ExecuteNavigationCommands(command);

            result.ResultCode.Should().Be(ResultCode.BadRequest);
            result.Position.Should().Be(newPosition);
            result.Message.Should().Contain(unknownCommand);
        }

        [TestCase(3, 0, Direction.N, "B", "3,100,N")]
        [TestCase(3, 0, Direction.S, "F", "3,100,S")]
        [TestCase(3, 100, Direction.N, "F", "3,0,N")]
        [TestCase(3, 100, Direction.S, "B", "3,0,S")]
        [TestCase(0, 5, Direction.E, "B", "100,5,E")]
        [TestCase(0, 5, Direction.W, "F", "100,5,W")]
        [TestCase(100, 5, Direction.E, "F", "0,5,E")]
        [TestCase(100, 5, Direction.W, "B", "0,5,W")]
        public void Should_Wrap_From_One_Edge_To_Another(int x, int y, Direction d, string command, string newPosition)
        {
            rover.SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands(command);

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [TestCase(3, int.MaxValue, Direction.N, "F", "3,0,N")]
        [TestCase(3, int.MaxValue, Direction.S, "B", "3,0,S")]
        [TestCase(int.MaxValue, 5, Direction.E, "F", "0,5,E")]
        [TestCase(int.MaxValue, 5, Direction.W, "B", "0,5,W")]
        public void Should_Wrap_On_Max_Size(int x, int y, Direction d, string command, string newPosition)
        {
            rover.LoadPlanetInformation(new PlanetInformation(int.MaxValue, int.MaxValue))
                .SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands(command);

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [TestCase(0, 0, Direction.N, "F", "0,0,N")]
        [TestCase(0, 0, Direction.S, "B", "0,0,S")]
        [TestCase(0, 0, Direction.E, "F", "0,0,E")]
        [TestCase(0, 0, Direction.W, "B", "0,0,W")]
        public void Should_Wrap_On_Min_Size(int x, int y, Direction d, string command, string newPosition)
        {
            rover.LoadPlanetInformation(new PlanetInformation(0, 0))
                .SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands(command);

            result.ResultCode.Should().Be(ResultCode.Ok);
            result.Position.Should().Be(newPosition);
        }

        [TestCase(0, 0, Direction.E, "F", "0,0,E", "1,0")]
        [TestCase(0, 0, Direction.N, "FFRFF", "0,2,E", "1,2")]
        public void Should_Detect_Obstacles(int x, int y, Direction d, string command, string newPosition, string obstacle)
        {
            var planetWithObstacles = new PlanetInformation(10, 10, new List<Obstacle>
            {
                new(1, 0),
                new(1, 2)
            });
            rover.LoadPlanetInformation(planetWithObstacles)
                .SetInitialPosition(new Position(x, y, d));

            var result = rover.ExecuteNavigationCommands(command);

            result.ResultCode.Should().Be(ResultCode.Forbidden);
            result.Position.Should().Be(newPosition);
            result.Message.Should().Contain(obstacle);
        }

        [Test]
        public void Initial_Position_Requires_Planet_Information()
        {
            Action act = () => new Rover().SetInitialPosition(new Position(0, 0, Direction.N));

            act.Should().Throw<InvalidPositionException>();
        }

        [Test]
        public void Initial_Position_Should_Be_Set_Inside_Planet_Boundaries()
        {
            Action act = () => new Rover()
                .LoadPlanetInformation(new PlanetInformation(10, 10))
                .SetInitialPosition(new Position(20, 20, Direction.N));

            act.Should().Throw<InvalidPositionException>();
        }
    }
}