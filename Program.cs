using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;    

class SnakeGame
{
    static List<int> scores = new List<int>();
    static bool gameOver = false;
    static int x = 5;
    static int appleX = 10;
    static int appleY = 1;
    static int appleGenTimer = 0;
    static int points = 0;
    static int gamespeed = 10;
    static void Main()
    {
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                    if(x>0)
                    {
                        x--;   
                    }
                    break;
                    case ConsoleKey.RightArrow:
                    if(x<20)
                    {    
                        x++;
                    }
                    break;
                    case ConsoleKey.Escape:
                    EndGame();
                    break;
                }

                
            }

            if (appleGenTimer == 40)
            {
                if (appleY==10)
                {
                    if (appleX >= x && appleX <= x + 4)
                    {
                        points++;
                        appleY = 1;
                        Random rng = new Random();
                        appleX = rng.Next(0, 20);
                    }
                    else 
                    {
                        EndGame();
                        SaveScore(points*10, "Scores.txt");
                    }
                }
                appleY++;    
                appleGenTimer = 0;
            }

            gamespeed = 200+points; //Increases Gamespeed over time
            Console.Clear();
            Console.WriteLine($"Apple Catcher | Score: {points*10} | HighScore: {GetCurrentHighScore("Scores.txt")}");     //
            Console.SetCursorPosition(appleX, appleY);                  //
            Console.WriteLine("O");                                     //              Renders each frame
            Console.SetCursorPosition(x, 10);                           // 
            Console.WriteLine("\\___/");                                // 
            Console.CursorVisible = false;                               //

            appleGenTimer++;                                            
            System.Threading.Thread.Sleep(1000/gamespeed);              //      makes the apple fall 1 space every 40000ms/gamespeed
            
        }
    }
    static void SaveScore(int score, string highScoreFile)
    {

        // Read existing scores from the file
        if (File.Exists(highScoreFile))
        {
            using (StreamReader reader = new StreamReader(highScoreFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out int existingScore))
                    {
                        scores.Add(existingScore);
                    }
                }
            }
        }
        scores.Add(score);

        // Sort the scores in descending order and keep only the top 10
        scores = scores.OrderByDescending(s => s).Take(10).ToList();

        // Write the top 10 scores back to the file
        using (StreamWriter writer = new StreamWriter(highScoreFile, false)) // false to overwrite
        {
            foreach (var s in scores)
            {
                writer.WriteLine(s);
            }
        }
    }
        static int GetCurrentHighScore(string highScoreFile)
    {
        int highestScore = 0;

        // Check if the file exists
        if (File.Exists(highScoreFile))
        {
            using (StreamReader reader = new StreamReader(highScoreFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Try to parse each line to an integer
                    if (int.TryParse(line, out int score))
                    {
                        // Update the highest score if the current score is greater
                        if (score > highestScore)
                        {
                            highestScore = score;
                        }
                    }
                }
            }
        }

        return highestScore;
    }
    static void EndGame()
    {
        gameOver = true;
    }
}