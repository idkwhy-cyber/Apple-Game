using System;
using System.Collections.Generic;
using System.Threading;

class SnakeGame
{

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
                    }
                }
                appleY++;    
                appleGenTimer = 0;
            }

            gamespeed = 200+points; //Increases Gamespeed over time
            Console.Clear();
            Console.WriteLine("Apple Catcher | Points: " + points);     //
            Console.SetCursorPosition(appleX, appleY);                  //
            Console.WriteLine("O");                                     //              Renders each frame
            Console.SetCursorPosition(x, 10);                           // 
            Console.WriteLine("\\___/");                                // 
            Console.CursorVisible = false;                               //

            appleGenTimer++;                                            
            System.Threading.Thread.Sleep(1000/gamespeed);              //      makes the apple fall 1 space every 40000ms/gamespeed
            
        }
    }
    static void EndGame()
    {
        gameOver = true;
    }
}