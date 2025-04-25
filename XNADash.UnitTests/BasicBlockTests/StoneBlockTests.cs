using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNADash.Levels;

namespace XNADash.UnitTests.BasicBlockTests
{
    /// <summary>
    /// Stone block tests
    /// </summary>
    [TestClass]
    public class StoneBlockTests
    {
        /// <summary>
        /// Test that stone falls down on empty
        /// </summary>
        [TestMethod]
        public void StoneFalls()
        {
            // arrange
            var sourceBoard = ".\r\n@\r\n \r\n \r\n%\r\n";
            var board = new LevelFactory.LevelReader().GetBoardFromString(null, null, sourceBoard);

            board.BoardSizeX = 1;
            board.BoardSizeY = 5;

            // act & assert

            // stone falls one down
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            var targetBoard = board.ToString();
            Assert.AreEqual(".\r\n \r\n@\r\n \r\n%", targetBoard);

            // and again
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            targetBoard = board.ToString();
            Assert.AreEqual(".\r\n \r\n \r\n@\r\n%", targetBoard);
        }

        /// <summary>
        /// Test that stone doesn't fall through grass
        /// </summary>
        [TestMethod]
        public void StoneDoesntFall()
        {
            // arrange
            var sourceBoard = ".\r\n@\r\n.\r\n \r\n%";
            var board = new LevelFactory.LevelReader().GetBoardFromString(null, null, sourceBoard);

            board.BoardSizeX = 1;
            board.BoardSizeY = 5;

            // act & assert

            // stone falls one down
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            var targetBoard = board.ToString();
            Assert.AreEqual(".\r\n@\r\n.\r\n \r\n%", targetBoard);
        }
    }
}
