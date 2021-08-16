# PlutoRover

This is my solution to the Pluto Rover coding exercise. The solution is divided into two projects:
* PlutoRower library implementation project
* PlutoRover unit tests project.

### Run unit tests:

There are few options to run unit tests:

* dotnet cli command (requires .net 5 sdk to be installed)

```
dotnet test ./PlutoRover.Tests/PlutoRover.Tests.csproj --logger "console;verbosity=normal"
```

* run in docker

```
docker build -t rover-tests . && docker run -it --rm rover-tests
```

### Use PlutoRover library:

Create a Rover instance:

```csharp
var rover = new Rover();
```

Define planet size and obstacles by creating PlanetInformation instance:

```csharp
var planetWithObstacles = new PlanetInformation(10, 10, new List<Obstacle>
            {
                new(1, 0),
                new(1, 2)
            });
```

To navigate Pluto Rover upload planet information and set initial position:

```csharp        
rover.LoadPlanetInformation(planetWithObstacles).SetInitialPosition(new Position(0, 0, Direction.N));
```

Send navigation commands:

```csharp
var result = rover.ExecuteNavigationCommands("FFRFF");
```

# Key requirements

To simplify navigation, the planet has been divided up into a grid. The rover's position and location is represented by a combination of x and y coordinates and a letter representing one of the four cardinal compass points. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North. Assume that the square directly North from (x, y) is (x, y+1).

In order to control a rover, NASA sends a simple string of letters. The only commands you can give the rover are ‘F’,’B’,’L’ and ‘R’

* Implement commands that move the rover forward/backward (‘F’,’B’). The rover may only move forward/backward by one grid point, and must maintain the same heading.
* Implement commands that turn the rover left/right (‘L’,’R’). These commands make the rover spin 90 degrees left or right respectively, without moving from its current spot.
* Implement wrapping from one edge of the grid to another. (Pluto is a sphere after all)
* Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point and reports the obstacle.

### Example:
* Let's say that the rover is located at 0,0 facing North on a 100x100 grid.
* Given the command "FFRFF" would put the rover at 2,2 facing East.
