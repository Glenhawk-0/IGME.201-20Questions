using System;

namespace _20Questions
{

    // Defining TreeNode Class 
    public class TreeNode
    {
        public string Data { get; set; }
        public TreeNode Left { get; set; } // left for "no" response 
        public TreeNode Right { get; set; } // right for "yes" response 

        public TreeNode(string data)
        {
            Left = null;
            Right = null;
            Data = data;
        }
    }


    internal class Program
    {

        static TreeNode root;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our 20 questions game! Think of something and we'll try to guess it!");

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
            // questions and answers from Nick's example 
            root = new TreeNode("Is it an animal?");
            root.Right = new TreeNode("Does it have four legs?");
            root.Left = new TreeNode("Is it a vehicle?");

            // right side of the sub-tree
            root.Right.Right = new TreeNode("Is it a pet?");
            root.Right.Right.Right = new TreeNode("It's a dog!");
            root.Right.Right.Left = new TreeNode("It's a horse!");

            root.Right.Left = new TreeNode("Does it fly?");
            root.Right.Left.Right = new TreeNode("It's a bird!");
            root.Right.Left.Left = new TreeNode("It's a fish!");

            // left side of the sub-tree 
            root.Left.Right = new TreeNode("Is it a car?");
            root.Left.Left = new TreeNode("Is it a building?");
        }

        // play game 
        static void PlayGame()
        {

        }

        // learns the new information given by user to expand the tree with new questions and answers 
        static void LearnNewInfo()
        {

        }
    }
}
