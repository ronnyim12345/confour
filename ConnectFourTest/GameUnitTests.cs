using NUnit.Framework;
using Ksu.Cis300.ConnectFour;
using System;

using System.Drawing;

namespace Tests
{
    public class GameUnitTests
    {
        Game _g;

        [SetUp]
        public void Setup()
        {
            _g = new Game();
        }

        [Test]
        [Category("A-Game Constructor")]
        public void A4_GameUnitTest_Base()
        {
            Assert.That(Game.COLUMN_SIZE == 6);
            Assert.That(Game.COLUMN_LABELS == "ABCDEFG");
            Assert.That(_g.Turn == PlayersTurn.Red);
        }

        [Test]
        [Category("A-Game Constructor")]
        public void A5_GameUnitTest_InitColumns()
        {
            Assert.That(_g.Column != null);
            char[] cols = Game.COLUMN_LABELS.ToCharArray();
            Array.Reverse(cols);
            Assert.That(_g.Column != null);
            Assert.That(_g.Column.Next == null);
            foreach (char c in cols)
            {
                Assert.That(_g.Column.Id == "" + c);
                _g.Column = _g.Column.Prev;
                if (c != Game.COLUMN_LABELS[0])
                {
                    Assert.That(_g.Column.Next.Id == "" + c);
                }
            }
            Assert.That(_g.Column == null);
        }


        [Test]
        [Category("B-Game Change Column")]
        public void B1_GameUnitTest_ChangeColumn_Same()
        {
            _g.ChangeColumn("G");
            Assert.That(_g.Column.Id == "G");
        }

        [Test]
        [Category("B-Game Change Column")]
        public void B2_GameUnitTest_ChangeColumn_First()
        {
            _g.ChangeColumn("A");
            Assert.That(_g.Column.Id == "A");
        }

        [Test]
        [Category("B-Game Change Column")]
        public void B3_GameUnitTest_ChangeColumn_FirstLast()
        {
            _g.ChangeColumn("G");
            _g.ChangeColumn("A");
            Assert.That(_g.Column.Id == "A");
        }

        [Test]
        [Category("B-Game Change Column")]
        public void B4_GameUnitTest_ChangeColumn_FirstMiddleLast()
        {
            _g.ChangeColumn("G");
            _g.ChangeColumn("D");
            _g.ChangeColumn("A");
            Assert.That(_g.Column.Id == "A");
        }

        private void assertCellHelper(DoubleLinkedListCell<GamePiece> cell, Color color, int row, char col)
        {
            Assert.That(cell != null);
            Assert.That(cell.Data != null);
            Assert.That(cell.Data.Row == row);
            Assert.That(cell.Data.Column == col);
            Assert.That(cell.Data.PieceColor == color);
        }


        [Test]
        [Category("C-Game Place New Piece")]
        public void C1_GameUnitTest_PlaceNewPiece_LastCol()
        {
            int row;
            Assert.That(_g.PlaceNewPiece(Color.Red, "G", out row) == "G1");
            Assert.That(row == 1);
            assertCellHelper(_g.Column.Data, Color.Red, 1, 'G');
        }

        [Test]
        [Category("C-Game Place New Piece")]
        public void C2_GameUnitTest_PlaceNewPiece_FirstCol()
        {
            int row;
            Assert.That(_g.PlaceNewPiece(Color.Black, "A", out row) == "A1");
            Assert.That(row == 1);
            assertCellHelper(_g.Column.Data, Color.Black, 1, 'A');
        }

        [Test]
        [Category("C-Game Place New Piece")]
        public void C3_GameUnitTest_PlaceNewPiece_MiddleCol()
        {
            int row;
            Assert.That(_g.PlaceNewPiece(Color.Black, "D", out row) == "D1");
            Assert.That(row == 1);
            assertCellHelper(_g.Column.Data, Color.Black, 1, 'D');
        }

        [Test]
        [Category("C-Game Place New Piece")]
        public void C4_GameUnitTest_PlaceNewPiece_Stack()
        {
            int row;
            Assert.That(_g.PlaceNewPiece(Color.Black, "D", out row) == "D1");
            Assert.That(row == 1);
            assertCellHelper(_g.Column.Data, Color.Black, 1, 'D');

            Assert.That(_g.PlaceNewPiece(Color.Red, "D", out row) == "D2");
            Assert.That(row == 2);
            assertCellHelper(_g.Column.Data, Color.Red, 2, 'D');

            Assert.That(_g.PlaceNewPiece(Color.Black, "D", out row) == "D3");
            Assert.That(row == 3);
            assertCellHelper(_g.Column.Data, Color.Black, 3, 'D');
        }

