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


        //game environment 
        static int roomOfUser = -1;
        static bool newGameCheck = true;

        //rooms
        static int adjacentRoom_1, adjacentRoom_2, adjacentRoom_3;
        static int roomOfWumpus, roomFirstBats, roomSecondBats, roomFirstPits, roomSecondPits;
        static string[] roomDescription = new string[20];
        static int lowRV = 1;       //Low, High Room Value: Requirements for 1-20
        static int highRV = 20;
        static int userNextRoom;

        //monster
        static int wumpusAwake = 0;
        static bool wampusAlive = true;


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
            //Room[] myRoomArray = new Room[20];  //create 20 rooms

            ////for (int i =0; i < myRoomArray.Length; i++)
            ////{
            ////    if Room(adjacentRoom_1)
            ////}
            //myRoomArray[1].adjacentRoom1 = 2;
            //myRoomArray[1].adjacentRoom2 = 5;
            //myRoomArray[1].adjacentRoom3 = 8;

            //myRoomArray[2].adjacentRoom1 = 1;
            //myRoomArray[2].adjacentRoom2 = 3;
            //myRoomArray[2].adjacentRoom3 = 10;

            //myRoomArray[3].adjacentRoom1 = 2;
            //myRoomArray[3].adjacentRoom2 = 4;
            //myRoomArray[3].adjacentRoom3 = 12;

            //myRoomArray[4].adjacentRoom1 = 3;
            //myRoomArray[4].adjacentRoom2 = 5;
            //myRoomArray[4].adjacentRoom3 = 14;

            //myRoomArray[5].adjacentRoom1 = 1;
            //myRoomArray[5].adjacentRoom2 = 4;
            //myRoomArray[5].adjacentRoom3 = 6;

            //myRoomArray[6].adjacentRoom1 = 5;
            //myRoomArray[6].adjacentRoom2 = 7;
            //myRoomArray[6].adjacentRoom3 = 15;

            //myRoomArray[7].adjacentRoom1 = 6;
            //myRoomArray[7].adjacentRoom2 = 8;
            //myRoomArray[7].adjacentRoom3 = 17;

            //myRoomArray[8].adjacentRoom1 = 1;
            //myRoomArray[8].adjacentRoom2 = 7;
            //myRoomArray[8].adjacentRoom3 = 9;

            //myRoomArray[9].adjacentRoom1 = 8;
            //myRoomArray[9].adjacentRoom2 = 10;
            //myRoomArray[9].adjacentRoom3 = 18;

            //myRoomArray[10].adjacentRoom1 = 2;
            //myRoomArray[10].adjacentRoom2 = 9;
            //myRoomArray[10].adjacentRoom3 = 11;

            //myRoomArray[11].adjacentRoom1 = 10;
            //myRoomArray[11].adjacentRoom2 = 12;
            //myRoomArray[11].adjacentRoom3 = 19;

            //myRoomArray[12].adjacentRoom1 = 3;
            //myRoomArray[12].adjacentRoom2 = 11;
            //myRoomArray[12].adjacentRoom3 = 13;

            //myRoomArray[13].adjacentRoom1 = 12;
            //myRoomArray[13].adjacentRoom2 = 14;
            //myRoomArray[13].adjacentRoom3 = 20;

            //myRoomArray[14].adjacentRoom1 = 4;
            //myRoomArray[14].adjacentRoom2 = 13;
            //myRoomArray[14].adjacentRoom3 = 15;

            //myRoomArray[15].adjacentRoom1 = 6;
            //myRoomArray[15].adjacentRoom2 = 14;
            //myRoomArray[15].adjacentRoom3 = 16;

            //myRoomArray[16].adjacentRoom1 = 15;
            //myRoomArray[16].adjacentRoom2 = 17;
            //myRoomArray[16].adjacentRoom3 = 20;

            //myRoomArray[17].adjacentRoom1 = 7;
            //myRoomArray[17].adjacentRoom2 = 16;
            //myRoomArray[17].adjacentRoom3 = 18;

            //myRoomArray[18].adjacentRoom1 = 9;
            //myRoomArray[18].adjacentRoom2 = 17;
            //myRoomArray[18].adjacentRoom3 = 19;

            //myRoomArray[19].adjacentRoom1 = 11;
            //myRoomArray[19].adjacentRoom2 = 18;
            //myRoomArray[19].adjacentRoom3 = 20;

            //myRoomArray[20].adjacentRoom1 = 13;
            //myRoomArray[20].adjacentRoom2 = 16;
            //myRoomArray[20].adjacentRoom3 = 19;


            Console.Out.WriteLine("\n\nWelcome to Hunt The Wumpus!");
            Console.Out.WriteLine("Press 'Y' to Play, or any other key to quit");


            //check random gen - Pass 
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
           // roomOfUser = userRoom;
            //numOfArrows = userArrows;

            // myRoomArray[userRoom] = myRoom; //set randomly generated value for user room to value in myRoomArray



                int[,] map = new int[20, 3]
                    {
                    {1, 4, 7},
                    {0, 2, 9},
                    {1, 3, 11},
                    {2, 4, 13},
                    {0, 3, 5},
                    {4, 6, 14},
                    {5, 7, 16},
                    {0, 6, 8},
                    {7, 9, 17},
                    {1, 8, 10},
                    {9, 11, 18},
                    {2, 10, 12},
                    {11, 13, 19},
                    {3, 12, 14},
                    {5, 13, 15},
                    {14, 16, 19},
                    {6, 15, 17},
                    {8, 16, 18},
                    {10, 17, 19},
                    {12, 15, 18},
                    };


            // map[roomNum(0 - 19), tunnelNum(0 - 2)] returns the tunnels


            Console.WriteLine("\nYou are in room: " + roomOfUser + "\nArrows left: "+ numOfArrows);

            if(wumpusAwake == 1)
            {
                Random rnd = new Random();
                int chance = rnd.Next(0, 100);

                if (chance >= 24) //probablility of 25% for Wumpus move
                {
                    Console.WriteLine("The Wumpus is on the move...\n");
                    //equal chance to move to any of the 3 adjacent rooms 
                    if (chance >= 25 && chance <= 49)
                    {
                        roomOfWumpus = map[roomOfWumpus, 0];
                    }
                    else if (chance >= 50 && chance <= 74)
                    {
                        roomOfWumpus = map[roomOfWumpus, 1];
                    }
                    else
                    {
                        roomOfWumpus = map[roomOfWumpus, 2];
                    }
                }
                else { }
                
                //Console.WriteLine("Wumpus room: " + roomOfWumpus);
            }

            if (map[roomOfUser, 0] == roomOfWumpus || map[roomOfUser, 1] == roomOfWumpus || map[roomOfUser, 2] == roomOfWumpus)
            {
                Console.WriteLine("I smell a Wumpus");
            }
            if (map[roomOfUser, 0] == roomFirstBats || map[roomOfUser, 1] == roomFirstBats || map[roomOfUser, 2] == roomFirstBats || map[roomOfUser, 0] == roomSecondBats || map[roomOfUser, 1] == roomSecondBats || map[roomOfUser, 2] == roomSecondBats)
            {
                Console.WriteLine("Bats nearby");
            }
            if (map[roomOfUser, 0] == roomFirstPits || map[roomOfUser, 1] == roomFirstPits || map[roomOfUser, 2] == roomFirstPits || map[roomOfUser, 0] == roomSecondPits || map[roomOfUser, 1] == roomSecondPits || map[roomOfUser, 2] == roomSecondPits)
            {
                Console.WriteLine("I feel a draft");
            }




            Console.WriteLine("(M)ove or (S)hoot?");
            Console.WriteLine("You see paths before you to rooms {0}, {1}, and {2}", map[roomOfUser, 0], map[roomOfUser, 1], map[roomOfUser, 2]);

            userChoice = Console.ReadLine();

            if (userChoice == "m" || userChoice == "M")
            {
                Console.WriteLine("Which Room?");

                string l = Console.ReadLine();
                int value;
                if (int.TryParse(l, out value))
                {
                    userNextRoom = Convert.ToInt32(l);

                    if (userNextRoom == map[roomOfUser, 0] || userNextRoom == map[roomOfUser, 1] || userNextRoom == map[roomOfUser, 2])
                    {
                        //this new value should be updated if userNextRoom == roomFirstBats || roomSecondBats
                        roomOfUser = userNextRoom;
                        Console.WriteLine("You have moved to room " + roomOfUser); //success 

                        if (userNextRoom == roomOfWumpus)
                        {
                            if (wumpusAwake == 1)
                            {
                                Console.WriteLine("You got stung by Wumpus!");
                                Console.WriteLine("Game Over");
                                playerLiving = 0;
                            }
                            else
                            {
                                Console.WriteLine("... Ooops! Bumped a Wumpus");

                                wumpusAwake = 1;
                                GameLoop(userRoom, userArrows);
                            }
                        }
                        else if (userNextRoom == roomFirstBats || userNextRoom == roomSecondBats)
                        {
                            Console.WriteLine("Zap--Super Bat snatch! Elsewhereville for you!");
                            int batCountry = 0; //Only want to relocate bats that carried player
                            if(userNextRoom == roomFirstBats)
                            {
                                batCountry = 1;
                            }
                            else
                            {
                                batCountry = 2;
                            }

                            if (batCountry == 1)
                            {
                                //carry logic 
                                Random rnd = new Random();
                                do
                                {
                                    roomOfUser = rnd.Next(lowRV, highRV);
                                } while (roomOfUser == roomOfWumpus || roomOfUser == roomSecondBats || roomOfUser == roomFirstPits || roomOfUser == roomSecondPits);
                                do
                                {
                                    roomFirstBats = rnd.Next(lowRV, highRV);
                                } while (roomFirstBats == roomOfUser || roomFirstBats == roomOfWumpus || roomFirstBats == roomSecondBats || roomFirstBats == roomFirstPits || roomFirstBats == roomSecondPits);

                            }
                            else //using bat room 2
                            {
                                //carry logic 
                                Random rnd = new Random();
                                do
                                {
                                    roomOfUser = rnd.Next(lowRV, highRV);
                                } while (roomOfUser == roomOfWumpus || roomOfUser == roomFirstBats || roomOfUser == roomFirstPits || roomOfUser == roomSecondPits);
                                do
                                {
                                    roomFirstBats = rnd.Next(lowRV, highRV);
                                } while (roomSecondBats == roomOfUser || roomSecondBats == roomOfWumpus || roomSecondBats == roomSecondBats || roomSecondBats == roomFirstPits || roomSecondBats == roomSecondPits);
                            }

                            GameLoop(userRoom, userArrows);
                        }

                        else if (userNextRoom == roomFirstPits || userNextRoom == roomSecondPits)
                        {
                            Console.WriteLine("YYYIIIIEEEE . . . fell in a pit\n\nYou get nothing! Good day sir!\n");
                           // playerLiving = 0; UNCOMMENT LATER
                        }

                    }
                    else
                    {
                        Console.WriteLine("You can't go there from here");
                        GameLoop(userRoom, userArrows);
                    }
                }
                else
                {
                    Console.WriteLine("You input only Integers from 1 to 20");
                    GameLoop(userRoom, userArrows);
                }
            } // end move options 
                else if (userChoice == "s" || userChoice == "S")
            {
                Console.WriteLine("Pick 5 rooms you want the arrow to travel through.\nBe careful! Don't shoot yourself, and don't miss the Wumpus!");


                Console.WriteLine("Enter 5 sequential rooms, pressing enter between each room number");
                bool validFirePath = false;

                //reset to avoid issues with setting fire path again 
                int target1 = 99;
                int target2 = 99;
                int target3 = 99;
                int target4 = 99;
                int target5 = 99;

                while (validFirePath == false) { // not needed in future 

                    target1 = int.Parse(Console.ReadLine());
                    target2 = int.Parse(Console.ReadLine());
                    //TO DO - Bug, will fail if user inputs 12345 then presses multiple blank enters 
                        if (target1 == target2)
                        {
                            Console.WriteLine("Arrows aren't that crooked - try another room");
                            GameLoop(roomOfUser, numOfArrows);
                    }
                        target3 = int.Parse(Console.ReadLine());
                        if (target2 == target3)
                        {
                            Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }
                        target4 = int.Parse(Console.ReadLine());
                        if (target4 == target3)
                        {
                            Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }
                        target5 = int.Parse(Console.ReadLine());
                        if (target5 == target4)
                        {
                            Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }
                        else
                        {
                        validFirePath = true;
                        }
                    

                }

                Console.WriteLine("You raise your bow to fire through rooms: " + target1 +"-"+ target2 + "-" + target3 + "-" + target4 + "-" + target5);

                if(target1 == map[roomOfUser, 0] || target1 == map[roomOfUser, 1] ||target1 == map[roomOfUser, 2]) //at each Target room, check for valid input
                {
                  


                } else // if the target room is not valid, arrow goes on random path 
                {

                }
                if (target1 == roomOfWumpus || target2 == roomOfWumpus || target3 == roomOfWumpus || target4 == roomOfWumpus || target5 == roomOfWumpus)
                {
                    Console.WriteLine("Aha! You got the Wumpus!");
                    Console.WriteLine("Congratulations! You won the game!");
                    Console.WriteLine("Hee hee hee - the Wumpus'll getcha next time!!\n\n");
                    ExitGame();
                }else if (target1 == roomOfUser || target2 == roomOfUser || target3 == roomOfUser || target4 == roomOfUser || target5 == roomOfUser)
                {
                    Console.WriteLine("Ouch! Arrow got you!");
                    Console.WriteLine("Congratulations, you played yourself. \nGame over.");
                    ExitGame();
                }
                else
                {
                    Console.WriteLine("Missed!\nYou woke the Wumpus!");
                    numOfArrows--;
                    wumpusAwake = 1;
                }



            } // end shoot options 







            if (playerLiving == 1)
            {
                if (numOfArrows != 0)
                {
                    GameLoop(userRoom, userArrows); // Normal game loop 
                }
                else
                {
                    Console.WriteLine("You're out of arrows\n Game Over");
                    ExitGame();
                }


            }
            else
            {
                Console.WriteLine("You are now dead.");
                ExitGame();
            }


        }



        static void Main()
        {
            


            using (var game = new Game1())

                StartGame();

            Console.ReadKey();


        } //End Main

        public static void ExitGame()
        {
            Console.Out.WriteLine("Press P to play again, or any other key to exit");
            string playAgainInput = Console.ReadLine();
            if (playAgainInput == "p" || playAgainInput == "P")
            {
                StartGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }


    } // End Program

} // End Hunt the Wumpus 
#endif
