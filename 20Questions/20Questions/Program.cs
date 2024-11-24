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
            string filePath = "../../../savedTree.txt"; // RELATIVE PATH TO THE TREE FILE
            Console.WriteLine("Tree is being saved to: " + Path.GetFullPath(filePath));

            Console.WriteLine("Welcome to our 20 questions game! Think of an animal and we'll try to guess it!");

            // CHECK IF THE FILE EXISTS
            if (File.Exists(filePath))
            {
                Console.WriteLine("Saved tree found. Loading tree...");
                root = loadTree(filePath);
            }
            else
            {
                Console.WriteLine("No saved tree found. Creating a new tree...");
                QuestionTreeDefault(); // BUILD DEFAULT TREE IF FILE DOESN'T EXIST
            }

            bool playAgain = true;

            while (playAgain)
            {
                // START THE GAME
                PlayGame(root);
                Console.WriteLine("Would you like to play again? yes or no");
                string response = Console.ReadLine().ToLower();

                // CHECK IF USER WANTS TO PLAY AGAIN
                if (response != "yes")
                {
                    playAgain = false;
                }
            }

            // SAVE TREE TO FILE BEFORE EXITING
            saveTree(root, filePath);
            Console.WriteLine("Thanks for playing!");
        }


        // simple starter tree with predefined question and answers `
        // SAVE FUNCTIONS


        //function to generate a string of each deth
        static string treeStringBuilder(TreeNode node, int depth)
            {
                if (node == null) return "";

                // CREATE A LINE USING ''*'' AS A DEPTH INDICATOR                         // adds crossplatform support instead of /n :)
                string currentLine = new string('*', depth) + node.QuestionOrAnswer + Environment.NewLine;

                // Recursively generate the string for the Yes and No branches
                string yesBranch = treeStringBuilder(node.Yes, depth + 1);
                string noBranch = treeStringBuilder(node.No, depth + 1);

                return currentLine + yesBranch + noBranch;
            }
            static void saveTree(TreeNode root, string filePath)
            {
                string treeString = treeStringBuilder(root, 0);
                File.WriteAllText(filePath, treeString);
                Console.WriteLine("Tree has been saved successfully!");
            }

        // LOAD FUNCTIONS
        static TreeNode loadLine(string[] lines, ref int index, int depth)
        {
            if (index >= lines.Length) return null;

            // Count the number of '*' to determine the depth
            int currentDepth = lines[index].TakeWhile(c => c == '*').Count();

            // If the depth doesn't match, backtrack
            if (currentDepth != depth) return null;

            string questionOrAnswer = lines[index].Substring(currentDepth).Trim();
            TreeNode node = new TreeNode(questionOrAnswer);
            index++;
            node.Yes = loadLine(lines, ref index, depth + 1);
            node.No = loadLine(lines, ref index, depth + 1);

            return node;
        }
        static TreeNode loadTree(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No saved tree found. Starting fresh.");
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);
            int index = 0;

            return loadLine(lines, ref index, 0);
        }
        static void QuestionTreeDefault()
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

            // use this down here to simply view stuff.  comment out when your done

            Console.WriteLine(root.QuestionOrAnswer);


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

                   
                    LearnNewInfo(node);
                    /*Old mockup of New answer code
                    // If the guess was incorrect, ask the user for a new question to improve the tree

                    /*Console.WriteLine("I guessed wrong! Please tell me a question that would differentiate a " +
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

                    /*Old new answer*/
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
            // ASK USER FOR NEW INFORMATION TO UPDATE THE TREE
            Console.WriteLine("I guessed incorrectly! Please help me create a new question to help me guess your animal next time.");
            Console.WriteLine("What is the name of the correct animal you had in mind?");
            string newAnswer = Console.ReadLine();

            Console.WriteLine("Thank you. Now, input a yes/no question that distinguishes your animal from my guess.");
            string newQuestion = Console.ReadLine();

            Console.WriteLine("Would the answer to this new question be 'yes' or 'no' for your animal?");
            string newPath = Console.ReadLine().ToLower();

            // CREATE A NEW QUESTION NODE
            TreeNode newQuestionNode = new TreeNode(newQuestion);

            // CREATE A NODE FOR THE CORRECT ANSWER (USER'S INPUT)
            TreeNode correctAnswerNode = new TreeNode(newAnswer);

            // DETERMINE WHERE THE USER'S ANIMAL BELONGS IN THE TREE
            if (newPath == "yes")
            {
                newQuestionNode.Yes = correctAnswerNode; // USER'S ANIMAL IS THE "YES" BRANCH
                newQuestionNode.No = new TreeNode(incorrectNode.QuestionOrAnswer)
                {
                    Yes = incorrectNode.Yes,
                    No = incorrectNode.No
                };
            }
            else
            {
                newQuestionNode.No = correctAnswerNode; // USER'S ANIMAL IS THE "NO" BRANCH
                newQuestionNode.Yes = new TreeNode(incorrectNode.QuestionOrAnswer)
                {
                    Yes = incorrectNode.Yes,
                    No = incorrectNode.No
                };
            }

            // REPLACE THE INCORRECT NODE'S DATA WITH THE NEW QUESTION NODE
            incorrectNode.QuestionOrAnswer = newQuestionNode.QuestionOrAnswer;
            incorrectNode.Yes = newQuestionNode.Yes;
            incorrectNode.No = newQuestionNode.No;

            Console.WriteLine("Thank you for helping me expand my data!");
        }


    }
}