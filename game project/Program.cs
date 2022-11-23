using System;
using System.Diagnostics;

namespace game_project
{
    class Program
    {
        public static char Medir;
        public static char meChar = 'X';
        public static char enemyChar = 'O';
        public static char foodChar = 'I';
        public static Int32 Xval = StartX;
        public static Int32 Yval = StartY;
        public static Int32 OldXval;
        public static Int32 OldYval;
        public static Int32 StartY = Console.WindowHeight / 2;
        public static Int32 StartX = Console.WindowWidth / 2;
        public static Int32 speed = 1;
        public static Int32 food = 6;
        public static Int32 ennemies = 12;
        public static Int32[] FoodX = new Int32 [food];
        public static Int32[] FoodY = new Int32 [food];
        public static Int32[] EnnemyX = new Int32[food];
        public static Int32[] EnnemyY = new Int32[food];
        public static Int32 Score = 0;
        // a bunch of global variables to make it easier so that I dont have to pass  thousands pointers and variables through function //
        static void Main(string[] args)
        {
            Int32 answer;
            do
            {

                Console.Clear();
                Console.WriteLine("1. Play Now");
                Console.WriteLine(" ");
                Console.WriteLine("2. Exit");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("Capture as much food (I) as possible while avoiding the rotten food (O)");
                Console.WriteLine("MOVE WITH ARROW KEYS");
                Console.WriteLine("If you get food you get 1 point if you get rotten food you loose 1 point");
                answer = gogetnum("Enter your Choice");

                switch (answer) // switch to give the game info and a type of menu incase I expand on the game and add an options menu to change speed, #of ennemies, gamelength, etc
                {
                    case 1:
                        Console.Clear();
                        videogame();
                        break;
                    case 2:
                        Console.WriteLine("Exit");
                        break;
                }

            } while (answer < 2);
            Console.WriteLine("Alright have a nice day");
            if (answer > 2)//copied from my last lab
            {
                Console.WriteLine("maybe write a number between 1 and 3 next time :(");
            }

   

            



        }
        static void videogame() //the main video game function 
        {
            Console.CursorVisible = false;
            Int32 Time1 = 70;
            Int32 gamelength = 2;
            Console.WindowHeight = 50;
            Console.WindowWidth = 100;
            ConsoleKey k = ConsoleKey.NoName;
            var sw = Stopwatch.StartNew(); //stop watch function worked better for ending the game after any # of minutes
            Int32 i;
            Int32 j;
            Random r = new Random();
            Random r1 = new Random();
            Random r3 = new Random();
            Random r4 = new Random(); 
            for (j = 0; j < food; j++) //giving the food and bad food random start positions
            {
                Int32 genrand3 = r3.Next(0, 80);
                Int32 genrand4 = r4.Next(0, 50);
                EnnemyX[j] = genrand3;
                EnnemyY[j] = genrand4;
            }
            for (i = 0; i < food; i++)
            {
                Int32 genrand = r.Next(0, 80);
                Int32 genrand2 = r1.Next(0, 50);
                FoodX[i] = genrand;
                FoodY[i] = genrand2;
            }
            while (gameover(k, gamelength, sw) != false)
            {
                k = keystroke();
                wait(Time1);
                move(k);
                draw();
                collisioncheck();
            }
            Console.Clear(); //end of game text displays score and time elapsed
            Console.WriteLine("Your score was " + Score + " After " + gamelength + " minutes of playing!");
            Console.WriteLine("Press Enter to Continue");
            Console.ReadKey();
            Console.WriteLine("Do better Next time!");
            Console.ReadKey();

        }
        static void collisioncheck() //collisions increase or decrease score and move the target to another location since I couldnt find a way to make it disapear
        {
            Random r = new Random();
            Random r1 = new Random();
            Random r3 = new Random();
            Random r4 = new Random();
            for (Int32 I= 0; I < food; I++)
            {
                Int32 genrand = r.Next(0, 80);
                Int32 genrand2 = r1.Next(0, 50);
                Int32 genrand3 = r3.Next(0, 80);
                Int32 genrand4 = r4.Next(0, 50);
                if (Xval == FoodX[I] && Yval == FoodY[I])
                {
                    FoodX[I] = genrand;
                    FoodY[I] = genrand2;
                    Score += 1;
                }
                if (Xval == EnnemyX[I] && Yval == EnnemyY[I])
                {
                    EnnemyX[I] = genrand3;
                    EnnemyY[I] = genrand4;
                    Score -= 1;
                }


            }
        }

        static void move(ConsoleKey k)
        {

            if (Medir == 'E')
            {
                Xval += speed;
            }
            if (Medir == 'S')
            {
                Yval += speed;
            }
            if (Medir == 'N')
            {
                Yval -= speed;
            }
            if (Medir == 'W')
            {
                Xval -= speed;
            }

            if (Xval >= Console.WindowWidth)
            {
                Xval = 0;
            }
            if (Yval >= Console.WindowHeight)
            {
                Yval = 0;
            }
            if (Xval < 0)
            {
                Xval = Console.WindowWidth;
            }
            if(Yval < 0)
            {
                Yval = Console.WindowHeight;
            }
            OldXval = Xval;
            OldYval = Yval;
            


        } //a simple move function completed a while ago..
        static ConsoleKey keystroke()
        {
            ConsoleKey k = ConsoleKey.NoName;
            

                if (Console.KeyAvailable)
                {
                    k = Console.ReadKey(true).Key;
                }
                if (k == ConsoleKey.RightArrow)
                {
                    Medir = 'E';
                }
                if (k == ConsoleKey.DownArrow)
                {
                    Medir = 'S';
                }
                if (k == ConsoleKey.UpArrow)
                {
                    Medir = 'N';
                }
                if (k == ConsoleKey.LeftArrow)
                {
                    Medir = 'W';
                }


            return k;
        } // keystroke function completed a while ago...
        static void wait(Int32 Time1) // a wait function to make sure the character doesnt move too fast and to create a type of delay
        {
            DateTime wait = DateTime.Now;
            while (Math.Abs((DateTime.Now.Millisecond - wait.Millisecond)) < Time1)
            {

            }


        } 
        static Boolean gameover(ConsoleKey j, Int32 Gamelength, Stopwatch sw) //game is ended if time exceeds the gamelength chosen

        {
            DateTime time = DateTime.Now;
            TimeSpan ts = sw.Elapsed;
            if ( ts.Minutes >= Gamelength )
            {
                return false;
            }



            return true;
        }

        static void draw() //the draw function which makes everything appear in their positions
        {
            
            Console.Clear();
            for(Int32 i = 0; i <food; i++)
            {
                Console.SetCursorPosition(FoodX[i], FoodY[i]);
                Console.Write(foodChar);
                Console.SetCursorPosition(EnnemyX[i], EnnemyY[i]);
                Console.Write(enemyChar);
            }

             Console.SetCursorPosition(Xval, Yval);
                Console.Write(meChar);
           
        }

        static Int32 gogetnum(String message)
        {
            bool check = false;
            Int32 number;
            do
            {

                Console.WriteLine(message);
                check = (int.TryParse(Console.ReadLine(), out number));
                if (check == false)
                {
                    Console.WriteLine("Maybe type a number???");
                }
            } while (check == false);
            return number;
        }

    }
}
