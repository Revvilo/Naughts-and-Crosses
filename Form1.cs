using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Naughts_and_Crosses
{
    public partial class NaughtsAndCrossesWindow : Form
    {

        public static string VERSION = "BETA v1.0";
        public NaughtsAndCrossesWindow()
        {
            InitializeComponent();
        }



        PictureBox[] tiles = new PictureBox[8];

        // The Array for the possible wins
        Control[][] winStates = new Control[8][];

        //Creates the random number generator and the player variable (E Means error)
        Random rand = new Random();
        char player = 'E';
        bool winState = false;
        bool draw = false;


        /// <summary>
        /// Runs the load checks, randomises the player and displays it on startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Naughts and Crosses " + VERSION;
            restartButton.Visible = false;

            winStates[0] = new PictureBox[] { boxTopLeft, boxTopMid, boxTopRight };    //   _
            winStates[1] = new PictureBox[] { boxMidLeft, boxMidMid, boxMidRight };    //   _
            winStates[2] = new PictureBox[] { boxBotLeft, boxBotMid, boxBotRight };    //   _
            winStates[3] = new PictureBox[] { boxTopLeft, boxMidLeft, boxBotLeft };    //   |..
            winStates[4] = new PictureBox[] { boxTopMid, boxMidMid, boxBotMid };       //   .|.
            winStates[5] = new PictureBox[] { boxTopRight, boxMidRight, boxBotRight }; //   ..|
            winStates[6] = new PictureBox[] { boxTopLeft, boxMidMid, boxBotRight };    //   \
            winStates[7] = new PictureBox[] { boxTopRight, boxMidMid, boxBotLeft };    //   /

            int randomNum = rand.Next(2);

            if (randomNum == 0)
            {
                player = 'O';
            }
            else if(randomNum == 1)
            {
                player = 'X';
            }

            playerDisplay.Text = "Player " + player + "'s turn";
        }


        /// <summary>
        /// Flips to the other player
        /// </summary>
        /// <returns></returns>
        public void flipPlayer()
        {
            if (player == 'X')
            {
                player = 'O';
            }
            else
            {
                player = 'X';
            }
        }


        /// <summary>
        /// Depending on the current player, place a Naught or Cross on the clicked picture box (Requires clicked picture box's code name)
        /// </summary>
        /// <param name="boxLoc"></param>
        public void placeImage(PictureBox boxLoc)
        {
            if (player == 'X')
            {
                boxLoc.Image = Naughts_and_Crosses.Properties.Resources.Cross;
                boxLoc.Tag = "Cross";

            }
            else if (player == 'O')
            {
                boxLoc.Image = Naughts_and_Crosses.Properties.Resources.Naught;
                boxLoc.Tag = "Naught";

            }
            boxLoc.Enabled = false;

            checkWin();

            if(winState)
            {
                playerDisplay.Text = "Game Over - " + player + " WINS!";
                disableTiles();
                restartButton.Visible = true;
                
            }
            else if (draw)
            {
                playerDisplay.Text = "Game Over - DRAW";
                disableTiles();
                restartButton.Visible = true;

            }
            else
            {
                flipPlayer();
                playerDisplay.Text = "Player " + player + "'s turn";

            }

        }
        


        /// <summary>
        /// Checks if the current arrangement of Naughts and Crosses equals a win or game over
        /// </summary>
        /// <returns></returns>
        public bool checkWin()
        {

            // Runs through every possible arrangement of tiles to check for the win
            foreach(Array state in winStates)
            {

                //=-=-=-=-= Cross checker

                foreach(PictureBox box in state)
                {
                    if((string)box.Tag == "Cross")
                    {
                        winState = true;
                    }
                    else
                    {
                        winState = false;
                        break;
                    }
                }

                if (winState == true)
                {
                    return winState;
                }


                
                //=-=-=-=-= Naught checker

                foreach (PictureBox box in state)
                {
                    if((string)box.Tag == "Naught")
                    {
                        winState = true;
                    }
                    else
                    {
                        winState = false;
                        break;
                    }
                }

                if (winState == true)
                {
                    return winState;
                }

            } // End foreach possible win state



            // And if no one has won, check if the board is full
            draw = true;

            foreach(Control c in Controls)
            {
                if(c is PictureBox)
                {
                    if (c.Enabled == true)
                    {
                        draw = false;
                        break;
                    }
                }

            } // End foreach control

            return draw;
        }

        /// <summary>
        /// Disables all the PictureBoxes
        /// </summary>
        public void disableTiles()
        {
            foreach(Control c in Controls)
            {
                if(c is PictureBox)
                {
                    c.Enabled = false;
                }
            }

        }


        /// <summary>
        /// Enables all the PictureBoxes
        /// </summary>
        public void enableTiles()
        {
            foreach (Control c in Controls)
            {
                if (c is PictureBox)
                {
                    c.Enabled = true;
                }
            }
        }


        /// <summary>
        /// Removes all Naughts and Crosses from the board
        /// </summary>
        public void clearTiles()
        {
            boxTopLeft.Image = null;
            boxTopMid.Image = null;
            boxTopRight.Image = null;
            boxMidLeft.Image = null;
            boxMidMid.Image = null;
            boxMidRight.Image = null;
            boxBotLeft.Image = null;
            boxBotMid.Image = null;
            boxBotRight.Image = null;

            boxTopLeft.Tag = null;
            boxTopMid.Tag = null;
            boxTopRight.Tag = null;
            boxMidLeft.Tag = null;
            boxMidMid.Tag = null;
            boxMidRight.Tag = null;
            boxBotLeft.Tag = null;
            boxBotMid.Tag = null;
            boxBotRight.Tag = null;
        }


        /// <summary>
        /// Restarts the game
        /// </summary>
        public void restart()
        {
            restartButton.Visible = false;
            clearTiles();
            enableTiles();
            Form1_Load(null, null);

        }


        //All the events that get triggered by clicking the picture boxes
        #region clickEvents

        private void boxTopLeft_Click(object sender, EventArgs e)
        {
            placeImage(boxTopLeft);

        }

        private void boxTopMid_Click(object sender, EventArgs e)
        {
            placeImage(boxTopMid);

        }

        private void boxTopRight_Click(object sender, EventArgs e)
        {
            placeImage(boxTopRight);


        }

        private void boxMidLeft_Click(object sender, EventArgs e)
        {
            placeImage(boxMidLeft);

        }

        private void boxMidMid_Click(object sender, EventArgs e)
        {
            placeImage(boxMidMid);

        }

        private void boxMidRight_Click(object sender, EventArgs e)
        {
            placeImage(boxMidRight);

        }

        private void boxBotLeft_Click(object sender, EventArgs e)
        {
            placeImage(boxBotLeft);

        }

        private void boxBotMid_Click(object sender, EventArgs e)
        {
            placeImage(boxBotMid);

        }

        private void boxBotRight_Click(object sender, EventArgs e)
        {
            placeImage(boxBotRight);

        }

        #endregion

        private void restartButton_Click(object sender, EventArgs e)
        {
            restart();
        }
    }
}
