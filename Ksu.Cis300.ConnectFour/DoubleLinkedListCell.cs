/* DoubleLinkedListCell.cs
 * Author: Ronny Im
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.ConnectFour
{
    public class DoubleLinkedListCell<T>
    {
        /// <summary>
        /// Previous LinkedList.
        /// </summary>
        public DoubleLinkedListCell<T> Prev { get; set; }


        /// <summary>
        /// The data of this linkedlist.
        /// </summary>
        public T Data { get; set; }


        /// <summary>
        /// The col and row of space.
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// Next Linkedlist.
        /// </summary>
        public DoubleLinkedListCell<T> Next { get; set; }


        /// <summary>
        /// Sets input string to Id.
        /// </summary>
        /// <param name="identifier"> The input string. </param>
        public DoubleLinkedListCell(string identifier)
        {
            Id = identifier;
        }

    }

}
