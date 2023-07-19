using System;

public delegate void ShowBox(int x, int y, int ball);

public class Lines
{
    public const int SIZE = 9;
    public const int BALLS = 7;
    Random rnd = new Random();
    int [,] map;
    ShowBox showBox;
    int fromX, fromY, toX, toY;
    bool BallSelected;

    public Lines(ShowBox showBox)
    {
        this.showBox = showBox;
        map = new int [SIZE,SIZE];
    }

    public void Start() 
    {
        ClearMap();
        FillBalls();
        while (CutLines())
        {

        }
    }

    public void Click (int x, int y)
    {
        if (!BallSelected)
            TakeBall(x,y);
        else
            SwapBall(x,y);
    }

    private void TakeBall(int x, int y)
    {
        fromX=x;
        fromY=y;
        BallSelected = true;
    }
    private void SwapBall (int x, int y)
    {
        if (!CanMove(x,y)) return;
        int temp;
        toX = x;
        toY = y;
        temp = map[toX,toY];
        SetMap(toX,toY,map[fromX,fromY]);
        SetMap(fromX,fromY,temp);
        BallSelected = false;
        if (!CutLines())
        {
            temp = map[fromX,fromY];
            SetMap(fromX,fromY,map[toX,toY]);
            SetMap(toX,toY,temp);
        }
        while (CutLines())
        {

        }

    }
    private void ClearMap()
    {
        for (int x = 0; x < SIZE; x++)
            for (int y = 0; y < SIZE; y++)
                SetMap(x,y,0);
    }

    private bool OnMap(int x, int y)
    {
        return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
    }

    private int GetMap(int x, int y)
    {
        if (!OnMap(x,y)) return 0;
        return map[x,y];
    }
    private void SetMap(int x, int y, int ball)
    {
        map[x,y]=ball;
        showBox(x,y,ball);
    }

    private void FillBalls()
    {
        int ball;
        for (int x = 0; x < SIZE; x++)
            for (int y = 0; y < SIZE; y++)
                if (map[x,y]==0)
                {
                    ball=rnd.Next(1,BALLS);
                    SetMap(x,y,ball);                 
                }
    }
    private bool[,] mark;
    private bool CutLines()
    {
        int balls = 0;
        mark = new bool[SIZE,SIZE];
        for (int x = 0; x < SIZE; x++)
            for (int y = 0; y < SIZE; y++)
            {
                balls += CutLine(x,y,1,0);
                balls += CutLine(x,y,0,1);
            }
        if (balls > 0)
            {
                for (int x = 0; x < SIZE; x++)
                    for (int y = 0; y < SIZE; y++)  
                        if (mark[x,y])
                            SetMap(x,y,0);
                ShiftDown();
                FillBalls();
                Game.gameScore+=balls;
                Game.timeStart=10;
                return true;
            }
        return false;
    }

    private int CutLine(int x0, int y0, int sx, int sy)
    {
        int ball = map[x0,y0];
        if (ball == 0) return 0;
        int count = 0;
        for (int x=x0, y=y0;GetMap(x,y) == ball;x += sx, y+= sy)
            count++;
        if (count < 3)
            return 0;
        for (int x=x0, y=y0;GetMap(x,y) == ball;x += sx, y+= sy)
            mark[x,y] = true;
        return count;
    }

    private bool CanMove (int x, int y)
    {
        if ((Math.Abs(fromX - x) < 2 && fromY - y == 0) || (Math.Abs(fromY - y) < 2 && fromX - x == 0))
            return true;
        else
        {
            BallSelected = false;
            return false;
        }
        
    }

    private void ShiftDown()
    {
        for (int i = 0; i < SIZE; i++)
            for (int y = 1; y < SIZE; y++)
                for (int x = 0; x < SIZE; x++)
                    if (map[x,y] == 0)
                        {
                            map[x,y]=map[x,y-1];
                            map[x,y-1]=0;
                            SetMap(x,y,map[x,y]);
                            SetMap(x,y-1,map[x,y-1]);
                        }               
    }
}
