//CS 4332.501_Assignment_1
//Hunt the Wumpus
// 9/22/16 

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
        static int roomOfUser = -1;
        //rooms
        static int roomOfWumpus, roomFirstBats, roomSecondBats, roomFirstPits, roomSecondPits;
        static string[] roomDescription = new string[20];
        static int lowRV = 1;       //Low, High Room Value: Requirements for 1-20
        static int highRV = 20;     
        static int userNextRoom;
        //wumpus
        static int wumpusAwake = 0;
        static bool wampusAlive = true;
        static int wumpusIsAlive = 1;

        public class Square
        {
            public VertexPositionTexture[] Verticies;

            public Square()
            {
                Verticies = new VertexPositionTexture[4];
            }
        }

    /// The main entry point for the application.
    /// </summary>
    [STAThread]

        public static void StartGame()
        {
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

            Console.Out.WriteLine("\n\nWelcome to Hunt The Wumpus!");
            Console.Out.WriteLine("Press 'Y' to Play, or any other key to quit");

            userChoice = Console.ReadLine();

            if (userChoice == "Y" || userChoice == "y")
            {
                Console.Out.WriteLine("Game Rules:");
                Console.Out.WriteLine("In this game, you are in a map that has 20 rooms, each connected to three \nother rooms, with the topology of a dodecahedron.");
                Console.Out.WriteLine("As the player: You have 5 arrows and 1 life. ");
                Console.Out.WriteLine("On each turn, you can (M)ove or (S)hoot. You can only move to any of the \nthree adjacent rooms.");
                Console.Out.WriteLine("(M)oving: there are 5 hazards, so pay attention to your surroundings: ");
                Console.Out.WriteLine(" - 1 Wumpus");
                Console.Out.WriteLine("         - Wumpus starts out asleep. You will wake it by entering its room,\n            or by firing an arrow");
                Console.Out.WriteLine("         - If you wake the Wumpus, and have a 75% chance of moving each round");
                Console.Out.WriteLine("         - One awake, the Wumpus will kill you if you're in the same room, \n            so step lightly!");
                Console.Out.WriteLine(" - 2 Superbats");
                Console.Out.WriteLine("         - If you enter a room with Superbats, they will pick you up and \n          randomly put you in another room.");
                Console.Out.WriteLine("         - They will then fly off and perch in another room");
                Console.Out.WriteLine(" - 2 Pits");
                Console.Out.WriteLine("         - If you enter a room with a pit, you will fall in and immediately die.");
                Console.Out.WriteLine("(S)hooting: you will use a crooked arrow to fire through five rooms, but be \ncareful!");
                Console.Out.WriteLine("- If you incorrectly aim to a non-adjacent room, the arrow will veer off course");
                Console.Out.WriteLine("- If the arrow re-enters the room where you are standing, you will shoot \n  yourself, and die");
                Console.Out.WriteLine("- If you run out of arrows, the game will end");
                Console.Out.WriteLine("- The arrows are crooked, but not boomerangs. \nThey cannot make a path from room 1-2-1");

                playerLiving = 1;
                wampusAlive = true;
                GameLoop(roomOfUser, numOfArrows);
            }
            else
            {
                Console.Out.WriteLine("Goodbye!");
                playerLiving = -1;
                //playerLiving set at -1, drops out and ends game 
            }
        } //End Start Game 

        public static void GameLoop(int userRoom, int userArrows)
        {
                // hard coded game map
                int[,] map = new int[20, 3]
                    {
                    {1, 4, 7},    //0
                    {0, 2, 9},    //1
                    {1, 3, 11},   //2
                    {2, 4, 13},   //3
                    {0, 3, 5},    //4
                    {4, 6, 14},   //5
                    {5, 7, 16},   //6
                    {0, 6, 8},    //7
                    {7, 9, 17},   //8
                    {1, 8, 10},   //9
                    {9, 11, 18},  //10
                    {2, 10, 12},  //11
                    {11, 13, 19}, //12
                    {3, 12, 14},  //13
                    {5, 13, 15},  //14
                    {14, 16, 19}, //15
                    {6, 15, 17},  //16
                    {8, 16, 18},  //17
                    {10, 17, 19}, //18
                    {12, 15, 18}, //19
                    };

            Console.WriteLine("\n--------------------\nYou are in room: " + roomOfUser + "\nArrows left: "+ numOfArrows);

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
                
               // Console.WriteLine("Wumpus room: " + roomOfWumpus); //Wumpus tracker 
            }

            // Hazard Warnings
            if (map[roomOfUser, 0] == roomOfWumpus || map[roomOfUser, 1] == roomOfWumpus || map[roomOfUser, 2] == roomOfWumpus)
            {
                Console.WriteLine("HAZARD: I smell a Wumpus");
            }

            if (map[roomOfUser, 0] == roomFirstBats || map[roomOfUser, 1] == roomFirstBats || map[roomOfUser, 2] == roomFirstBats || map[roomOfUser, 0] == roomSecondBats || map[roomOfUser, 1] == roomSecondBats || map[roomOfUser, 2] == roomSecondBats)
            {
                Console.WriteLine("HAZARD: Bats nearby");
            }

            if (map[roomOfUser, 0] == roomFirstPits || map[roomOfUser, 1] == roomFirstPits || map[roomOfUser, 2] == roomFirstPits || map[roomOfUser, 0] == roomSecondPits || map[roomOfUser, 1] == roomSecondPits || map[roomOfUser, 2] == roomSecondPits)
            {
                Console.WriteLine("HAZARD: I feel a draft");
            }
            Console.WriteLine("You see paths before you to rooms {0}, {1}, and {2}", map[roomOfUser, 0], map[roomOfUser, 1], map[roomOfUser, 2]);
            Console.WriteLine("(M)ove or (S)hoot?");
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
                        roomOfUser = userNextRoom;
                        Console.WriteLine("You have moved to room " + roomOfUser); //success 

                        if (userNextRoom == roomOfWumpus)
                        {
                            if (wumpusAwake == 1)
                            {
                                Console.WriteLine("You got stung by Wumpus!");
                                Console.WriteLine("Game Over");
                                ExitGame();
                            }
                            else
                            {
                                Console.WriteLine("... Ooops! Bumped a Wumpus");
                                wumpusAwake = 1;
                                    Random rnd = new Random();  // first case: Wumpus has chance to move on same turn when woken
                                    int chance = rnd.Next(0, 100);

                                    if (chance >= 24) //probablility of 25% for Wumpus move
                                    {
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

                                GameLoop(userRoom, userArrows);
                            }
                        }
                        else if (userNextRoom == roomFirstBats || userNextRoom == roomSecondBats)
                        {
                            Console.WriteLine("Zap--Super Bat snatch! Elsewhereville for you!");
                            int batCountry = 0; //Only want to relocate bats that carried player
                            if (userNextRoom == roomFirstBats)
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

                                GameLoop(userRoom, userArrows);
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
                                } while (roomSecondBats == roomOfUser || roomSecondBats == roomOfWumpus || roomSecondBats == roomFirstBats || roomSecondBats == roomFirstPits || roomSecondBats == roomSecondPits);
                            }
                            GameLoop(userRoom, userArrows);
                        }

                        else if (userNextRoom == roomFirstPits || userNextRoom == roomSecondPits)
                        {
                            Console.WriteLine("YYYIIIIEEEE . . . fell in a pit\n\nYou get nothing! Good day sir!\n");
                            playerLiving = 0; 
                           ExitGame();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not Possible");
                        GameLoop(userRoom, userArrows);
                    }
                }
                else
                {
                    Console.WriteLine("Only Integers from 1 to 20");
                    GameLoop(userRoom, userArrows);
                }
            } // end move options 

            else if (userChoice == "s" || userChoice == "S")
            { 
                if(roomOfUser == roomOfWumpus)  //check - if user wakes up Wumpus but decides to shoot before moving, they die 
                {
                    Console.WriteLine("You fool! You're still in the same room with the Wumpus!");
                    Console.WriteLine("You got stung by Wumpus!");
                    Console.WriteLine("Game Over");
                    ExitGame();
                }

                Console.WriteLine("Enter 5 sequential rooms, pressing ENTER between each room number");
                //bool validFirePath = false; //not needed

                int target1 = 99;    //reset to avoid issues with setting fire path again 
                int target2 = 99;
                int target3 = 99;
                int target4 = 99;
                int target5 = 99;

                string c = Console.ReadLine();
                int value;

                if (int.TryParse(c, out value))
                {
                    target1 = value;
                }
                else
                {
                    Console.WriteLine("Please input a valid int");
                    GameLoop(roomOfUser, numOfArrows);
                }

                c = Console.ReadLine();
                if (int.TryParse(c, out value))
                {
                    target2 = value;
                }
                else
                {
                    Console.WriteLine("Please input a valid int");
                    GameLoop(roomOfUser, numOfArrows);
                }

                if (target1 == target2)
                    {
                        Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }
                c = Console.ReadLine();

                if (int.TryParse(c, out value))
                {
                    target3 = value;
                }
                else
                {
                    Console.WriteLine("Please input a valid int");
                    GameLoop(roomOfUser, numOfArrows);
                }

                if (target2 == target3 | target3 == target1)
                    {
                        Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }

                c = Console.ReadLine();

                if (int.TryParse(c, out value))
                {
                    target4 = value;
                }
                else
                {
                    Console.WriteLine("Please input a valid int");
                    GameLoop(roomOfUser, numOfArrows);
                }

                if (target4 == target3 | target4 == target2)
                    {
                        Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }
                c = Console.ReadLine();

                if (int.TryParse(c, out value))
                {
                    target5 = value;
                }
                else
                {
                    Console.WriteLine("Please input a valid int");
                    GameLoop(roomOfUser, numOfArrows);
                }

                if (target5 == target4 | target5 == target3)
                    {
                        Console.WriteLine("Arrows aren't that crooked - try another room");
                        GameLoop(roomOfUser, numOfArrows);
                    }
                    else
                    {
                    numOfArrows--;
                    }

                Console.WriteLine("You raise your bow to fire through rooms: " + target1 + "-" + target2 + "-" + target3 + "-" + target4 + "-" + target5);

                bool properAimCheck = false;
                int mymisfireRoom = 0;

                while (properAimCheck == false) {

                    if (target1 == map[roomOfUser, 0] || target1 == map[roomOfUser, 1] || target1 == map[roomOfUser, 2]) //at each Target room, check for valid input
                    {
                        if (target2 == map[target1, 0] || target2 == map[target1, 1] || target2 == map[target1, 2])
                        {
                            if (target3 == map[target2, 0] || target3 == map[target2, 1] || target3 == map[target2, 2])
                            {
                                if (target4 == map[target3, 0] || target4 == map[target3, 1] || target4 == map[target3, 2])
                                {
                                    if (target5 == map[target4, 0] || target5 == map[target4, 1] || target5 == map[target4, 2])
                                    {
                                        Console.WriteLine("Congratulations, you aimed correctly!");
                                        properAimCheck = true;  //sucessful aim - PASS 
                                        break;
                                    } else                           //for every incorrect target, must calculate new random path
                                    {                                //must use new random value to avoid paths like 1-2-1-2-1
                                          mymisfireRoom = target5;
                                        target5 = map[target4, 0];
                                        while (target5 == target4 | target5 == target3)
                                        {
                                            Random rnd = new Random();
                                            int arrowBend = rnd.Next(2, 3);
                                            target5 = map[target4, arrowBend];
                                        }
                                        Console.WriteLine("Your arrow veered off on the 5th room!");
                                        break;
                                    }
                                } else
                                {
                                    mymisfireRoom = target4;
                                    target4 = map[target3, 0];
                                    while(target4 == target3 | target4 == target2)
                                    {
                                        Random rnd = new Random();
                                        int arrowBend = rnd.Next(2, 3);
                                        target4 = map[target3, arrowBend];
                                    }
                                    target5 = map[target4, 0];
                                    while (target5 == target4 | target5 == target3)
                                    {
                                        Random rnd = new Random();
                                        int arrowBend = rnd.Next(2, 3);
                                        target5 = map[target4, arrowBend];
                                    }
                                    Console.WriteLine("Your arrow veered off on the 4th room!");
                                    break;
                                }
                            } else
                            {
                                mymisfireRoom = target3;
                                target3 = map[target2, 0];
                                while (target3 == target2 | target3 == target1)
                                {
                                    Random rnd = new Random();
                                    int arrowBend = rnd.Next(2, 3);
                                    target3 = map[target2, arrowBend];
                                }
                                target4 = map[target3, 0];
                                while (target4 == target3 | target4 == target2)
                                {
                                    Random rnd = new Random();
                                    int arrowBend = rnd.Next(2, 3);
                                    target4 = map[target3, arrowBend];
                                }
                                target5 = map[target4, 0];
                                while (target5 == target4 | target5 == target3)
                                {
                                    Random rnd = new Random();
                                    int arrowBend = rnd.Next(2, 3);
                                    target5 = map[target4, arrowBend];
                                }
                                Console.WriteLine("Your arrow veered off on the 3rd room!");
                                break;
                            }

                        } else
                        {
                            mymisfireRoom = target2;
                            target2 = map[target1, 0];
                            while (target2 == target1 | target2 == roomOfUser)
                            {
                                Random rnd = new Random();
                                int arrowBend = rnd.Next(2, 3);
                                target2 = map[target1, arrowBend];
                            }
                            target3 = map[target2, 0];
                            while (target3 == target2 | target3 == target1)
                            {
                                Random rnd = new Random();
                                int arrowBend = rnd.Next(2, 3);
                                target3 = map[target2, arrowBend];
                            }
                            target4 = map[target3, 0];
                            while (target4 == target3 | target4 == target2)
                            {
                                Random rnd = new Random();
                                int arrowBend = rnd.Next(2, 3);
                                target4 = map[target3, arrowBend];
                            }
                            target5 = map[target4, 0];
                            while (target5 == target4 | target5 == target3)
                            {
                                Random rnd = new Random();
                                int arrowBend = rnd.Next(2, 3);
                                target5 = map[target4, arrowBend];
                            }
                            Console.WriteLine("Your arrow veered off on the 2nd room!");
                            break;
                        }
                    } else
                    {
                        mymisfireRoom = target1;
                        target1 = map[roomOfUser, 0];
                        if (target1 == roomOfUser)
                        {
                            Random rnd = new Random();
                            int arrowBend = rnd.Next(2, 3);
                            target1 = map[roomOfUser, arrowBend];
                        }
                        target2 = map[target1, 0];
                        while (target2 == target1 | target2 == roomOfUser)
                        {
                            Random rnd = new Random();
                            int arrowBend = rnd.Next(2, 3);
                            target2 = map[target1, arrowBend];
                        }
                        target3 = map[target2, 0];
                        while (target3 == target2 | target3 == target1)
                        {
                            Random rnd = new Random();
                            int arrowBend = rnd.Next(2, 3);
                            target3 = map[target2, arrowBend];
                        }
                        target4 = map[target3, 0];
                        while (target4 == target3 | target4 == target2)
                        {
                            Random rnd = new Random();
                            int arrowBend = rnd.Next(2, 3);
                            target4 = map[target3, arrowBend];
                        }
                        target5 = map[target4, 0];
                        while (target5 == target4 | target5 == target3)
                        {
                            Random rnd = new Random();
                            int arrowBend = rnd.Next(2, 3);
                            target5 = map[target4, arrowBend];
                        }
                        Console.WriteLine("Your arrow veered off on the 1st room!");
                        break;
                    } //End fire checks 
                }   

                if (properAimCheck == false)
                {
                    Console.WriteLine("Oh no! The arrow went off course when you aimed at: " + mymisfireRoom);  //give veer path if off course 
                    Console.WriteLine("Your arrow took the path: " + target1 + "-" + target2 + "-" + target3 + "-" + target4 + "-" + target5);
                }

                if (target1 == roomOfWumpus || target2 == roomOfWumpus || target3 == roomOfWumpus || target4 == roomOfWumpus || target5 == roomOfWumpus)
                {
                    Console.WriteLine("Aha! You got the Wumpus!");
                    wampusAlive = false;
                }else if(wumpusAwake == 0)
                    {
                    Console.WriteLine("You woke the Wumpus!"); // any arrow fire will wake Wumpus, if miss
                    wumpusAwake = 1;
                }

                if (target1 == roomOfUser || target2 == roomOfUser || target3 == roomOfUser || target4 == roomOfUser || target5 == roomOfUser)
                {
                    Console.WriteLine("Ouch! Arrow got you!");
                    playerLiving = 0;
                }
                else
                {
                    
                }

                if(playerLiving == 0 && wampusAlive == false)
                {
                    Console.WriteLine("You shot the Wumpus... but you also shot yourself. Consider this a tie game!");
                    Console.WriteLine("Next time, shoot more carefully!");
                    ExitGame();
                }
                else if(playerLiving == 1 && wampusAlive == false){
                    Console.WriteLine("Congratulations! You won the game!");
                    Console.WriteLine("Hee hee hee - the Wumpus'll getcha next time!!\n\n");
                        ExitGame();
                }
                else if(playerLiving == 0 && wampusAlive == true)
                {
                    Console.WriteLine("Congratulations, you played yourself. \nGame over.");
                    ExitGame();
                }
                else
                {
                    Console.WriteLine("Missed!");
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

        //enter game 
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
