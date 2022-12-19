using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacAndToes
{
    public class TicTacToe
    {
        public bool?[,] Table { get; protected set; }
        public string PlayerXName { get; protected set; }
        public string PlayerOName { get; protected set; }
        int Size => Table.GetLength(0);
        public TicTacToe(int size, string playerXName, string playerOName)
        {
            Table = new bool?[size, size];
            if (size < 3)
                Console.WriteLine("Min size is 3!");
            else if (size > 34)
                Console.WriteLine("Max size is 34! (but please don't play it, 1156 turn is kinda too much)");
            else if (size == 3)
                ClassicGame();
            else
                WeirdGame();
            PlayerXName = playerXName;
            PlayerOName = playerOName;
        }
        public void Info()
        {
            Console.Clear();
            var (xScore, oScore) = GetFinalScore();
            DrawTable();
            Console.WriteLine($"Scores:\nX - {xScore}\nO - {oScore}\n");
        }
        void ClassicGame()
        {
            (bool xWon, bool oWon, bool allTaken) result;
            while (true)
            {
                Console.Clear();
                DrawTable();
                Console.WriteLine("X's turn:");
                ChoosePlacement(true);
                result = ClassicVictoryCheck();
                if (result.oWon || result.xWon || result.allTaken) break;
                Console.Clear();
                DrawTable();
                Console.WriteLine("O's turn:");
                ChoosePlacement(false);
                result = ClassicVictoryCheck();
                if (result.oWon || result.xWon || result.allTaken) break;
            }
            Console.Clear();
            DrawTable();
            if (result.xWon)
            {
                Console.WriteLine("Let's congratulate to warrior X for great match!");
            }
            else if (result.oWon)
            {
                Console.WriteLine("Let's congratulate to warrior O for great match!");
            }
            else
            {
                Console.WriteLine("Seems like it's a draw... What else to expect from two powerful player!");
            }
        }
        void WeirdGame()//while getting contiguous chars you get (n - 2) score where n represents length of line
        {
            int turnCount = 0;
            while (true)
            {
                Console.Clear();
                DrawTable();
                Console.WriteLine("X's turn:");
                ChoosePlacement(true);
                turnCount++;
                if (turnCount == Size * Size) break;
                Console.Clear();
                DrawTable();
                Console.WriteLine("O's turn:");
                ChoosePlacement(false);
                turnCount++;
                if (turnCount == Size * Size) break;
            }
            Console.Clear();
            var (xScore, oScore) = GetFinalScore();
            DrawTable();
            Console.WriteLine($"Scores:\nX - {xScore}\nO - {oScore}\n");
        }
        void ChoosePlacement(bool user)
        {
            bool takingChoice = true;
            while (takingChoice)
            {
                var xCoordinate = Program.TakeNumber("Give X coordinate (top side numbers): ");
                var yCoordinate = Program.TakeNumber("Give Y coordinate (left side numbers): ");
                if (xCoordinate > Size || yCoordinate > Size || yCoordinate < 1 || xCoordinate < 1)
                {
                    continue;
                }
                if (Table[yCoordinate - 1, xCoordinate - 1] != null) continue;
                Table[yCoordinate - 1, xCoordinate - 1] = user;
                takingChoice = false;
            }

        }
        (int xScore, int oScore) GetFinalScore()
        {
            int xScore = 0;
            int oScore = 0;
            bool? lastSlot;
            int count;
            for (int i = 0; i < Size; i++)
            {
                lastSlot = Table[i, 0];
                count = 1;
                bool isAdded = false;
                for (int j = 1; j < Size; j++)
                {
                    isAdded = false;
                    if (Table[i, j] != lastSlot)
                    {
                        if (count >= 3)
                        {
                            if (lastSlot == true)
                            {
                                xScore += (count - 2);
                            }
                            else if (lastSlot == false)
                            {
                                oScore += (count - 2);
                            }
                        }
                        isAdded = true;
                        count = 0;
                        lastSlot = Table[i, j];
                    }
                    count++;
                }
                if (!isAdded)
                {
                    if (count >= 3)
                    {
                        if (lastSlot == true)
                        {
                            xScore += (count - 2);
                        }
                        else if (lastSlot == false)
                        {
                            oScore += (count - 2);
                        }
                    }
                }
            }//horizontal
            for (int i = 0; i < Size; i++)
            {
                lastSlot = Table[0, i];
                count = 1;
                bool isAdded = false;
                for (int j = 1; j < Size; j++)
                {
                    isAdded = false;
                    if (Table[j, i] != lastSlot)
                    {
                        if (count >= 3)
                        {
                            if (lastSlot == true)
                            {
                                xScore += (count - 2);
                            }
                            else if (lastSlot == false)
                            {
                                oScore += (count - 2);
                            }
                        }
                        isAdded = true;
                        count = 0;
                        lastSlot = Table[j, i];
                    }
                    count++;
                }
                if (!isAdded)
                {
                    if (count >= 3)
                    {
                        if (lastSlot == true)
                        {
                            xScore += (count - 2);
                        }
                        else if (lastSlot == false)
                        {
                            oScore += (count - 2);
                        }
                    }
                }
            }//vertical
            { //from left to right diagonal \
                for (int i = Size - 3; i >= 0; i--)
                {
                    int temp = i;
                    lastSlot = Table[0, temp++];
                    count = 1;
                    bool isAdded = false;
                    for (int j = 1; j < Size && temp < Size; j++, temp++)
                    {
                        isAdded = false;
                        if (Table[j, temp] != lastSlot)
                        {
                            if (count >= 3)
                            {
                                if (lastSlot == true)
                                {
                                    xScore += (count - 2);
                                }
                                else if (lastSlot == false)
                                {
                                    oScore += (count - 2);
                                }
                            }
                            isAdded = true;
                            count = 0;
                            lastSlot = Table[j, temp];
                        }
                        count++;
                    }
                    if (!isAdded)
                    {
                        if (lastSlot == true)
                        {
                            xScore += (count - 2);
                        }
                        else if (lastSlot == false)
                        {
                            oScore += (count - 2);
                        }
                    }
                }
                for (int i = 1; i <= Size - 3; i++)
                {
                    int temp = i;
                    lastSlot = Table[temp++, 0];
                    count = 1;
                    bool isAdded = false;
                    for (int j = 1; j < Size && temp < Size; j++, temp++)
                    {
                        isAdded = false;
                        if (Table[temp, j] != lastSlot)
                        {
                            if (count >= 3)
                            {
                                if (lastSlot == true)
                                {
                                    xScore += (count - 2);
                                }
                                else if (lastSlot == false)
                                {
                                    oScore += (count - 2);
                                }
                            }
                            isAdded = true;
                            count = 0;
                            lastSlot = Table[temp, j];
                        }
                        count++;
                    }
                    if (!isAdded)
                    {
                        if (lastSlot == true)
                        {
                            xScore += (count - 2);
                        }
                        else if (lastSlot == false)
                        {
                            oScore += (count - 2);
                        }
                    }
                }
            }
            {//from right to left diagonal /
                for (int i = 2; i < Size; i++)
                {
                    int temp = i;
                    lastSlot = Table[0, temp--];
                    count = 1;
                    bool isAdded = false;
                    for (int j = 1; j < Size && temp >= 0; j++, temp--)
                    {
                        isAdded = false;
                        if (Table[j, temp] != lastSlot)
                        {
                            if (count >= 3)
                            {
                                if (lastSlot == true)
                                {
                                    xScore += (count - 2);
                                }
                                else if (lastSlot == false)
                                {
                                    oScore += (count - 2);
                                }
                            }
                            isAdded = true;
                            count = 0;
                            lastSlot = Table[j, temp];
                        }
                        count++;
                    }
                    if (!isAdded)
                    {
                        if (lastSlot == true)
                        {
                            xScore += (count - 2);
                        }
                        else if (lastSlot == false)
                        {
                            oScore += (count - 2);
                        }
                    }
                }
                for (int i = 1; i <= Size - 3; i++)
                {
                    int temp = i;
                    lastSlot = Table[temp++, Size - 1];
                    count = 1;
                    bool isAdded = false;
                    for (int j = Size - 2; j >= 0 && temp < Size; j--, temp++)
                    {
                        isAdded = false;
                        if (Table[temp, j] != lastSlot)
                        {
                            if (count >= 3)
                            {
                                if (lastSlot == true)
                                {
                                    xScore += (count - 2);
                                }
                                else if (lastSlot == false)
                                {
                                    oScore += (count - 2);
                                }
                            }
                            isAdded = true;
                            count = 0;
                            lastSlot = Table[temp, j];
                        }
                        count++;
                    }
                    if (!isAdded)
                    {
                        if (lastSlot == true)
                        {
                            xScore += (count - 2);
                        }
                        else if (lastSlot == false)
                        {
                            oScore += (count - 2);
                        }
                    }
                }
            }
            return (xScore, oScore);
        }
        (bool xWon, bool oWon, bool allTaken) ClassicVictoryCheck()
        {
            bool? lastSlot;
            for (int i = 0; i < 3; i++)
            {
                lastSlot = Table[i, 0];
                bool skipped = false;
                for (int j = 1; j < 3; j++)
                {
                    if (Table[i, j] != lastSlot)
                    {
                        skipped = true;
                        break;
                    }
                }
                if (!skipped)
                {
                    if (lastSlot == true)
                        return (true, false, false);
                    else if (lastSlot == false)
                        return (false, true, false);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                lastSlot = Table[0, i];
                bool skipped = false;
                for (int j = 1; j < 3; j++)
                {
                    if (Table[j, i] != lastSlot)
                    {
                        skipped = true;
                        break;
                    }
                }
                if (!skipped)
                {
                    if (lastSlot == true)
                        return (true, false, false);
                    else if (lastSlot == false)
                        return (false, true, false);
                }
            }
            if (Table[0, 0] == Table[1, 1] && Table[0, 0] == Table[2, 2])
            {
                if (Table[1, 1] == true)
                    return (true, false, false);
                else if (Table[1, 1] == false)
                    return (false, true, false);
            }
            if (Table[2, 0] == Table[1, 1] && Table[2, 0] == Table[0, 2])
            {
                if (Table[1, 1] == true)
                    return (true, false, false);
                else if (Table[1, 1] == false)
                    return (false, true, false);
            }
            foreach (var item in Table)
            {
                if (item != null) return (false, false, false);
            }
            return (false, false, true);
        }
        void DrawTable()
        {
            StringBuilder sb = new();
            int size = Table.GetLength(0);
            if (size > 9) sb.Append(' ');
            sb.Append("   | ");
            for (int i = 1; i <= size; i++)
            {
                sb.Append($"{i} | ");
            }
            int lineSize = sb.Length + 2;
            sb.Append('\n');

            for (int i = 1; i <= size; i++)
            {
                sb.Append(new string('-', lineSize));
                sb.Append('\n');
                if (i < 10 && size >= 10)
                {
                    sb.Append(' ');
                }
                sb.Append($" {i} | ");

                for (int j = 0; j < Table.GetLength(0); j++)
                {
                    if (j >= 9)
                    {
                        sb.Append(' ');
                    }
                    switch (Table[i - 1, j])
                    {
                        case true:
                            sb.Append("X | ");
                            break;
                        case false:
                            sb.Append("O | ");
                            break;
                        default:
                            sb.Append("  | ");
                            break;
                    }

                }
                sb.Append('\n');
            }
            sb.Append(new string('-', lineSize));

            Console.WriteLine(sb);
        }
    }
}
