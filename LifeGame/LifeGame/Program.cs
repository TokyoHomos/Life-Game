using System;
using System.IO;
using System.Threading;

namespace LifeGame
{
    internal class Program
    {
        static int[,] grid;
        static int[,] newGrid;
        static int gridRow;
        static int gridCol;
        static int generation;
        static void InitializeGrid()
        {
            Random random = new Random();
        
            for (int i = 0; i < gridRow; i++)
            {
                for (int j = 0; j < gridCol; j++)
                {
                    grid[i, j] = random.Next(0, 2); // 0 1
                }
            }
        }
        static void DisplayGrid()
        {
            for (int i = 0; i < gridRow; i++)
            {
                for (int j = 0; j < gridCol; j++)
                {
                    Console.Write($"{grid[i, j]}  ");
                }
                Console.WriteLine();
            }
        }
        static int CountLiveNeighbors(int x, int y) 
        {
            int count = 0;
        
            for (int i = -1; i <= 1; i++) 
            {
                for (int n = -1; n <= 1; n++)
                {
                    if (i == 0 && n == 0)
                    {
                        continue;
                    }
                    int xi = x + i;
                    int yn = y + n;
        
                    if (xi >= 0 && xi < gridRow && yn >= 0 && yn < gridCol)
                    {
                        if (grid[xi, yn] == 1)
                        {
                            count++;
                        }
                    }
                }
            }
        
            return count;
        }
        static void UpdateGrid()
        {
            for (int i = 0; i < gridRow; i++)
            {
                for (int n = 0; n < gridCol; n++)
                {
                    int neighbors = CountLiveNeighbors(i, n);
        
                    if (grid[i, n] == 1)
                    {
                        if (neighbors < 2 || neighbors > 3)
                            newGrid[i, n] = 0; // dead
                        else
                            newGrid[i, n] = 1; // stay alive
                    }
                    else
                    {
                        if (neighbors == 3)
                            newGrid[i, n] = 1; // born
                        else
                            newGrid[i, n] = 0; // still dead
                    }
                }
            }
            for (int a = 0; a < gridRow; a++)
            {
                for (int b = 0; b < gridCol; b++)
                {
                    grid[a, b] = newGrid[a, b];
                }
            }
        
        }
        static void SaveGenerationData(int generation)
        {
            using (StreamWriter writer = new StreamWriter("Generatins.txt", true))
            {
                writer.WriteLine($"Generation {generation}: ");
                for (int i = 0; i < gridRow; i++)
                {
                    for (int n = 0; n < gridCol; n++)
                    {
                        writer.Write(newGrid[i, n]);
                    }
                    writer.WriteLine();
                }
                writer.Close();
            }
        
        
        }
        static void Main(string[] args)
        {
            Console.Write("Enter Grid Row: ");
            gridRow = int.Parse(Console.ReadLine());
            Console.Write("Enter Grid Col: ");
            gridCol = int.Parse(Console.ReadLine());
            Console.Write("Enter How Much Generation Need: ");
            generation = int.Parse(Console.ReadLine());
        
            grid = new int[gridRow, gridCol];
            newGrid = new int[gridRow, gridCol];
        
            InitializeGrid();
        
            for (int i = 1; i <= generation; i++)
            {
                Console.Clear();
                Console.WriteLine($"Generation: {i}");
                DisplayGrid();
                SaveGenerationData(generation);
                UpdateGrid();
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
        
            }
        
        
        
            Console.ReadKey();
        }
    }
}
        