using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static string gameBoard = @"
    1   2   3

A   $ | $ | $ 
    -----------
B   $ | $ | $ 
    -----------
C   $ | $ | $ 

";
        static char[] moves;

        static void Main(string[] args)
        {
            moves = new char[9];

            for(int i = 0; i < 9; i++)
            {
                moves[i] = ' ';
            }

            updateScreen();
            while (gameLoop());

            System.Console.In.ReadLine();
        }

        static bool gameLoop()
        {
            System.Console.Write("Enter Coordinates: ");
            if (getPlayerMove() == true)
            {
                if (getGameContinues() == true)
                {
                    getComputerMove();
                }

                return getGameContinues();
            }
            else
            {
                return true;
            }

            
        }

        static void getComputerMove()
        {
            Hashtable lanes = getLanes();
            if (win(lanes) == false)
            {
                if (defend(lanes) == false)
                {
                    if(streak(lanes) == false) 
                    {
                        if (moveToMiddle() == false)
                        {
                            if (moveToOpenLane(lanes) == false)
                            {
                                goAnyWhere();
                            }
                        }
                    }
                }
            }
        }

        static bool moveToMiddle()
        {
            bool moved = false;

            if(moves[4] == ' ')
            {
                moves[4] = 'O';
                moved = true;
            }

            return moved;
        }

        static bool moveToOpenLane(Hashtable lanes)
        {
            bool moved = false;

            foreach (DictionaryEntry lane in getLanes())
            {
                if (moved == false)
                {
                    int spaceCount = 0;

                    foreach (char square in ((string)lane.Value).ToCharArray())
                    {

                        if (square == ' ')
                        {
                            spaceCount++;
                        }
                    }

                    if (spaceCount == 3)
                    {
                        string laneValue = (string)lane.Value;
                        string laneKey = (string)lane.Key;
                        int spaceIndex = laneValue.IndexOf(' ');
                        char[] coordinate = new char[1];
                        coordinate[0] = laneKey[spaceIndex];

                        int moveIndex = int.Parse(new string(coordinate));
                        moves[moveIndex] = 'O';
                        moved = true;
                    }
                }

            }

            return moved;
        }

        static bool win(Hashtable lanes)
        {
            bool moved = false;

            foreach (DictionaryEntry lane in getLanes())
            {
                if (moved == false)
                {
                    int oCount = 0;
                    int spaceCount = 0;

                    foreach (char square in ((string)lane.Value).ToCharArray())
                    {
                        if (square == 'O')
                        {
                            oCount++;
                        }

                        if (square == ' ')
                        {
                            spaceCount++;
                        }
                    }

                    if (oCount == 2 && spaceCount == 1)
                    {
                        string laneValue = (string)lane.Value;
                        string laneKey = (string)lane.Key;
                        int spaceIndex = laneValue.IndexOf(' ');
                        char[] coordinate = new char[1];
                        coordinate[0] = laneKey[spaceIndex];

                        int moveIndex = int.Parse(new string(coordinate));
                        moves[moveIndex] = 'O';
                        moved = true;
                    }
                }
                
            }

            return moved;
        }

        static bool defend(Hashtable lanes)
        {
            bool moved = false;

            foreach (DictionaryEntry lane in getLanes())
            {
                if (moved == false)
                {
                    int xCount = 0;
                    int spaceCount = 0;

                    foreach (char square in ((string)lane.Value).ToCharArray())
                    {
                        if (square == 'X')
                        {
                            xCount++;
                        }

                        if (square == ' ')
                        {
                            spaceCount++;
                        }
                    }

                    if (xCount == 2 && spaceCount == 1)
                    {
                        string laneValue = (string)lane.Value;
                        string laneKey = (string)lane.Key;
                        int spaceIndex = laneValue.IndexOf(' ');
                        char[] coordinate = new char[1];
                        coordinate[0] = laneKey[spaceIndex];

                        int moveIndex = int.Parse(new string(coordinate));
                        moves[moveIndex] = 'O';
                        moved = true;
                    }
                }

            }

            return moved;
        }

        static bool streak(Hashtable lanes)
        {
            bool moved = false;

            foreach (DictionaryEntry lane in getLanes())
            {
                if (moved == false)
                {
                    int oCount = 0;
                    int spaceCount = 0;

                    foreach (char square in ((string)lane.Value).ToCharArray())
                    {
                        if (square == 'O')
                        {
                            oCount++;
                        }

                        if (square == ' ')
                        {
                            spaceCount++;
                        }
                    }

                    if (oCount == 1 && spaceCount == 2)
                    {
                        string laneValue = (string)lane.Value;
                        string laneKey = (string)lane.Key;
                        int spaceIndex = laneValue.IndexOf(' ');
                        char[] coordinate = new char[1];
                        coordinate[0] = laneKey[spaceIndex];

                        int moveIndex = int.Parse(new string(coordinate));
                        moves[moveIndex] = 'O';
                        moved = true;
                    }
                }

            }

            return moved;
        }

        static void goAnyWhere()
        {
            for(int i = 0; i < 9; i++)
            {
                if(moves[i] == ' ')
                {
                    moves[i] = 'O';
                }
            }
        }

        static bool getGameContinues()
        {
            bool continues = true;
            updateScreen();

            foreach(string lane in getLanes().Values)
            {
                if (lane == "XXX")
                {
                    System.Console.Write("Winner: X!");
                    return false;
                }

                if (lane == "OOO")
                {
                    System.Console.Write("Winner: O!");
                    return false;
                }
            }

            bool hasEmptySpace = false;
            foreach(char move in moves)
            {
                if(move == ' ')
                {
                    hasEmptySpace = true;
                }
            }

            if (hasEmptySpace == false)
            {
                System.Console.Write("Draw");
                return false;
            }

            return continues;
        }

        static Hashtable getLanes()
        {
            Hashtable lanes = new Hashtable() ;

            lanes.Add("012", combineChars(moves[0], moves[1], moves[2]));
            lanes.Add("345", combineChars(moves[3], moves[4], moves[5]));
            lanes.Add("678", combineChars(moves[6], moves[7], moves[8]));
            lanes.Add("036", combineChars(moves[0], moves[3], moves[6]));
            lanes.Add("147", combineChars(moves[1], moves[4], moves[7]));
            lanes.Add("258", combineChars(moves[2], moves[5], moves[8]));
            lanes.Add("048", combineChars(moves[0], moves[4], moves[8]));
            lanes.Add("642", combineChars(moves[6], moves[4], moves[2]));

            return lanes;
        }

        static string combineChars(char char1, char char2, char char3)
        {
            char[] combined = new char[3];
            combined[0] = char1;
            combined[1] = char2;
            combined[2] = char3;


            return new string(combined);
        }

        static void updateScreen()
        {
            System.Console.Clear();
            int currMoveIndex = 0;

            for (int i = 0; i < gameBoard.Length; i++)
            {
                if (gameBoard[i] != '$')
                {
                    System.Console.Write(gameBoard[i]);
                }
                else
                {
                    System.Console.Write(moves[currMoveIndex]);
                    currMoveIndex++;
                }
            }
        }

        static bool getPlayerMove()
        {
            bool moved = false;

            string line = System.Console.ReadLine();
            int index = -1;

            if (line.Length == 2)
            {
                index = getMoveIndex(line.Trim().ToUpper());

                if(index != -1)
                {
                    if(moves[index] == ' ')
                    {
                        moves[index] = 'X';
                        moved = true;
                    }
                    else
                    {
                        updateScreen();
                        System.Console.WriteLine("Cannot move there");
                    }
                    
                }
                else
                {
                    updateScreen();
                    System.Console.WriteLine("Invalid Input");
                }

            }
            else
            {
                updateScreen();
                System.Console.WriteLine("Invalid Input");
            }

            return moved;
        }

        static int getMoveIndex(string coordinate)
        {
            int index = -1;

            char verticalCoordinate = coordinate[0];
            char horizontalCoordinate = coordinate[1];

            switch(verticalCoordinate)
            {
                case 'A':
                    index = 0;
                    break;
                case 'B':
                    index = 3;
                    break;
                case 'C':
                    index = 6;
                    break;
                default:
                    break;
            }

            switch(horizontalCoordinate)
            {
                case '2':
                    index += 1;
                    break;
                case '3':
                    index += 2;
                    break;
                default:
                    break;
            }

            return index;
        }
    }
}
