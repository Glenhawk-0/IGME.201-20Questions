using System;

namespace _20Questions
{

    // Defining TreeNode Class 
    class TreeNode
    {
        public string QuestionOrAnswer { get; set; }
        public TreeNode No { get; set; }
        public TreeNode Yes { get; set; }

        // Create a disconnected tree node with specified data
        public TreeNode(String questionOrAnswer)
        {
            QuestionOrAnswer = questionOrAnswer;
            No = null;
            Yes = null;
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
                PlayGame(root);
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
            // Create the root
            root = new TreeNode("Does it Fly?");

            // Add the left side sub-tree to the root
            root.No = new TreeNode("is it still a bird though?");
            root.No.No = new TreeNode("does it 'breath' water ");
            root.No.No.Yes = new TreeNode("fish");
            //root.No.No.Yes.Yes = new TreeNode("");
            root.No.Yes = new TreeNode("penguin?");

            // Add the right side sub-tree to the root
            root.Yes = new TreeNode("does it poop on you");
            root.Yes.No = new TreeNode("... um.. no way.. this animal doesnt exist");

            root.Yes.Yes = new TreeNode("Does it live in the city");

            root.Yes.Yes.Yes = new TreeNode("pigeon");
            root.Yes.Yes.No = new TreeNode("seagull");

            root.Yes.Yes.No.No = new TreeNode("its not a seagull!? darn, i give up");

            // use this down here to simply view stuff.  comment out when your done
            /*
            Console.WriteLine(root.QuestionOrAnswer);
            /**/

            // Print the tree starting at the root



        }




        // play game 
        static void PlayGame(TreeNode node)
        {
            // The game plays through recursion
            if (node.Yes == null && node.No == null)
            {
                // If we reach an answer (leaf node), we guess it
                Console.WriteLine("I think it's a " + node.QuestionOrAnswer + "!");
                Console.WriteLine("Was I correct? (yes/no)");
                string response = Console.ReadLine().ToLower();

                if (response == "no")
                {
                    // If the guess was incorrect, ask the user for a new question to improve the tree
                    Console.WriteLine("I guessed wrong! Please tell me a question that would differentiate a " +
                                      node.QuestionOrAnswer + " from your answer.");
                    string newQuestion = Console.ReadLine();

                    Console.WriteLine("What would the answer be for your item? (yes/no)");
                    string answer = Console.ReadLine().ToLower();

                    Console.WriteLine("What is the correct answer? (e.g., 'dog')");
                    string newAnswer = Console.ReadLine();

                    // Create a new node for the new question and answers
                    TreeNode newQuestionNode = new TreeNode(newQuestion);
                    if (answer == "yes")
                    {
                        newQuestionNode.Yes = new TreeNode(newAnswer);
                        newQuestionNode.No = node;
                    }
                    else
                    {
                        newQuestionNode.No = new TreeNode(newAnswer);
                        newQuestionNode.Yes = node;
                    }

                    // Replace the old guess with the new question node
                    node = newQuestionNode;
                }
                else
                {
                    // If the guess was correct, end the game
                    Console.WriteLine("Yay! I guessed it right.");
                }
            }
            else
            {
                // If the current node is a question, ask it
                Console.WriteLine(node.QuestionOrAnswer + " (yes/no)");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    PlayGame(node.Yes); // Move to the "yes" branch
                }
                else if (response == "no")
                {
                    PlayGame(node.No); // Move to the "no" branch
                }
                else
                {
                    Console.WriteLine("Please answer with 'yes' or 'no'.");
                    PlayGame(node); // Retry the same question
                }
            }
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
                newNode.Yes = correctNode;
                newNode.No = incorrectNode;
            } else
            {
                //place to left
                newNode.No = correctNode;
                newNode.Yes = incorrectNode;
            }
            incorrectNode.QuestionOrAnswer = newQuestion;
            incorrectNode.Yes = newNode.Yes;
            incorrectNode.No = newNode.No;
            Console.WriteLine("Thank you for helping me expand my data!");
        }
    }
}