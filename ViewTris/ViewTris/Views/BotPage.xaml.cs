namespace ViewTris.Views;

public partial class BotPage : ContentPage
{
    struct Move
    {
        public int row;
        public int col;
    }
    const string Player = "X";
    const string Bot = "O";
    string[,] griglia = new string[3, 3]
    {
        {null,null,null },
        {null,null,null},
        {null,null,null }
    };
    Button[,] buttonGriglia;
    public BotPage()
    {
        InitializeComponent();
        buttonGriglia = new Button[3, 3]
        {
            {c00,c01,c02 },
            {c10,c11,c12 },
            {c20,c21,c22 }
        };
    }

    private void CellClicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (btn.Text != null)
            return;
        btn.Text = Player;
        griglia = new string[3, 3]
        {
            { c00.Text,c01.Text,c02.Text},
            { c10.Text,c11.Text,c12.Text},
            { c20.Text,c21.Text,c22.Text}
        };
        Move bestMove = findBestMove(griglia);
        buttonGriglia[bestMove.row, bestMove.col].Text = Bot;
        griglia = new string[3, 3]
        {
            { c00.Text,c01.Text,c02.Text},
            { c10.Text,c11.Text,c12.Text},
            { c20.Text,c21.Text,c22.Text}
        };
    }
    bool isMovesLeft(string[,] griglia)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (griglia[i, j] == null)
                    return true;
        return false;
    }
    int evaluate(string[,] griglia)
    {
        // Checking for Rows for X or O victory. 
        for (int row = 0; row < 3; row++)
        {
            if (griglia[row, 0] == griglia[row, 1] &&
                griglia[row, 1] == griglia[row, 2])
            {
                if (griglia[row, 0] == Player)
                    return +10000;
                else if (griglia[row, 0] == Bot)
                    return -10000;
            }
        }

        // Checking for Columns for X or O victory. 
        for (int col = 0; col < 3; col++)
        {
            if (griglia[0, col] == griglia[1, col] &&
                griglia[1, col] == griglia[2, col])
            {
                if (griglia[0, col] == Player)
                    return +10000;

                else if (griglia[0, col] == Bot)
                    return -10000;
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (griglia[0, 0] == griglia[1, 1] && griglia[1, 1] == griglia[2, 2])
        {
            if (griglia[0, 0] == Player)
                return +10000;
            else if (griglia[0, 0] == Bot)
                return -10000;
        }

        if (griglia[0, 2] == griglia[1, 1] && griglia[1, 1] == griglia[2, 0])
        {
            if (griglia[0, 2] == Player)
                return +10000;
            else if (griglia[0, 2] == Bot)
                return -10000;
        }

        // Else if none of them have won then return 0 
        return 0;
    }


    int minimax(string[,] griglia, int depth, bool isMax)
    {
        int score = evaluate(griglia);

        // If Maximizer has won the game return his/her 
        // evaluated score 
        if (score == 10000)
            return score;

        // If Minimizer has won the game return his/her 
        // evaluated score 
        if (score == -10000)
            return score;

        // If there are no more moves and no winner then 
        // it is a tie 
        if (isMovesLeft(griglia) == false)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (griglia[i, j] == null)
                    {
                        // Make the move 
                        griglia[i, j] = Player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best,
                            minimax(griglia, depth + 1, !isMax));

                        // Undo the move 
                        griglia[i, j] = null;
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (griglia[i, j] == null)
                    {
                        // Make the move 
                        griglia[i, j] = Bot;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best,
                               minimax(griglia, depth + 1, !isMax));

                        // Undo the move 
                        griglia[i, j] = null;
                    }
                }
            }
            return best;
        }
    }
    Move findBestMove(string[,] griglia)
    {
        int bestVal = -1000;
        Move bestMove;
        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function for 
        // all empty cells. And return the cell with optimal 
        // value. 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty 
                if (griglia[i, j] == null)
                {
                    // Make the move 
                    griglia[i, j] = Player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(griglia, 0, false);

                    // Undo the move 
                    griglia[i, j] = null;

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }
        return bestMove;
    }
}