        [Test]
        [Category("C-Game Place New Piece")]
        public void C5_GameUnitTest_PlaceNewPiece_StackLinked()
        {
            int row;
            Assert.That(_g.PlaceNewPiece(Color.Black, "D", out row) == "D1");
            Assert.That(row == 1);
            assertCellHelper(_g.Column.Data, Color.Black, 1, 'D');

            Assert.That(_g.PlaceNewPiece(Color.Red, "D", out row) == "D2");
            Assert.That(row == 2);
            assertCellHelper(_g.Column.Data.Prev, Color.Black, 1, 'D');
            assertCellHelper(_g.Column.Data.Prev.Next, Color.Red, 2, 'D');
            assertCellHelper(_g.Column.Data, Color.Red, 2, 'D');

            Assert.That(_g.PlaceNewPiece(Color.Black, "D", out row) == "D3");
            Assert.That(row == 3);
            assertCellHelper(_g.Column.Data.Prev.Prev, Color.Black, 1, 'D');
            assertCellHelper(_g.Column.Data.Prev.Prev.Next, Color.Red, 2, 'D');
            assertCellHelper(_g.Column.Data.Prev, Color.Red, 2, 'D');
            assertCellHelper(_g.Column.Data.Prev.Next, Color.Black, 3, 'D');
            assertCellHelper(_g.Column.Data, Color.Black, 3, 'D');
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D1_GameUnitTest_FindCellStart()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "A", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(true, _g.FindCell('A', row, out found));
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D2_GameUnitTest_FindCellEnd()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "G", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(true, _g.FindCell('G', row, out found));
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D3_GameUnitTest_FindCellMid()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(true, _g.FindCell('D', row, out found));
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D4_GameUnitTest_FindCellEndBackandForth()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "F", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(true, _g.FindCell('F', row, out found));
            Assert.AreEqual(id, found.Id);

            id = _g.PlaceNewPiece(Color.Black, "B", out row);
            Assert.AreEqual(true, _g.FindCell('B', row, out found));
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D5_GameUnitTest_FindCellFail()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(false, _g.FindCell('D', 5, out found));
            Assert.IsNull(found);
        }
        [Test]
        [Category("D-Game Find Cell")]
        public void D6_GameUnitTest_FindCellFailBoundsCol()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(false, _g.FindCell('Z', row, out found));
            Assert.IsNull(found);
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D7_GameUnitTest_FindCellFailBoundsColLess()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(false, _g.FindCell('?', row, out found));
            Assert.IsNull(found);
        }

