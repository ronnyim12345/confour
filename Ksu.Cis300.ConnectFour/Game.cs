/* Game.cs
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
    /// <summary>
    /// Types of colors.
    /// </summary>
    public enum PlayersTurn
    {
        Red,
        Black
    }
    

    public class Game
    {


        /// FIELDS ///

        /// <summary>
        /// Size of columns.
        /// </summary>
        public const int COLUMN_SIZE = 6;


        /// <summary>
        /// Labels of columns.
        /// </summary>
        public const string COLUMN_LABELS = "ABCDEFG";



        /// <summary>
        /// Array that carries colors.
        /// </summary>
        public readonly Color[] PlayersColors = new Color[2] { Color.Red, Color.Black };





        /// PROPERTIES ///

      

        /// <summary>
        /// Keeps track of current Column.
        /// </summary>
        public DoubleLinkedListCell<DoubleLinkedListCell<GamePiece>> Column { get; set; } = null;


        /// <summary>
        /// The Players Turn. 
        /// </summary>
        public PlayersTurn Turn { get; set; } = PlayersTurn.Red;







        /// CONSTRUCTOR ///

        /// <summary>
        /// Loads the columns in their right place.
        /// </summary>
        public Game()
        {
            DoubleLinkedListCell<DoubleLinkedListCell<GamePiece>> pointerCell = new DoubleLinkedListCell<DoubleLinkedListCell<GamePiece>>(null);

            pointerCell.Id = "A";


            for (int i = 1; i < 7; i++)
            {

                DoubleLinkedListCell<DoubleLinkedListCell<GamePiece>> newCell = new DoubleLinkedListCell<DoubleLinkedListCell<GamePiece>>(null);

                newCell.Id = COLUMN_LABELS[i].ToString();


                pointerCell.Next = newCell;
                newCell.Prev = pointerCell;


                pointerCell = newCell;
            }


            Column = pointerCell;


        }






        /// METHODS ///
       



        /// <summary>
        /// Sets Column to the cell it needs to be.
        /// </summary>
        /// <param name="columnId"> Column ID. </param>
        public void ChangeColumn(string columnId)
        {
            while(Column.Prev != null)
            {
                Column = Column.Prev;
            }


            while(Column.Id != columnId)
            {
                Column = Column.Next;
            }




        }


        /// <summary>
        /// Places a new piece 
        /// </summary>
        /// <param name="color"> The color. </param>
        /// <param name="col"> The column. </param>
        /// <param name="row"> The row. </param>
        /// <returns> The Id. </returns>
        public string PlaceNewPiece(Color color, string col, out int row)
        {
            ChangeColumn(col);


            if(Column.Data == null)
            {
                GamePiece newPiece = new GamePiece(color, 1, Convert.ToChar(Column.Id.ToString()));



                DoubleLinkedListCell<GamePiece> newCell = new DoubleLinkedListCell<GamePiece>(null);


                newCell.Data = newPiece;
                newCell.Id = Column.Id + "1";


                Column.Data = newCell;

                row = 1;


            }
            else
            {
                while(Column.Data.Next != null)
                {
                    Column.Data = Column.Data.Next;
                }

                row = Convert.ToInt32(Column.Data.Id[1]) - 47;

                GamePiece newPiece = new GamePiece(color, row, Convert.ToChar(Column.Id[0].ToString()));



                DoubleLinkedListCell<GamePiece> newCell = new DoubleLinkedListCell<GamePiece>(null);


                newCell.Data = newPiece;
                newCell.Id = Column.Id + row.ToString();


                Column.Data.Next = newCell;
                newCell.Prev = Column.Data;



                Column.Data = newCell;


            }


            return Column.Id + row.ToString();
        }



        /// <summary>
        /// Return true/false if there is a cell from Column that matches the given row/col.
        /// </summary>
        /// <param name="col"> The col. </param>
        /// <param name="row"> The row. </param>
        /// <param name="found"> The cell. </param>
        /// <returns> Whether it was found. </returns>
        public bool FindCell(char col, int row, out DoubleLinkedListCell<GamePiece> found)
        {
            /// CHECKS IF THE COL ITS ASKING FOR IS REAL ///

            if(col < 'A' || col > 'G')
            {
                found = null;
                return false;
            }

            ChangeColumn(col.ToString());





            /// PERHAPS EMPTY ///
            

            if (Column.Data == null)
            {
                found = null;
                return false;
            }



            /// LOOKS FOR IT ///



            while(Column.Data.Prev != null)
            {
                Column.Data = Column.Data.Prev;
            }



            if (Column.Data.Id[1].ToString() == row.ToString())
            {
                found = Column.Data;
                return true;
            }


            while (Column.Data.Next != null)
            {
                Column.Data = Column.Data.Next;

                if (Column.Data.Id[1].ToString() == row.ToString())
                {
                    found = Column.Data;
                    return true;
                }

            }



            /// PERHAPS ONLY ONE ROW ///


            if (Column.Data.Id[1].ToString() == row.ToString())
            {
                found = Column.Data;
                return true;
            }



            /// DIDNT FIND IT ///


            found = null;
            return false;
        }



        /// <summary>
        /// Helper function for CheckWin. Checks if won.
        /// </summary>
        /// <param name="row"> row. </param>
        /// <param name="col"> col. </param>
        /// <param name="rowDirection"> direction of row. </param>
        /// <returns> Whether he won. </returns>
        private bool Check(int row, char col, int rowDirection, int colDirection, Color color)
        {
            bool exists = false;

            int stopper = 1;           // IF STOPPER = 4, HE WON


            ChangeColumn(col.ToString());




            /// GO LEFT ///

            if (rowDirection == 0 && colDirection == 1)    
            {
                if (Column.Prev != null)
                {
                    do
                    {
                        Column = Column.Prev;
                        exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found);
                        if (exists && found.Data.PieceColor == color)
                        {
                            stopper++;
                        }
                        else
                        {
                            break;
                        }
                    } while (Column.Prev != null && stopper != 4);

                    if (stopper == 4)
                    {
                        return true;
                    }
                }


                return Check(row, Convert.ToChar(Column.Id), 2, 1, color);


            }





            /// GO RIGHT ///

            if (rowDirection == 2 && colDirection == 1)              
            {
                stopper = 0;


                exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found2);
                if (exists && found2.Data.PieceColor == color)
                {
                    stopper++;
                }




                if (Column.Next != null)
                {
                    do
                    {
                        Column = Column.Next;
                        exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found);
                        if (exists && found.Data.PieceColor == color)
                        {
                            stopper++;
                        }
                        else
                        {
                            break;
                        }
                    } while (Column.Next != null && stopper != 4);

                    if (stopper == 4)
                    {
                        return true;
                    }
                }


                return false;


            }




            /// GO DOWN, THEN UP ///

            if (rowDirection == 1 && colDirection == 0)
            {

                while (Column.Data.Prev != null)         // GOES ALL THE WAY TO BOTTOM
                {
                    Column.Data = Column.Data.Prev;
                }


                if (Column.Data.Next == null)             // RETURNS FALSE IF THERES ONLY 1 ROW
                {
                    return false;
                }

                stopper = 0;

                if (Column.Data.Data.PieceColor == color)
                {
                    stopper++;
                }


                do
                {
                    Column.Data = Column.Data.Next;

                    if (Column.Data.Data.PieceColor == color)
                    {
                        stopper++;
                    }
                    else
                    {
                        stopper = 0;
                    }


                } while (Column.Data.Next != null && stopper != 4);



                if (stopper == 4)    // HE WON
                {
                    return true;
                }


                /// DIDNT WIN ////

                return false;

            }






            /// DIAG DOWNwARD ///

            if (rowDirection == 2 && colDirection == 0)
            {
                if (Column.Next != null)
                {
                    do
                    {
                        Column = Column.Next;
                        row--;
                        exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found);
                        if (exists && found.Data.PieceColor == color)
                        {
                            stopper++;
                        }
                        else
                        {
                            break;
                        }
                    } while (Column.Next != null && stopper != 4);

                    if (stopper == 4)
                    {
                        return true;
                    }
                }

                return Check(row, Convert.ToChar(Column.Id), 0, 2, color);


            }






            /// DIAG DOWNwARD REVERSED ///

            if (rowDirection == 0 && colDirection == 2)
            {
                if (Column.Prev != null)
                {
                    stopper = 0;


                    exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found2);
                    if (exists && found2.Data.PieceColor == color)
                    {
                        stopper++;
                    }




                    do
                    {
                        Column = Column.Prev;
                        row++;
                        exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found);
                        if (exists && found.Data.PieceColor == color)
                        {
                            stopper++;
                        }
                        else
                        {
                            break;
                        }
                    } while (Column.Prev != null && stopper != 4);

                    if (stopper == 4)
                    {
                        return true;
                    }
                }

                return false;


            }



            /// DIAG UPwARD ///

            if (rowDirection == 2 && colDirection == 2)
            {
                if (Column.Next != null)
                {
                    do
                    {
                        Column = Column.Next;
                        row++;
                        exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found);
                        if (exists && found.Data.PieceColor == color)
                        {
                            stopper++;
                        }
                        else
                        {
                            break;
                        }
                    } while (Column.Next != null && stopper != 4);

                    if (stopper == 4)
                    {
                        return true;
                    }
                }

                return Check(row, Convert.ToChar(Column.Id), 0, 0, color);


            }






            /// DIAG UPwARD REVERSED ///

            if (rowDirection == 0 && colDirection == 0)
            {
                if (Column.Prev != null)
                {
                    stopper = 0;


                    exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found2);
                    if (exists && found2.Data.PieceColor == color)
                    {
                        stopper++;
                    }




                    do
                    {
                        Column = Column.Prev;
                        row--;
                        exists = FindCell(Convert.ToChar(Column.Id), row, out DoubleLinkedListCell<GamePiece> found);
                        if (exists && found.Data.PieceColor == color)
                        {
                            stopper++;
                        }
                        else
                        {
                            break;
                        }
                    } while (Column.Prev != null && stopper != 4);

                    if (stopper == 4)
                    {
                        return true;
                    }
                }

                return false;


            }









            /// SHOULD NEVER COME HERE ///

            return false;
        }

        /// <summary>
        /// Checks if cell was placed in a spot that connects 4.
        /// </summary>
        /// <param name="cell"> The cell. </param>
        /// <returns> Whether he won. </returns>
        public bool CheckWin(DoubleLinkedListCell<GamePiece> cell)
        {
            Color theColor = cell.Data.PieceColor;


            bool win = false;


            /// KEY  FOR BELOW ///
            /*     0 = GO NEG DIRECTION (LEFT OR DOWN)
             *     1 = NEUTRAL (DONT MOVE)
             *     2 = GO POSITIVE DIRECTION (RIGHT OR UP)
             */




            win = Check(Convert.ToInt32(cell.Id[1]) - 48, cell.Id[0], 0, 1, theColor);     // GOES ROW DIRECTION LEFT, THEN RIGHT

            if (!win)
            {
                win = Check(Convert.ToInt32(cell.Id[1]) - 48, cell.Id[0], 1, 0, theColor);     // GOES COL DIRECTION DOWN, THEN UP
            }

            if (!win)
            {
                win = Check(Convert.ToInt32(cell.Id[1]) - 48, cell.Id[0], 2, 0, theColor);     // DIAG DOWNWARD, THEN REVERSED
            }

            if (!win)
            {
                win = Check(Convert.ToInt32(cell.Id[1]) - 48, cell.Id[0], 2, 2, theColor);     // DIAG UPWARD, THEN REVERSED
            }

            if (win)          // HE WON
            {
                return true;
            }



            /// DIDNT WIN ///

            return false;
        }

    }
}
