/* GamePiece.cs
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
    public class GamePiece
    {
        /// <summary>
        /// Color of piece.
        /// </summary>
        public Color PieceColor { get; set; }


        /// <summary>
        /// The Row.
        /// </summary>
        public int Row { get; set; }


        /// <summary>
        /// The Column.
        /// </summary>
        public char Column { get; set; }


        /// <summary>
        /// Sets all the values.
        /// </summary>
        /// <param name="color"> Color. </param>
        /// <param name="row"> Row. </param>
        /// <param name="column"> Column. </param>
        public GamePiece(Color color, int row, char column)
        {
            PieceColor = color;
            Row = row;
            Column = column;
        }



    }
}