        [Test]
        [Category("D-Game Find Cell")]
        public void D8_GameUnitTest_FindCellFailBoundsRow()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(false, _g.FindCell('D', 10, out found));
            Assert.IsNull(found);
        }
        [Test]
        [Category("D-Game Find Cell")]
        public void D9_GameUnitTest_FindCellFailBoundsRowLess()
        {
            int row;
            string id;
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            Assert.AreEqual(false, _g.FindCell('D', -1, out found));
            Assert.IsNull(found);
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E1_GameUnitTest_CheckWin_Vertical()
        {
            int row = 0;
            string id = "";
            foreach (char c in Game.COLUMN_LABELS)
            {
                _g = new Game();
                for (int r = 0; r < Game.COLUMN_SIZE; r++)
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                DoubleLinkedListCell<GamePiece> found;
                _g.FindCell(c, row, out found);
                Assert.AreEqual(true, _g.CheckWin(found), "Expected a win in column " + c);
            }
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E2_GameUnitTest_CheckWin_VerticalStack()
        {
            int row;
            string id = "";
            id = _g.PlaceNewPiece(Color.Red, "A", out row);
            for (int r = 3; r <= Game.COLUMN_SIZE; r++)
            {
                id = _g.PlaceNewPiece(Color.Black, "A", out row);
            }
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found), "Expected a win in column A");
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E3_GameUnitTest_CheckWin_Horizontal()
        {
            int row = 0;
            string id = "";
            for (int r = 1; r <= Game.COLUMN_SIZE; r++)
            {
                _g = new Game();


                foreach (char c in Game.COLUMN_LABELS.Substring(0, 4))
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                DoubleLinkedListCell<GamePiece> found;
                _g.FindCell(id[0], row, out found);
                Assert.AreEqual(true, _g.CheckWin(found), "Expected a win in row " + r);
            }
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E4_GameUnitTest_CheckWin_HorizontalLoss()
        {
            int row;
            string id = "";
            foreach (char c in Game.COLUMN_LABELS.Substring(0, 3))
            {
                id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
            }
            id = _g.PlaceNewPiece(Color.Red, "E", out row);
            id = _g.PlaceNewPiece(Color.Red, "F", out row);
            id = _g.PlaceNewPiece(Color.Red, "G", out row);
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(false, _g.CheckWin(found), "Expected a loss in row 1");
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E5_GameUnitTest_CheckWin_HorizontalMiddle()
        {
            int row;
            string id = "";
            id = _g.PlaceNewPiece(Color.Red, "C", out row);
            id = _g.PlaceNewPiece(Color.Red, "E", out row);
            id = _g.PlaceNewPiece(Color.Red, "F", out row);
            id = _g.PlaceNewPiece(Color.Red, "G", out row);
            id = _g.PlaceNewPiece(Color.Red, "D", out row);
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found), "Expected a win in row 1");
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E6_GameUnitTest_CheckWin_DiagnalDownRight()
        {
            int row = 0;
            string id = "";
            int i = 1;
            foreach (char c in Game.COLUMN_LABELS.Substring(0, 4))
            {
                for (int r = i; r < Game.COLUMN_SIZE; r++)
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                id = _g.PlaceNewPiece(Color.Red, c.ToString(), out row);
                i++;
            }
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E7_GameUnitTest_CheckWin_DiagnalDownLeft()
        {
            int row = 0;
            string id = "";
            int i = 1;
            foreach (char c in "GFED")
            {
                for (int r = i; r < Game.COLUMN_SIZE; r++)
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                id = _g.PlaceNewPiece(Color.Red, c.ToString(), out row);
                i++;
            }
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E8_GameUnitTest_CheckWin_DiagnalDownRightMiddle()
        {
            int row;
            string id = "";
            int i = 1;
            foreach (char c in Game.COLUMN_LABELS.Substring(0, 4))
            {
                for (int r = i; r < Game.COLUMN_SIZE; r++)
                {
                    id = _g.PlaceNewPiece(Color.Red, c.ToString(), out row);
                }

                i++;
            }
            id = _g.PlaceNewPiece(Color.Black, "A", out row);
            id = _g.PlaceNewPiece(Color.Black, "B", out row);
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            id = _g.PlaceNewPiece(Color.Black, "C", out row);
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E9_GameUnitTest_CheckWin_DiagnalDownRightEnd()
        {
            int row;
            string id = "";
            int i = 1;
            foreach (char c in Game.COLUMN_LABELS.Substring(0, 4))
            {
                for (int r = i; r < Game.COLUMN_SIZE; r++)
                {
                    id = _g.PlaceNewPiece(Color.Red, c.ToString(), out row);
                }

                i++;
            }
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            id = _g.PlaceNewPiece(Color.Black, "C", out row);
            id = _g.PlaceNewPiece(Color.Black, "B", out row);
            id = _g.PlaceNewPiece(Color.Black, "A", out row);


            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E10_GameUnitTest_CheckWin_DiagnalUpRight()
        {
            int row;
            string id = "";
            int i = Game.COLUMN_SIZE;
            foreach (char c in Game.COLUMN_LABELS.Substring(2, 4))
            {
                for (int r = i; r > 1; r--)
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                i--;
            }
            id = _g.PlaceNewPiece(Color.Red, "F", out row);
            id = _g.PlaceNewPiece(Color.Red, "E", out row);
            id = _g.PlaceNewPiece(Color.Red, "D", out row);
            id = _g.PlaceNewPiece(Color.Red, "C", out row);
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E11_GameUnitTest_CheckWin_DiagnalUpRightEnd()
        {
            int row = 0;
            string id = "";
            int i = Game.COLUMN_SIZE;
            foreach (char c in Game.COLUMN_LABELS.Substring(2, 4))
            {
                for (int r = i; r > 1; r--)
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                id = _g.PlaceNewPiece(Color.Red, c.ToString(), out row);
                i--;
            }
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E12_GameUnitTest_CheckWin_DiagnalUpRightMiddle()
        {
            int row;
            string id = "";
            int i = Game.COLUMN_SIZE;
            foreach (char c in Game.COLUMN_LABELS.Substring(3, 4))
            {
                for (int r = i; r > 1; r--)
                {
                    id = _g.PlaceNewPiece(Color.Black, c.ToString(), out row);
                }
                i--;
            }
            id = _g.PlaceNewPiece(Color.Red, "G", out row);
            id = _g.PlaceNewPiece(Color.Red, "F", out row);
            id = _g.PlaceNewPiece(Color.Red, "D", out row);
            id = _g.PlaceNewPiece(Color.Red, "E", out row);
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(true, _g.CheckWin(found));
        }

        [Test]
        [Category("E-Game CheckWin")]
        public void E13GameUnitTest_CheckWin_DiagnalUpRightMiddle_Fail()
        {
            int row;
            string id = "";
            int i = Game.COLUMN_SIZE;
            foreach (char c in Game.COLUMN_LABELS.Substring(3, 4))
            {
                for (int r = i; r > 1; r--)
                {
                    id = _g.PlaceNewPiece(Color.Purple, c.ToString(), out row);
                }
                i--;
            }
            id = _g.PlaceNewPiece(Color.Red, "G", out row);
            id = _g.PlaceNewPiece(Color.Red, "F", out row);
            id = _g.PlaceNewPiece(Color.Black, "D", out row);
            id = _g.PlaceNewPiece(Color.Red, "E", out row);
            DoubleLinkedListCell<GamePiece> found;
            _g.FindCell(id[0], row, out found);
            Assert.AreEqual(false, _g.CheckWin(found));
        }

    }
}