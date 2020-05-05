using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FlappyBird
{
    class MainEngine
    {

        public int user_choice;
        public int total_moves;
        public List<Body> bodies;

        public void DisplayMenu()
        {
            while (true)
            {
                ClearScreen();
                Console.Write("\n\n\t\t\t\t FLAPPY BIRD \n\n\n\n");
                Console.Write("\t\t\t\t1.Play");
                Console.Write("\t\t\t\t2.Exit");
                Console.Write("\n\n\n\n\tEnter your choice:");
                Console.WindowWidth = 100;
                Console.WindowHeight = 50;

                try
                {
                    user_choice = int.Parse(Console.ReadLine());
                }
                catch { }

                ParseUserChoice();
            }

        }

        public void drawObject(Body obj)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(obj.x, obj.y);
            Console.Write(obj.rep);
        }

        public void drawUser(Bird obj)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(obj.x, obj.y);
            Console.Write(obj.rep);
        }

        private void User_exit()
        {
            Environment.Exit(0);
        }

        public void GameOver(Bird user)
        {
            ClearScreen();
            Console.WriteLine("\n\n\n\n\n\t\t\tGAME OVER!!!!\n\n\n\n\t\t\t YOUR SCORE: {0} \n\n\n\n\t\t\tPress Enter key to go back to menu: ", user.score / 2);
            while (true)
            {
                ConsoleKey consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == ConsoleKey.Enter)
                    break;
            }

            DisplayMenu();
        }

        private void PlayGame()
        {
            var r = new Random();
            bodies = new List<Body>();
            var user = new Bird();
            Int64 wallCount = 0;

            while (true)
            {
                if (wallCount % 40 == 0)
                {
                    var obj = new Body(r);
                    var obj2 = new Body();

                    obj2.y = obj.y + 10;
                    obj2.x = obj.x;
                    bodies.Add(obj);
                    bodies.Add(obj2);
                }
                ClearScreen();

                for (int j = 0; j < bodies.Count(); j++)
                {
                    drawObject(bodies[j]);

                    if ((bodies[j].x == user.x && bodies[j + 1].x == user.x && (user.y > bodies[j + 1].y || user.y < bodies[j].y)))
                    {
                        GameOver(user);
                    }

                    bodies[j].x--;

                    if (bodies[j].x == user.x)
                        user.score++;

                    if (bodies[j].x == 0)
                    {
                        bodies.RemoveAt(j);
                    }

                }

                drawUser(user);
                ConsoleKey keyinfo;
                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    keyinfo = Console.ReadKey(true).Key;

                    if (keyinfo == ConsoleKey.UpArrow)
                        user.y--;
                }
                else
                    user.y++;

                if (user.y > 50 || user.y < 1)
                    GameOver(user);

                wallCount++;
                drawUser(user);
            }
        }

        private void ClearScreen()
        {
            Console.Clear();
        }

        public void ParseUserChoice()
        {
            if (user_choice == 2)
                User_exit();
            else if (user_choice == 1)
            {
                total_moves = 0;
                ClearScreen();
                PlayGame();
            }
            else
            {
                ClearScreen();
                Console.WriteLine("\n\n\n\n\n\n\t\t\t\t\t INVALID CHOICE!");
                Console.WriteLine("\n\n\n\t\t\t\t\t\t Press any key to go back to the MENU!");
                Console.ReadKey();
            }
        }

    }
}
