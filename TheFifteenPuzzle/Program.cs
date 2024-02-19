var board = new Board();

public class Board
{
    private const int BOARD_WIDTH = 4; //x
    private const int BOARD_HEIGHT = 4; //y

    private int? [,] _spaces;

    public Board()
    {
        _spaces = new int?[BOARD_WIDTH, BOARD_HEIGHT];
        FillSpaces();
        PickRandomStartPosition();
        Render();
    }

    private void Render()
    {
        for (int y = 0; y < _spaces.GetLength(0); y++)
        {
            for (int x = 0; x < _spaces.GetLength(1); x++)
            {
                if (x != BOARD_WIDTH - 1)
                {
                    Console.Write(_spaces[x, y] is null ? ", " : $"{_spaces[x, y]}, ");
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
        var x = random.Next(0, BOARD_WIDTH + 1);
        var y = random.Next(0, BOARD_HEIGHT + 1);

        _spaces[x,y] = null;
    }
    private void FillSpaces()
    {
        var random = new Random();

        for (int y = 0; y < _spaces.GetLength(0); y++)
        {
            for (int x = 0; x < _spaces.GetLength(1); x++)
            {
                int? number = null;
                while (number is null || IsNumberUsed(number))
                {
                    number = random.Next(1, BOARD_HEIGHT * BOARD_WIDTH + 1);
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