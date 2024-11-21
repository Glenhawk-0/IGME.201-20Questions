﻿using System;

namespace _20Questions
{

    // Defining TreeNode Class 
    class TreeNode
    {
        // Fields 
        // (with auto-properties so the code fits on the slides)
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public String Data { get; set; }

        // Create a disconnected tree node with specified data
        public TreeNode(String data)
        {
            Left = null;  // Left == no
            Right = null; //Right == yes
            Data = data;
        }
    }


    internal class Program
    {

        public static TreeNode root;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our 20 questions game! Think of an animal and we'll try to guess it!");

            QuestionTree();

            bool playAgain = true;

            while (playAgain)
            {
                // start game 
                PlayGame();
                Console.WriteLine("Would you like to play again? yes or no");
                string response = Console.ReadLine().ToLower();

                // check if user wants to play again 
                if (response != "yes")
                {
                    playAgain = false;
                }
            }

            Console.WriteLine("Thanks for playing!");
        }

        // simple starter tree with predefined question and answers 
        static void QuestionTree()
        {
            // Build the binary tree example from the slides
            // manually (this will be ugly, but we'll cover how
            // to write an "AddNode" for a tree next week).

            // Create the root
            root = new TreeNode("Does it Fly?");

            // Add the left side sub-tree to the root
            root.Left = new TreeNode("is it still a bird though?");
            root.Left.Left = new TreeNode("does it 'breath' water ");
            root.Left.Left.Right = new TreeNode("is it a fish");
            root.Left.Left.Right.Right = new TreeNode("Win Condition (im not going further than its a fish)");
            root.Left.Right = new TreeNode("is it a penguin?");

            // Add the right side sub-tree to the root
            root.Right = new TreeNode("does it poop on you");
            root.Left = new TreeNode("damn... i give up");

            root.Right.Right = new TreeNode("Does it live in the city");

            root.Right.Right.Right = new TreeNode("is it a pigeon");
            root.Right.Right.Left = new TreeNode("is it a seagull");

            // use this down here to simply view stuff.  comment out when your done
            /*
            Console.WriteLine(root.Data);
            Console.WriteLine(root.Left.Data);
            Console.WriteLine(root.Right.Data);
          
            Console.WriteLine(root.Right.Right.Data);/**/
            



        }




        // play game 
        static void PlayGame()
        {
            //thank you nick. the root stuff can now be called outside of the question tree class
            Console.WriteLine(root.Data);
            Console.WriteLine(root.Left.Data);
            Console.WriteLine(root.Right.Data);

            string response = Console.ReadLine().ToLower();

        }

        // learns the new information given by user to expand the tree with new questions and answers 
        static void LearnNewInfo(TreeNode incorrectNode)
        {
            //Guessed incorrectly
            Console.WriteLine("I guessed incorrectly! Please help me create a new question to help me guess your animal next time.");
            Console.WriteLine("What is the name of the correct animal you had in mind?");
            //store user inputted answer into new variable
            string newAnswer = Console.ReadLine();
            Console.WriteLine("Thank you. Now, input a yes/no question that distinguishes your animal from my guess.");
            //store user inputted question into new variable
            string newQuestion = Console.ReadLine();
            //What is the answer
            Console.WriteLine("Would the answer to this new question be 'yes' or 'no' for your animal?");
            string newPath = Console.ReadLine().ToLower();
            
            //create node
            TreeNode newNode = new TreeNode(newQuestion);
            TreeNode correctNode = new TreeNode(newAnswer);
            //add correctly based off y/n
            if (newPath == "yes")
            {
                //place to right
                newNode.Right = correctNode;
                newNode.Left = new TreeNode(incorrectNode.Data);
                incorrectNode.Data = newQuestion;
                incorrectNode.Left = newNode.Left;
                incorrectNode.Right = newNode.Right;
            } else
            {
                //place to left
                newNode.Left = correctNode;
                newNode.Right = new TreeNode(incorrectNode.Data);
                incorrectNode.Data = newQuestion;
                incorrectNode.Left = newNode.Left;
                incorrectNode.Right = newNode.Right;
            }
            Console.WriteLine("Thank you for helping me expand my data!");
        }
    }
}
