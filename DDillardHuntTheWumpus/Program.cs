using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;


namespace DDillardHuntTheWumpus
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        //player 
        static int numOfArrows = 5;  //End game if out of arrows, each arrow visits 5 rooms 
        static int playerLiving = 1; //used in StartGame, RunGame
        static string userChoice;    //used in StartGame

        //environment 
        static int roomOfUser = -1;

        static int adjacentRoom_1, adjacentRoom_2, adjacentRoom_3;
        static int roomOfWumpus, roomFirstBats, roomSecondBats, roomFirstPits, roomSecondPits;
        // static string[] room = new string[20];
       //  static int[] room = new int[20];
        // static string[] adjacentRooms = new string[20];
       //  static int[] adjacentRooms = new int[60];
        static string[] roomDescription = new string[20];
        static int lowRV = 1;       //Low, High Room Value: Requirements for 1-20
        static int highRV = 20;     

        public class Room
        {
            public int roomNumber { get; set; }

            public int adjacentRoom1 = 0;
            public int adjacentRoom2 = 0;
            public int adjacentRoom3 = 0;

            int[] link = new int[2];
            //Methods to do
        }

        public class Square
        {
            public VertexPositionTexture[] Verticies;

            public Square()
            {
                Verticies = new VertexPositionTexture[4];
            }
        }



    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]



        public static void StartGame()
        {
            //TODO: Make Random Room Gen recursive
            Random rnd = new Random();
            roomOfUser = rnd.Next(lowRV, highRV);

            //all unique rooms have unique room #
            do
            {
                roomOfWumpus = rnd.Next(lowRV, highRV);
            } while (roomOfWumpus == roomOfUser);

            do
            {
                roomFirstBats = rnd.Next(lowRV, highRV);
            } while ( roomFirstBats == roomOfUser || roomFirstBats == roomOfWumpus);

            do
            {
                roomSecondBats = rnd.Next(lowRV, highRV);
            } while (roomSecondBats == roomOfUser || roomSecondBats == roomOfWumpus || roomSecondBats == roomFirstBats);

            do
            {
                roomFirstPits = rnd.Next(lowRV, highRV);
            } while (roomFirstPits == roomOfUser || roomFirstPits == roomOfWumpus || roomFirstPits == roomFirstBats || roomFirstPits == roomSecondBats);

            do
            {
                roomSecondPits = rnd.Next(lowRV, highRV);
            } while (roomSecondPits == roomOfUser || roomSecondPits == roomOfWumpus || roomSecondPits == roomFirstBats || roomSecondPits == roomSecondBats || roomSecondPits == roomFirstPits);


            //logic to generate adjacent rooms 
            Room[] myRoomArray = new Room[20];  //create 20 rooms

            //for (int i =0; i < myRoomArray.Length; i++)
            //{
            //    if Room(adjacentRoom_1)
            //}
            myRoomArray[1].adjacentRoom1 = 2;
            myRoomArray[1].adjacentRoom2 = 5;
            myRoomArray[1].adjacentRoom3 = 8;

            myRoomArray[2].adjacentRoom1 = 1;
            myRoomArray[2].adjacentRoom2 = 3;
            myRoomArray[2].adjacentRoom3 = 10;

            myRoomArray[3].adjacentRoom1 = 2;
            myRoomArray[3].adjacentRoom2 = 4;
            myRoomArray[3].adjacentRoom3 = 12;

            myRoomArray[4].adjacentRoom1 = 3;
            myRoomArray[4].adjacentRoom2 = 5;
            myRoomArray[4].adjacentRoom3 = 14;

            myRoomArray[5].adjacentRoom1 = 1;
            myRoomArray[5].adjacentRoom2 = 4;
            myRoomArray[5].adjacentRoom3 = 6;

            myRoomArray[6].adjacentRoom1 = 5;
            myRoomArray[6].adjacentRoom2 = 7;
            myRoomArray[6].adjacentRoom3 = 15;

            myRoomArray[7].adjacentRoom1 = 6;
            myRoomArray[7].adjacentRoom2 = 8;
            myRoomArray[7].adjacentRoom3 = 17;

            myRoomArray[8].adjacentRoom1 = 1;
            myRoomArray[8].adjacentRoom2 = 7;
            myRoomArray[8].adjacentRoom3 = 9;

            myRoomArray[9].adjacentRoom1 = 8;
            myRoomArray[9].adjacentRoom2 = 10;
            myRoomArray[9].adjacentRoom3 = 18;

            myRoomArray[10].adjacentRoom1 = 2;
            myRoomArray[10].adjacentRoom2 = 9;
            myRoomArray[10].adjacentRoom3 = 11;

            myRoomArray[11].adjacentRoom1 = 10;
            myRoomArray[11].adjacentRoom2 = 12;
            myRoomArray[11].adjacentRoom3 = 19;

            myRoomArray[12].adjacentRoom1 = 3;
            myRoomArray[12].adjacentRoom2 = 11;
            myRoomArray[12].adjacentRoom3 = 13;

            myRoomArray[13].adjacentRoom1 = 12;
            myRoomArray[13].adjacentRoom2 = 14;
            myRoomArray[13].adjacentRoom3 = 20;

            myRoomArray[14].adjacentRoom1 = 4;
            myRoomArray[14].adjacentRoom2 = 13;
            myRoomArray[14].adjacentRoom3 = 15;

            myRoomArray[15].adjacentRoom1 = 6;
            myRoomArray[15].adjacentRoom2 = 14;
            myRoomArray[15].adjacentRoom3 = 16;

            myRoomArray[16].adjacentRoom1 = 15;
            myRoomArray[16].adjacentRoom2 = 17;
            myRoomArray[16].adjacentRoom3 = 20;

            myRoomArray[17].adjacentRoom1 = 7;
            myRoomArray[17].adjacentRoom2 = 16;
            myRoomArray[17].adjacentRoom3 = 18;

            myRoomArray[18].adjacentRoom1 = 9;
            myRoomArray[18].adjacentRoom2 = 17;
            myRoomArray[18].adjacentRoom3 = 19;

            myRoomArray[19].adjacentRoom1 = 11;
            myRoomArray[19].adjacentRoom2 = 18;
            myRoomArray[19].adjacentRoom3 = 20;

            myRoomArray[20].adjacentRoom1 = 13;
            myRoomArray[20].adjacentRoom2 = 16;
            myRoomArray[20].adjacentRoom3 = 19;


            Console.Out.WriteLine("Welcome to Hunt The Wumpus!");
            Console.Out.WriteLine("Press 'Y' to Play, or any other key to quit");


            //check random gen 
            /*
            Console.Out.WriteLine("Room of Player = " + roomOfUser);
            Console.Out.WriteLine("Room of Wumpus = "+roomOfWumpus);
            Console.Out.WriteLine("Room of 1B = " + roomFirstBats);
            Console.Out.WriteLine("Room of 2B = " + roomSecondBats);
            Console.Out.WriteLine("Room of 1P = " + roomFirstPits);
            Console.Out.WriteLine("Room of 2P = " + roomSecondPits);
            */
            userChoice = Console.ReadLine();

            //input = Console.Read();
            if (userChoice == "Y" || userChoice == "y")
            {
                Console.Out.WriteLine("In this game, you have <fill in later--!");
                Console.Out.WriteLine(" Game Rules");
                playerLiving = 1;
                GameLoop(roomOfUser, numOfArrows);
            }
            else
            {
                Console.Out.WriteLine("Goodbye!");
                playerLiving = -1;
                //playerLiving set at -1, drops out and ends game 
            }

            // } while (playerLiving == -1);

            //action to happen at the start of every "round"
            if (playerLiving == 1)
            {
                Console.Out.WriteLine("You are standing in room #");
                Console.Out.WriteLine("Press 'M' to Move, or press 'S' to shoot");
                Console.ReadKey();
            }
            else if (playerLiving == 0)
            {
                Console.Out.WriteLine("You have failed! Game Over!");
                Console.Out.WriteLine("Press P to play again, or any other key to exit");
                // to do - write game logic to enter start of game again 
                Console.ReadKey();
            }
            else
            {
                Console.ReadKey();
            }


        } //End Start Game 


        public static void GameLoop(int userRoom, int userArrows)
        {
            roomOfUser = userRoom;
            numOfArrows = userArrows;

           // myRoomArray[userRoom] = myRoom; //set randomly generated value for user room to value in myRoomArray


            Console.WriteLine("You are in room: " + roomOfUser);
            Console.WriteLine("(M)ove or (S)hoot?");
            Console.WriteLine("You see paths before you to rooms {0}, {1}, and {2}" /*CHECK AGAINST myRoomArray Adjacent rooms*/);

            userChoice = Console.ReadLine();

        }



        static void Main()
        {
            


            using (var game = new Game1())

                StartGame();

            Console.ReadKey();


        } //End Main

    } // End Program

} // End Hunt the Wumpus 
#endif
