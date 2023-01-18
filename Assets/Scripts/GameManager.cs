using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Square[,] squares;
    public GameObject squarePrefab;
    public int rows, columns, mines;
    public bool gameOver;

    void Start()
    {
        // Instantiate the squares array and populate it with square objects
        squares = new Square[rows, columns];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                squares[x, y] = Instantiate(squarePrefab, new Vector3(x * 1.25f, 0, y * 1.25f), Quaternion.identity).GetComponent<Square>();
                squares[x, y].x = x;
                squares[x, y].y = y;
            }
        }

        // Place mines randomly on the board
        int minesPlaced = 0;
        while (minesPlaced < mines)
        {
            int randomX = Random.Range(0, rows);
            int randomY = Random.Range(0, columns);
            if (!squares[randomX, randomY].isMine)
            {
                squares[randomX, randomY].isMine = true;
                minesPlaced++;
            }
        }

        // Calculate the number of neighboring mines for each square
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (!squares[x, y].isMine)
                {
                    squares[x, y].neighboringMines = countNeighboringMines(x, y);
                }
            }
        }
    }

    void Update()
    {
        if (gameOver)
        {
            // Handle end of game
        }
    }

    public void revealSquare(int x, int y)
    {
        if (!gameOver)
        {
            if (!squares[x, y].isRevealed && !squares[x, y].isFlagged)
            {
                squares[x, y].reveal();

                if (squares[x, y].isMine)
                {
                    gameOver = true;
                    revealAllMines();
                }
                else if (squares[x, y].neighboringMines == 0)
                {
                    revealAdjacentSquares(x, y);
                }
            }
        }
    }

    public void flagSquare(int x, int y)
    {
        if (!gameOver)
        {
            squares[x, y].flag();
        }
    }

    private int countNeighboringMines(int x, int y)
    {
        int mineCount = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int neighborX = x + i;
                int neighborY = y + j;
                if (neighborX >= 0 && neighborX < rows && neighborY >= 0 && neighborY < columns)
                {
                    if (squares[neighborX, neighborY].isMine)
                    {
                        mineCount++;
                    }
                }
            }
        }
        return mineCount;
    }

    private void revealAdjacentSquares(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int neighborX = x + i;
                int neighborY = y + j;
                if (neighborX >= 0 && neighborX < rows && neighborY >= 0 && neighborY < columns)
                {
                    revealSquare(neighborX, neighborY);
                }
            }
        }
    }

    private void revealAllMines()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (squares[x, y].isMine)
                {
                    squares[x, y].reveal();
                }
            }
        }
    }
}
