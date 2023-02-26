# **HW_TicTacToe_Task2_drusse14**
## *GitHub Repository*
[Tic_Tac_Toe](https://github.com/FuzzyPickles92/Tic_Tac_Toe.git)

## *Lessons Learned*
1. I learned how to use control structures like the switch statement, if statements, and loops to control the flow of the game and handle different scenarios.
2.I learned the importance of validating user input to prevent errors and handle unexpected input. An example of this would be using methods like `int.TryParse() ` and conditional statements to check if the input is valid.


## *Struggles*
1. Trying to find a simple way to consolidate all your Console.WriteLine calls into a single method. 
2. Updating the `StartGame` method to create a promptfor the user to choose who goes first.



## *Bugs*
### *Reported(Issues)*
1. To help with isolating volatile code away from stable code, you need to a QA an DEV branch.
2. The Interface does not show a symbol representation for Player 1 or Player.
3. int.Parse call does not account for Null Reference scenario.
4. Consolidate all your Console.WriteLine calls into a single method.

### *Resolved*
1. Resolved: I have created the QA and Dev branches.
2. Resolved: In this updated version, the player variable has been renamed to currentPlayer to make its purpose clearer. The player1Symbol and player2Symbol variables have been introduced to hold the symbols used by Player 1 and Player 2, respectively. These variables are displayed in the introductory messages at the beginning of the game and are used throughout the program to indicate which player's turn it is and which symbol each player is using.
3. Resolved: This line was removed from my program after re-work/update.
4. Unresolved: I am still thinking of a solution for this issue. I have a potential solution in mind, just need to test it first.