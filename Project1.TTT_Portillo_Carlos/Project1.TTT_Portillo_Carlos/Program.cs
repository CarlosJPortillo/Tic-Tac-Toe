using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Project1TicTac
{
    class Program
    {
        struct CursorPos
        {
            public int row;
            public int col;
        }

        static void Main(string[] args)
        {
            //Declare Variables
            int menuselection = 0;
            string turn = "player1";
            string gameturn = "human";
            int timelimit = 3000;
            bool tie = false;
            bool win = false;
            string playagain = "Y";
            menuscreen();
            Console.WriteLine();
            int rows = 3;
            int cols = rows;

            string[,] board = new string[rows, cols];

            int[,] position = new int[rows, cols];
            int count = 0;

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            CursorPos cursorPos = new CursorPos();
            cursorPos.row = 0;
            cursorPos.col = 0;
            int xposition = cursorPos.col;
            int yposition = cursorPos.row;

            //loop to make sure selection input is entered and valid//
            while (menuselection != 6)
            {

                validinput(ref menuselection);
                while (menuselection < 1 || menuselection > 6)
                {
                    Console.WriteLine("Enter a valid selection");
                    while (!int.TryParse(Console.ReadLine(), out menuselection))
                    {
                        Console.WriteLine("Enter a valid selection");

                    }
                }

                //core of the game-------------------------------
                switch (menuselection)
                {
                    //enter number of rows & cols, default is already 3 to start
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Please enter the number of rows and columns you wish to play with");
                        validinput(ref rows);
                        while (rows < 2 || rows > 7)
                        {
                            Console.WriteLine("Enter a valid selection");
                            while (!int.TryParse(Console.ReadLine(), out rows))
                            {
                                Console.WriteLine("Enter a valid selection");

                            }
                        }
                        cols = rows;
                        break;
                    //enter time limit, default is already at 3000ms
                    case 2:
                        Console.WriteLine("Please enter a time limit you wish to have");
                        validinput(ref timelimit);
                        Console.Clear();

                        break;
                    //Play easy game 
                    case 3:
                        while (playagain == "Y" || playagain == "y")
                        {
                            Console.Clear();
                            createpositions(ref position, count, rows, cols);
                            draw(ref board, rows, cols);
                            win = false;
                            tie = false;
                            turn = "player1";
                            Console.SetCursorPosition(0, 0);
                            while (win == false & tie == false & turn != "exit")
                            {
                                //first player, computer player second if statement//
                                if (gameturn == "human")
                                {
                                    xposition = 0;
                                    yposition = 0;
                                    do
                                    {
                                        if (Console.KeyAvailable)
                                        {
                                            cki = Console.ReadKey(true);
                                            HumanMove(cki, board, position, ref cursorPos, rows, cols, ref xposition, ref yposition, ref turn,
                                            ref win);
                                        }
                                    } while (turn == "player1");
                                    if (turn != "exit")
                                    {
                                        turn = "player2";
                                        tie = checktie(board, rows, cols);
                                        commencegame(win, tie, ref turn);
                                    }

                                    while (turn == "player2")
                                    {

                                        ComputerMove(ref board, position, rows, cols, turn, ref cursorPos);
                                        win = checkwin(board, cols, rows, win, turn);
                                        tie = checktie(board, rows, cols);
                                        turn = "player1";
                                        commencegame(win, tie, ref turn);

                                    }
                                    //computer play ends turns
                                    displayresults(turn, rows, cols);

                                    //end human player first, computer player second if
                                }
                                //computer player first, human player second if
                                if (gameturn == "computer")
                                {

                                    //computer player turn loop
                                    while (turn == "player1")
                                    {
                                        ComputerMove(ref board, position, rows, cols, turn, ref cursorPos);
                                        win = checkwin(board, cols, rows, win, turn);
                                        tie = checktie(board, rows, cols);
                                        turn = "player2";
                                        commencegame(win, tie, ref turn);

                                        //end computer player turn loop
                                    }
                                    xposition = 0;
                                    yposition = 0;
                                    do
                                    {
                                        if (Console.KeyAvailable)
                                        {
                                            cki = Console.ReadKey(true);
                                            HumanMove(cki, board, position, ref cursorPos, rows, cols, ref xposition, ref yposition, ref turn,
                                            ref win);
                                        }
                                    } while (turn == "player2");
                                    if (turn != "exit")
                                    {
                                        turn = "player1";
                                        tie = checktie(board, rows, cols);
                                        commencegame(win, tie, ref turn);
                                    }
                                    //computer player first, human player second "end if"//
                                }
                                displayresults(turn, rows, cols);
                            }
                            if (turn != "exit")
                            {
                                Console.WriteLine("Do you wish to continue press y");
                                playagain = Console.ReadLine();

                            }
                            SwitchPlayerTurns(ref gameturn);
                            if (turn == "exit")
                            {
                                playagain = "oopsorry";
                            }
                        }
                        Console.Clear();
                        break;
                    //Less Easy Mode Game-----------------------------------------//
                    case 4:
                        while (playagain == "Y" || playagain == "y")
                        {
                            Console.Clear();
                            createpositions(ref position, count, rows, cols);
                            draw(ref board, rows, cols);
                            win = false;
                            tie = false;
                            turn = "player1";
                            Console.SetCursorPosition(0, 0);
                            while (win == false & tie == false & turn != "exit")
                            {
                                //first player, computer player second if statement//
                                if (gameturn == "human")
                                {
                                    xposition = 0;
                                    yposition = 0;

                                    do
                                    {

                                        if (Console.KeyAvailable)
                                        {
                                            cki = Console.ReadKey(true);
                                            HumanMove(cki, board, position, ref cursorPos, rows, cols, ref xposition, ref yposition, ref turn,
                                            ref win);
                                        }
                                    } while (turn == "player1");
                                    if (turn != "exit")
                                    {
                                        turn = "player2";
                                        tie = checktie(board, rows, cols);
                                        commencegame(win, tie, ref turn);
                                    }

                                    while (turn == "player2")
                                    {

                                        PossibleWin(ref board, cols, rows, ref turn);
                                        if (turn != "Move_has_ended")
                                        {
                                            PossibleBlock(ref board, cols, rows, ref turn);
                                        }
                                        if (turn == "Move_has_ended")
                                        {
                                            Console.Clear();
                                            redraw(board, rows, cols);
                                            cursorPos.row = 0;
                                            cursorPos.col = 0;
                                            Console.SetCursorPosition(cursorPos.row, cursorPos.col);
                                        }
                                        if (turn == "player2")
                                        {
                                            ComputerMove(ref board, position, rows, cols, turn, ref cursorPos);
                                        }
                                        turn = "player2";
                                        win = checkwin(board, cols, rows, win, turn);
                                        tie = checktie(board, rows, cols);
                                        turn = "player1";
                                        commencegame(win, tie, ref turn);

                                    }
                                    //computer play ends turns
                                    displayresults(turn, rows, cols);

                                    //end human player first, computer player second if
                                }
                                //computer player first, human player second if
                                if (gameturn == "computer")
                                {

                                    //computer player turn loop
                                    while (turn == "player1")
                                    {
                                        PossibleWin(ref board, cols, rows, ref turn);
                                        if (turn != "Move_has_ended")
                                        {
                                            PossibleBlock(ref board, cols, rows, ref turn);
                                        }
                                        if (turn == "Move_has_ended")
                                        {
                                            Console.Clear();
                                            redraw(board, rows, cols);
                                            cursorPos.row = 0;
                                            cursorPos.col = 0;
                                            Console.SetCursorPosition(cursorPos.row, cursorPos.col);
                                        }
                                        if (turn == "player1")
                                        {
                                            ComputerMove(ref board, position, rows, cols, turn, ref cursorPos);
                                        }
                                        win = checkwin(board, cols, rows, win, turn);
                                        tie = checktie(board, rows, cols);
                                        turn = "player2";
                                        commencegame(win, tie, ref turn);

                                        //end computer player turn loop
                                    }
                                    xposition = 0;
                                    yposition = 0;
                                    do
                                    {
                                        if (Console.KeyAvailable)
                                        {
                                            cki = Console.ReadKey(true);
                                            HumanMove(cki, board, position, ref cursorPos, rows, cols, ref xposition, ref yposition, ref turn,
                                            ref win);
                                        }
                                    } while (turn == "player2");
                                    if (turn != "exit")
                                    {
                                        turn = "player1";
                                        tie = checktie(board, rows, cols);
                                        commencegame(win, tie, ref turn);
                                    }
                                    //computer player first, human player second "end if"//
                                }
                                displayresults(turn, rows, cols);


                            }
                            if (turn != "exit")
                            {
                                Console.WriteLine("Do you wish to continue press y");
                                playagain = Console.ReadLine();

                            }
                            SwitchPlayerTurns(ref gameturn);
                            if (turn == "exit")
                            {
                                playagain = "oopsorry";
                            }
                        }
                        Console.Clear();
                        break;
                    //Hard Mode Game-----------------------------------------//
                    case 5:
                        while (playagain == "Y" || playagain == "y")
                        {

                            Console.Clear();
                            createpositions(ref position, count, rows, cols);
                            draw(ref board, rows, cols);
                            win = false;
                            tie = false;
                            turn = "player1";
                            Stopwatch sw = new Stopwatch();
                            Console.SetCursorPosition(0, 0);
                            while (win == false & tie == false & turn != "exit")
                            {
                                //first player, computer player second if statement//
                                if (gameturn == "human")
                                {
                                    xposition = 0;
                                    yposition = 0;
                                    //Timer and Sleep for a few miliseconds to show timer starting at 3 before countdown//
                                    timer(sw, timelimit, rows);
                                    Thread.Sleep(300);
                                    sw.Start();
                                    do
                                    {
                                        //
                                        timer(sw, timelimit, rows);

                                        if (Console.KeyAvailable)
                                        {

                                            cki = Console.ReadKey(true);
                                            HumanMove(cki, board, position, ref cursorPos, rows, cols, ref xposition, ref yposition, ref turn,
                                            ref win);

                                        }
                                    } while (turn == "player1" & timelimit - sw.ElapsedMilliseconds > 0);
                                    if (turn != "exit")
                                    {
                                        sw.Reset();
                                        turn = "player2";
                                        tie = checktie(board, rows, cols);
                                        commencegame(win, tie, ref turn);
                                    }

                                    while (turn == "player2")
                                    {

                                        PossibleWin(ref board, cols, rows, ref turn);
                                        if (turn != "Move_has_ended")
                                        {
                                            PossibleBlock(ref board, cols, rows, ref turn);
                                        }
                                        if (turn == "Move_has_ended")
                                        {
                                            Console.Clear();
                                            redraw(board, rows, cols);
                                            cursorPos.row = 0;
                                            cursorPos.col = 0;
                                            Console.SetCursorPosition(cursorPos.row, cursorPos.col);
                                        }
                                        if (turn == "player2")
                                        {
                                            ComputerMove(ref board, position, rows, cols, turn, ref cursorPos);
                                        }
                                        turn = "player2";
                                        win = checkwin(board, cols, rows, win, turn);
                                        tie = checktie(board, rows, cols);
                                        turn = "player1";
                                        commencegame(win, tie, ref turn);

                                    }
                                    //computer play ends turns
                                    displayresults(turn, rows, cols);

                                    //end human player first, computer player second if
                                }
                                //computer player first, human player second if
                                if (gameturn == "computer")
                                {

                                    //computer player turn loop
                                    while (turn == "player1")
                                    {
                                        PossibleWin(ref board, cols, rows, ref turn);
                                        if (turn != "Move_has_ended")
                                        {
                                            PossibleBlock(ref board, cols, rows, ref turn);
                                        }
                                        if (turn == "Move_has_ended")
                                        {
                                            Console.Clear();
                                            redraw(board, rows, cols);
                                            cursorPos.row = 0;
                                            cursorPos.col = 0;
                                            Console.SetCursorPosition(cursorPos.row, cursorPos.col);
                                        }
                                        if (turn == "player1")
                                        {
                                            ComputerMove(ref board, position, rows, cols, turn, ref cursorPos);
                                        }
                                        win = checkwin(board, cols, rows, win, turn);
                                        tie = checktie(board, rows, cols);
                                        turn = "player2";
                                        commencegame(win, tie, ref turn);

                                        //end computer player turn loop
                                    }
                                    xposition = 0;
                                    yposition = 0;
                                    //Timer and Sleep for a few miliseconds to show timer starting at 3 before countdown//
                                    timer(sw, timelimit, rows);
                                    Thread.Sleep(300);
                                    sw.Start();
                                    do
                                    {
                                        timer(sw, timelimit, rows);
                                        if (Console.KeyAvailable)
                                        {
                                            cki = Console.ReadKey(true);
                                            HumanMove(cki, board, position, ref cursorPos, rows, cols, ref xposition, ref yposition, ref turn,
                                            ref win);
                                        }
                                    } while (turn == "player2" & timelimit - sw.ElapsedMilliseconds > 0);
                                    if (turn != "exit")
                                    {
                                        sw.Reset();
                                        turn = "player1";
                                        tie = checktie(board, rows, cols);
                                        commencegame(win, tie, ref turn);
                                    }
                                    //computer player first, human player second "end if"//
                                }
                                displayresults(turn, rows, cols);


                            }
                            if (turn != "exit")
                            {
                                Console.WriteLine("Do you wish to continue press y");
                                playagain = Console.ReadLine();

                            }
                            SwitchPlayerTurns(ref gameturn);
                            if (turn == "exit")
                            {
                                playagain = "oopsorry";
                            }
                        }
                        Console.Clear();
                        break;
                    case 6:
                        turn = "Goodbye";
                        break;

                }
                if (turn != "Goodbye")
                {
                    menuscreen();
                    playagain = "y";
                }
            }
        }

        static void menuscreen()
        {
            Console.WriteLine("1 - Enter board size(default 3x3)");
            Console.WriteLine();
            Console.WriteLine("2 - Time to move(hard mode, default 3000 ms");
            Console.WriteLine();
            Console.WriteLine("3 - Play Easy mode games(press escape to end playing and return to menu");
            Console.WriteLine();
            Console.WriteLine("4 - Play Less Easy mode games(press escape to end playing and return to menu");
            Console.WriteLine();
            Console.WriteLine("5 - Play Hard Mode mode games(press escape to end playing and return to menu");
            Console.WriteLine();
            Console.WriteLine("6 - Exit");
        }
        static void draw(ref string[,] board, int rows, int cols)
        {
            board = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j] = "*";
                    Console.Write(board[i, j] + "||");
                }
                Console.WriteLine();
                for (int z = 0; z < rows; z++)
                {
                    Console.Write("---");
                }

                Console.WriteLine();
            }
        }
        static void redraw(string[,] board, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(board[i, j] + "||");
                }
                Console.WriteLine();
                for (int z = 0; z < rows; z++)
                {
                    Console.Write("---");
                }

                Console.WriteLine();
            }
            //end method-------------
        }
        static void createpositions(ref int[,] position, int count, int rows, int cols)
        {
            position = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {

                for (int y = 0; y < cols; y
                    ++)
                {
                    count++;
                    position[i, y] = count;
                }

            }
            // end method ---------------
        }

        static int validinput(ref int variable)
        {
            while (!int.TryParse(Console.ReadLine(), out variable))
            {
                Console.WriteLine("Enter a valid number");
            }
            return variable;
        }
        //end method
        static void HumanMove(ConsoleKeyInfo cki, string[,] board, int[,] position, ref CursorPos cursorPos,
                              int rows, int cols, ref int xposition, ref int yposition, ref string turn, ref bool win)
        {
            string loopend = "111";


            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow:

                    cursorPos.col = cursorPos.col - 3;
                    xposition--;
                    if (cursorPos.col < 0)
                    {
                        cursorPos.col = cols * 3 - 3;
                    }
                    if (xposition < 0)
                    {
                        xposition = cols - 1;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    cursorPos.col = cursorPos.col + 3;
                    xposition++;
                    if (cursorPos.col > cols * 3 - 3)
                    {
                        cursorPos.col = 0;
                    }
                    if (xposition > cols - 1)
                    {
                        xposition = 0;
                    }

                    break;
                case ConsoleKey.DownArrow:


                    cursorPos.row = cursorPos.row + 2;
                    yposition++;
                    if (cursorPos.row > rows * 2 - 2)
                    {
                        cursorPos.row = 0;
                    }
                    if (yposition > rows - 1)
                    {
                        yposition = 0;
                    }

                    break;
                case ConsoleKey.UpArrow:

                    cursorPos.row = cursorPos.row - 2;
                    yposition--;
                    if (cursorPos.row < 0)
                    {
                        cursorPos.row = rows * 2 - 2;
                    }
                    if (yposition < 0)
                    {
                        yposition = rows - 1;
                    }


                    break;
                case ConsoleKey.Spacebar:
                    Console.Clear();
                    InputSelection(ref board, position, xposition, yposition, rows, cols, turn, ref loopend);
                    redraw(board, rows, cols);
                    win = checkwin(board, cols, rows, win, turn);
                    if (turn == "player1")
                    {
                        turn = "player2";
                    }
                    else if (turn == "player2")
                    {
                        turn = "player1";
                    }

                    break;
                case ConsoleKey.Escape:
                    turn = "exit";
                    break;
            }
            Console.SetCursorPosition(cursorPos.col, cursorPos.row);
        }
        //end method

        static void ComputerMove(ref string[,] board, int[,] position, int rows, int cols, string turn, ref CursorPos cursorPos)
        {
            string loopend = "111";
            Random rnd = new Random();

            int xposition = rnd.Next(0, rows);
            int yposition = rnd.Next(0, cols);
            while (loopend == "111")
            {
                InputSelection(ref board, position, xposition, yposition, rows, cols, turn, ref loopend);
                xposition = rnd.Next(0, rows);
                yposition = rnd.Next(0, cols);
            }
            Console.Clear();
            redraw(board, rows, cols);
            cursorPos.row = 0;
            cursorPos.col = 0;
            Console.SetCursorPosition(cursorPos.row, cursorPos.col);
        }
        //Method to insert selection and change content of board according to move//
        static void InputSelection(ref string[,] board, int[,] position, int xposition, int yposition, int ROWS, int COLS,
                                 string turn, ref string loopend)
        {
            int variable = position[yposition, xposition];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (variable == position[i, j])
                    {
                        if (board[i, j] == "*")
                        {
                            if (turn == "player1")
                            {

                                board[i, j] = "x";
                                loopend = "222";
                            }
                            else if (turn == "player2")
                            {
                                board[i, j] = "o";
                                loopend = "222";
                            }
                        }
                    }
                }
            }
        }
        //end method//
        //method to check to see if either player has won---//
        static bool checkwin(string[,] board, int COLS, int ROWS, bool win, string turn)
        {
            int i = 0;
            int j = 0;
            int wincounter;
            string playercharacter = "x";
            if (turn == "player2")
            {
                playercharacter = "o";
            }

            for (i = 0; i < ROWS; i++)
            {
                wincounter = 0;
                for (j = 0; j < COLS; j++)
                {
                    if (board[i, j] == playercharacter)
                    {
                        wincounter++;

                    }

                }
                if (wincounter == COLS)
                {
                    win = true;
                }

            }
            for (j = 0; j < COLS; j++)
            {
                wincounter = 0;
                for (i = 0; i < ROWS; i++)
                {
                    if (board[i, j] == playercharacter)
                    {
                        wincounter++;

                    }

                }
                if (wincounter == ROWS)
                {
                    win = true;
                }

            }
            wincounter = 0;
            j = 0;
            for (i = 0; i < ROWS; i++)
            {
                if (board[i, j] == playercharacter)
                {
                    wincounter++;
                }
                j++;
                if (wincounter == ROWS)
                {
                    win = true;
                }
            }
            wincounter = 0;
            j = 0;
            for (i = ROWS - 1; i >= 0; i--)
            {
                if (board[i, j] == playercharacter)
                {
                    wincounter++;
                }
                j++;
                if (wincounter == ROWS)
                {
                    win = true;
                }
            }


            return win;
        }
        static bool checktie(string[,] board, int rows, int cols)
        {
            bool flag = true;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j] == "*")
                    {
                        flag = false;
                    }

                }

            }
            return flag;
        }
        //method to see if game should continue changing between turns of player or jump out of loop in main
        static void commencegame(bool win, bool tie, ref string turn)
        {
            if (win == true)
            {
                if (turn == "player1")
                {
                    turn = "XXXX";
                }
                else if (turn == "player2")
                {
                    turn = "ZZZZ";
                }
            }
            else if (tie == true)
            {
                turn = "No_more_turns";
            }
        }
        //displays whether player 1 or player 2 has won or if game ends in draw---//
        static void displayresults(string turn, int rows, int cols)
        {
            if (turn == "XXXX")
            {
                Console.SetCursorPosition((rows * 2), (rows * 2));
                Console.WriteLine("Congraluations player 2 wins");
            }
            else if (turn == "ZZZZ")
            {
                Console.SetCursorPosition((rows * 2), (rows * 2));
                Console.WriteLine("Congraluations player 1 wins");
            }
            if (turn == "No_more_turns")
            {
                Console.SetCursorPosition((rows * 2), (rows * 2));
                Console.WriteLine("Its a draw");
            }
        }
        //method to change player
        static void SwitchPlayerTurns(ref string gameturn)
        {
            if (gameturn == "human")
            {
                gameturn = "computer";
            }
            else if (gameturn == "computer")
            {
                gameturn = "human";
            }
        }

        static void PossibleWin(ref string[,] board, int COLS, int ROWS, ref string turn)
        {
            int i = 0;
            int j = 0;
            int wincounter = 0;
            string playercharacter = "x";
            if (turn == "player2")
            {
                playercharacter = "o";
            }
            //horizontal----------win
            for (i = 0; i < ROWS; i++)
            {
                wincounter = 0;
                for (j = 0; j < COLS; j++)
                {
                    if (board[i, j] == playercharacter)
                    {
                        wincounter++;
                        if (wincounter == COLS - 1)
                        {
                            for (j = 0; j < COLS; j++)
                            {
                                if (board[i, j] == "*")
                                {
                                    board[i, j] = playercharacter;
                                    turn = "Move_has_ended";
                                    j = COLS;
                                    i = ROWS;
                                }
                            }
                        }
                    }
                }
            }
            //vertical win-------------------------
            if (turn != "Move_has_ended")
            {
                for (j = 0; j < COLS; j++)
                {
                    wincounter = 0;
                    for (i = 0; i < ROWS; i++)
                    {
                        if (board[i, j] == playercharacter)
                        {
                            wincounter++;
                        }

                        if (wincounter == ROWS - 1)
                        {
                            for (i = 0; i < ROWS; i++)
                            {
                                if (board[i, j] == "*")
                                {
                                    board[i, j] = playercharacter;
                                    turn = "Move_has_ended";
                                    i = ROWS;
                                    j = COLS;
                                }

                            }

                        }
                    }
                }
            }
            //diagonol win
            if (turn != "Move_has_ended")
            {
                wincounter = 0;
                j = 0;
                for (i = 0; i < ROWS; i++)
                {
                    if (board[i, j] == playercharacter)
                    {
                        wincounter++;
                    }
                    if (wincounter == ROWS - 1)
                    {
                        j = 0;
                        for (i = 0; i < ROWS; i++)
                        {
                            if (board[i, j] == "*")
                            {
                                board[i, j] = playercharacter;
                                turn = "Move_has_ended";
                                i = ROWS;
                                j = COLS;
                            }
                            j++;
                        }
                        //end if//
                    }
                    j++;
                    //end for
                }
                //end if//
            }
            //diagnol win 2
            if (turn != "Move_has_ended")
            {
                wincounter = 0;
                j = 0;
                for (i = ROWS - 1; i >= 0; i--)
                {
                    if (board[i, j] == playercharacter)
                    {
                        wincounter++;
                    }
                    if (wincounter == ROWS - 1)
                    {
                        j = 0;
                        for (i = ROWS - 1; i >= 0; i--)
                        {
                            if (board[i, j] == "*")
                            {
                                board[i, j] = playercharacter;
                                turn = "Move_has_ended";
                                i = 0;
                            }
                            j++;
                            //end for
                        }
                        //end if
                    }
                    j++;
                    //end for
                }
                // end if
            }
            //end method
        }
        //Method to see possible block---
        static void PossibleBlock(ref string[,] board, int COLS, int ROWS, ref string turn)
        {
            int i = 0;
            int j = 0;
            int wincounter = 0;
            string playercharacter = "o";
            string opposingcharacter = "x";
            if (turn == "player1")
            {
                playercharacter = "x";
                opposingcharacter = "o";
            }
            //horizontal----------win
            for (i = 0; i < ROWS; i++)
            {
                wincounter = 0;
                for (j = 0; j < COLS; j++)
                {
                    if (board[i, j] == opposingcharacter)
                    {
                        wincounter++;
                        if (wincounter == COLS - 1)
                        {
                            for (j = 0; j < COLS; j++)
                            {
                                if (board[i, j] == "*")
                                {
                                    board[i, j] = playercharacter;
                                    turn = "Move_has_ended";
                                    j = COLS;
                                    i = ROWS;
                                }
                            }
                        }
                    }
                }
            }
            //vertical win-------------------------
            if (turn != "Move_has_ended")
            {
                for (j = 0; j < COLS; j++)
                {
                    wincounter = 0;
                    for (i = 0; i < ROWS; i++)
                    {
                        if (board[i, j] == opposingcharacter)
                        {
                            wincounter++;
                        }

                        if (wincounter == ROWS - 1)
                        {
                            for (i = 0; i < ROWS; i++)
                            {
                                if (board[i, j] == "*")
                                {
                                    board[i, j] = playercharacter;
                                    turn = "Move_has_ended";
                                    i = ROWS;
                                    j = COLS;
                                }

                            }

                        }
                    }
                }
            }
            //diagnol----------------win
            if (turn != "Move_has_ended")
            {
                wincounter = 0;
                j = 0;
                for (i = 0; i < ROWS; i++)
                {
                    if (board[i, j] == opposingcharacter)
                    {
                        wincounter++;
                    }
                    if (wincounter == ROWS - 1)
                    {
                        j = 0;
                        for (i = 0; i < ROWS; i++)
                        {
                            if (board[i, j] == "*")
                            {
                                board[i, j] = playercharacter;
                                turn = "Move_has_ended";
                                i = ROWS;
                                j = COLS;
                            }
                            j++;
                            //end for
                        }
                        //end if//
                    }
                    j++;
                    //end for
                }
                //end if//
            }
            //diagnol win 2
            if (turn != "Move_has_ended")
            {
                wincounter = 0;
                j = 0;
                for (i = ROWS - 1; i >= 0; i--)
                {
                    if (board[i, j] == opposingcharacter)
                    {
                        wincounter++;
                    }
                    if (wincounter == ROWS - 1)
                    {
                        j = 0;
                        for (i = ROWS - 1; i >= 0; i--)
                        {
                            if (board[i, j] == "*")
                            {
                                board[i, j] = playercharacter;
                                turn = "Move_has_ended";
                                i = 0;
                            }
                            j++;
                            //end for
                        }
                        //end if
                    }
                    j++;
                    //end for
                }
                // end if
            }
            //end method
        }
        //method to dispaly timer//
        static void timer(Stopwatch SW, int timelimit, int rows)
        {

            Thread.Sleep(100);
            Console.SetCursorPosition((1), (rows * 2));
            Console.WriteLine("Seconds remainding: {0:f1} ", (timelimit - SW.ElapsedMilliseconds) / (1000));
        }

    }
}