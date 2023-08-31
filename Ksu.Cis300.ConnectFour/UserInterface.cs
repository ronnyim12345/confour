/* UserInterface.cs
 * Author: Ronny Im
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.ConnectFour
{
    public partial class UserInterface : Form
    {


        /// FIELD ///

        /// <summary>
        /// Makes the game.
        /// </summary>
        private Game _game = new Game();

        /// <summary>
        /// Keeps track of how many buttons are disabled.
        /// </summary>
        private int _disableAllButtons = 0;


        /// CONSTRUCTOR ///

        /// <summary>
        /// UserInterface constructor
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();

            MakeUI();
        }





        /// METHODS/EVENT HANDLERS ///

        /// <summary>
        /// Loads buttons and slots on board.
        /// </summary>
        private void MakeUI()
        {
            for(int i=0; i<7; i++)
            {


                /// MAKE BUTTON ///
                
                Button theButton = new Button();

                theButton.Name = Game.COLUMN_LABELS[i].ToString();
                theButton.Text = Game.COLUMN_LABELS[i].ToString();
                theButton.Width = 45;
                theButton.Height = 20;



                /// MAKE PADDING ///
                
                Padding thePadding = new Padding();

                thePadding.Left = 5;
                thePadding.Right = 5;
                thePadding.Top = 5;
                thePadding.Bottom = 5;

                theButton.Margin = thePadding;

                theButton.Click += uxPlaceButtonClick;

                uxPlaceButtonContainer.Controls.Add(theButton);



                /// MAKE INDIVIDUAL COLUMN ///  


                for (int j=Game.COLUMN_SIZE; j>0; j--)
                {
                    Label theLabel = new Label();

                    theLabel.Width = 45;
                    theLabel.Height = 45;
                    theLabel.Margin = thePadding;
                    theLabel.BackColor = Color.White;
                    theLabel.Name = Game.COLUMN_LABELS[i] + j.ToString();


                    uxBoardContainer.Controls.Add(theLabel);



                }

            }
            


            /// MAKE LABEL RED ///

            uxTurnLabel.Text = "Red";
            uxTurnLabel.BackColor = Color.Red;


        }



        /// <summary>
        /// Event handler for every button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPlaceButtonClick(object sender, EventArgs e)
        {
            /// GETS INFO FROM THE BUTTON ///

            Button theButton = new Button();

            theButton = (Button)sender;

            string column = theButton.Text.ToString();

            



            /// GETS WHOSE TURN IT IS ///


            Color color = _game.PlayersColors[Convert.ToInt32(_game.Turn)];





            /// CALLS "PLACE NEW PIECE" METHOD ///

            _game.PlaceNewPiece(color, column, out int row);





            /// CALLS "SET COLOR" METHOD ///

            SetColor(column + row.ToString(), color);



            /// SWITCH PLAYER'S TURN ///

            if(_game.Turn == PlayersTurn.Red)
            {
                _game.Turn = PlayersTurn.Black;
                uxTurnLabel.BackColor = Color.Black;
            }
            else
            {
                _game.Turn = PlayersTurn.Red;
                uxTurnLabel.BackColor = Color.Red;
            }




            /// CALLS "CHECK WIN" METHOD ///
            
            _game.FindCell(Convert.ToChar(column), row, out DoubleLinkedListCell<GamePiece> found);


            bool won = _game.CheckWin(found);


            if (won)
            {
                if (_game.Turn == PlayersTurn.Black)
                {
                    MessageBox.Show("Red Wins!");
                }
                else
                {
                    MessageBox.Show("Black Wins!");
                }
                Environment.Exit(0);
            }



            /// DISABLE BUTTONS ///

            _game.FindCell(Convert.ToChar(column), row, out DoubleLinkedListCell<GamePiece> found2);


            while (_game.Column.Data.Prev != null)         // GOES ALL THE WAY TO BOTTOM
            {
                _game.Column.Data = _game.Column.Data.Prev;
            }

            int counter = 1;



            while (_game.Column.Data.Next != null)         // GOES ALL THE WAY UP
            {
                _game.Column.Data = _game.Column.Data.Next;
                counter++;
            }

            if(counter == 6)                               // DISABLES THAT BUTTON
            {
                Control[] theControl = new Control[1];

                theControl = uxPlaceButtonContainer.Controls.Find(column, true);

                theControl[0].Enabled = false;

                _disableAllButtons++;
            }




            /// EXIT IF BOARD IS FULL ///

            if(_disableAllButtons == 7)
            {
                MessageBox.Show("Draw!");
                Environment.Exit(0);
            }


        }


        /// <summary>
        /// Sets color to that id.
        /// </summary>
        /// <param name="id"> id. </param>
        /// <param name="color"> color. </param>
        private void SetColor(string id, Color color)
        {
            Control[] theControl = new Control[20];


            theControl = uxBoardContainer.Controls.Find(id, true);

            
            theControl[0].BackColor = color;


        }
    }
}
