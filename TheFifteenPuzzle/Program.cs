var board = new Board();
var input = new InputController();


while (true)
{
    board.ProcessMove(input.GetInput());
    board.Update();
}


public class InputController()
{
    public Direction GetInput()
    {
        var key = Console.ReadKey(false).Key;

        return key switch
        {
            ConsoleKey.W => Direction.Up,
            ConsoleKey.S => Direction.Down,
            ConsoleKey.A => Direction.Left,
            ConsoleKey.D => Direction.Right,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class Board
{
    private const int BoardWidth = 3; //x
    private const int BoardHeight = 3; //y
    
    private int _blankPosX = 0;
    private int _blankPosY = 0;

    private int? [,] _spaces;

    public Board()
    {
        _spaces = new int?[BoardWidth, BoardHeight];
        PickRandomStartPosition();
        FillSpaces();
        Update();
    }

    public void ProcessMove(Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
            {
                if (_blankPosY + 1 >= BoardHeight) break;
                
                var swap = _spaces[_blankPosX, _blankPosY + 1];
                _spaces[_blankPosX, _blankPosY + 1] = null;
                _spaces[_blankPosX, _blankPosY] = swap;

                _blankPosY += 1;
                break;
            }
            case Direction.Up:
            {
                if (_blankPosY - 1 < 0) break;
                
                var swap = _spaces[_blankPosX, _blankPosY - 1];
                _spaces[_blankPosX, _blankPosY - 1] = null;
                _spaces[_blankPosX, _blankPosY] = swap;

                _blankPosY -= 1;
                break;
            }
            case Direction.Right:
            {
                if (_blankPosX + 1 >= BoardWidth) break;
                
                var swap = _spaces[_blankPosX + 1, _blankPosY];
                _spaces[_blankPosX + 1, _blankPosY ] = null;
                _spaces[_blankPosX, _blankPosY] = swap;

                _blankPosX += 1;
                break;
            }
            case Direction.Left:
            {
                if (_blankPosX - 1 < 0) break;
                
                var swap = _spaces[_blankPosX - 1, _blankPosY];
                _spaces[_blankPosX - 1, _blankPosY] = null;
                _spaces[_blankPosX, _blankPosY] = swap;

                _blankPosX -= 1;
                break;
            }
        }
        Console.WriteLine(direction);
    }
    public void Update()
    {
        Console.Clear();
        Render();

        if (HasWon())
        {
            Console.WriteLine("You're winner");
            Environment.Exit(0);
        };
    }

    private bool HasWon()
    {
        var flattened = new int? [(BoardWidth * BoardHeight) - 1];
        var flatTrack = 0;
        //flatten the 2d array into one array.
        for (var y = 0; y < BoardHeight; y++)
        {
            for (var x = 0; x < BoardWidth; x++)
            {
                if (_spaces[x, y] is null)
                    continue;
                
                flattened[flatTrack] = _spaces[x, y];
                flatTrack++;
            }
        }

        for (int i = 1; i < flattened.Length; i++)
            if (flattened[i - 1] != flattened[i] -1) 
                return false;
        

        return true;
    }
    
    private void Render()
    {
        for (int y = 0; y < _spaces.GetLength(0); y++)
        {
            for (int x = 0; x < _spaces.GetLength(1); x++)
            {
                if (x != BoardWidth - 1)
                {
                    Console.Write(_spaces[x, y] is null ? " \u2588, " : $" {_spaces[x, y]}, ");
                }
                else
                {
                    Console.Write(_spaces[x, y] is null ? "" : $"{_spaces[x, y]}");
                }
            }
            Console.WriteLine();
        }
    }
    private void PickRandomStartPosition()
    {
        var random = new Random();
        var x = random.Next(0, BoardWidth);
        var y = random.Next(0, BoardHeight);

        _blankPosX = x;
        _blankPosY = y;
        
        _spaces[x,y] = null;
    }
    private void FillSpaces()
    {
        var random = new Random();

        for (int y = 0; y < _spaces.GetLength(0); y++)
        {
            for (int x = 0; x < _spaces.GetLength(1); x++)
            {
                if (x == _blankPosX && y == _blankPosY)
                    continue;
                
                int? number = null;
                
                while (number is null || IsNumberUsed(number))
                {
                    number = random.Next(1, BoardHeight * BoardWidth + 1);
                }
                _spaces[x, y] = Convert.ToInt32(number);
            }
        }

        bool IsNumberUsed(int? number)
        {
            for (int y = 0; y < _spaces.GetLength(0); y++)
            {
                for (int x = 0; x < _spaces.GetLength(1); x++)
                {
                    if (_spaces[x, y] == number)
                        return true;
                }
            }
            
            return false;
        }
    }
}

public enum Direction {Up, Down, Left, Right};