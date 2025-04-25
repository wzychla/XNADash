using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNADash.Levels;

namespace XNADash.UnitTests.PhysicsTests
{
    /// <summary>
    /// Test specific in-game levels that do some physics without even player moving around
    /// There are multiple levels where blocks change: some fall, some are lifted by balloons, etc.
    /// 
    /// All these tests are expressed in the similar way:
    /// 
    /// * load starting board 
    /// * update it N-1 times
    /// * assert it's not the expected board
    /// * update the last, Nth, time
    /// * assert it's the expected board
    /// </summary>
    [TestClass]
    public class SpecificLevelTests
    {
        /// <summary>
        /// On level 12, all balloons should fall down as there are two hearts on each of baloons
        /// </summary>
        [TestMethod]
        public void Level12()
        {
            // arrange
            var sourceBoard =
@"####################
# # # # #!@# # # #*#
# # # # #$$# # # # #
#$# # # #@0# # # #$#
#$#$# # ##.# # #$#$#
#0 $#$# #  # #$#$ 0#
# #0 $#$#  #$#$ 0# #
# # #0 $#  #$ 0# # #
# # # #0    0# # # #
# # # # #  # # # # #
#                  #
####################";

            var targetBoard =
@"####################
# # # # #!@# # # #*#
# # # # #$$# # # # #
# # # # #@0# # # # #
# # # # ##.# # # # #
#   # # #  # # #   #
# #   # #  # #   # #
# # #   #  #   # # #
#$#$#$#$    $#$#$#$#
#$#$#$#$#  #$#$#$#$#
#0 0 0 0    0 0 0 0#
####################";

            var board = new LevelFactory.LevelReader().GetBoardFromString(null, null, sourceBoard);

            var numberOfUpdates = 10;

            // act

            // update the board 
            for ( int frame = 0; frame < numberOfUpdates-1; frame++ )
            {
                board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
                Assert.AreNotEqual(targetBoard, board.ToString(), true, $"should not be eq on {frame}");
            }

            // assert

            // final update yields
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            Assert.AreEqual(targetBoard, board.ToString());

        }

        /// <summary>
        /// On level 18, the balloon goes up with stone on it
        /// </summary>
        [TestMethod]
        public void Level18()
        {
            // arrange
            var sourceBoard =
@"%%@%%%%%%%%%%%%%@%%%
%*...$.%$...%$.%.&&%
%# %@..%.%..%@.% %&%
%  %..$..%@$@..% %&%
% #%..%@......$% %&%
%  %.%%.%.%%@%%% %&%
%# %..$.%..$.%   %&%
%  %!%%%%%%%%%  @%&%
% #%&&&&&&&&&&&&0%&%
%  %%%%%%%%%%%%%%%&%
%&&&&&&&&&&&&&&&&&&%
%%%%%%%%%%%%%%%%%%%%";

            var targetBoard =
@"%%@%%%%%%%%%%%%%@%%%
%*...$.%$...%$.%.&&%
%# %@..%.%..%@.%@%&%
%  %..$..%@$@..%0%&%
% #%..%@......$% %&%
%  %.%%.%.%%@%%% %&%
%# %..$.%..$.%   %&%
%  %!%%%%%%%%%   %&%
% #%&&&&&&&&&&&& %&%
%  %%%%%%%%%%%%%%%&%
%&&&&&&&&&&&&&&&&&&%
%%%%%%%%%%%%%%%%%%%%";

            var board = new LevelFactory.LevelReader().GetBoardFromString(null, null, sourceBoard);

            var numberOfUpdates = 5;

            // act

            // update the board 
            for (int frame = 0; frame < numberOfUpdates - 1; frame++)
            {
                board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
                Assert.AreNotEqual(targetBoard, board.ToString(), true, $"should not be eq on {frame}");
            }

            // assert

            // final update yields
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            Assert.AreEqual(targetBoard, board.ToString());
        }

        /// <summary>
        /// On level 22, multiple blocks fall down
        /// </summary>
        /// <remarks>
        /// This test fails! There's something wrong in the engine
        /// </remarks>
        [TestMethod]
        public void Level22()
        {
            // arrange
            var sourceBoard =
@"%%%%%%%%%%%%%%%%%%%%
%@$@$@$@$@$@$@$@$@$%
%#@$@$@$@$@$@$@$@$#%
% #    #   #     # %
%   #    #  #      %
%# #   #  #   ##   %
%     #      #   # %
% # ##  #  ##     #%
%  #               %
%   #  #  #   # ## %
%!      #    #  *  %
%%%%%%%%%%%%%%%%%%%%";

            var targetBoard =
@"%%%%%%%%%%%%%%%%%%%%
%                  %
%#                #%
% #    #   #@    # %
%   #@$  #  #$@    %
%# # @@#  #  @##   %
%   $$#     $#   # %
% #@##  #  ##   $ #%
%@ #   $       $@$ %
%$@ # @#$$#   #$##@%
%!@$ $@ #@ @$# @* $%
%%%%%%%%%%%%%%%%%%%%";

            var board = new LevelFactory.LevelReader().GetBoardFromString(null, null, sourceBoard);

            var numberOfUpdates = 29;

            // act

            // update the board 
            for (int frame = 0; frame < numberOfUpdates - 1; frame++)
            {
                board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
                Assert.AreNotEqual(targetBoard, board.ToString(), true, $"should not be eq on {frame}");
            }

            // assert

            // final update yields
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            Assert.AreEqual(targetBoard, board.ToString());
        }

        /// <summary>
        /// On level 26, multiple stones fall down
        /// </summary>
        [TestMethod]
        public void Level26()
        {
            // arrange
            var sourceBoard =
@"@@@@ .....@..$&.#..$
@@@% .....@@....#...
@@@%......@@@...#...
@@@%%%%%%%%%%%%%%...
@@@%...#....&..$%@..
!@ %...#..@$..@.%@.@
*  %.&.#...&@...%@..
   %...#........%...
   %.@.%%%%%%%%%%.@@
   %.......@........
   %.....@.$.@......
   %.......&.@@.....";

            var targetBoard =
@"   @ .....@..$&.#..$
   % .....@@....#...
   %......@@@...#...
   %%%%%%%%%%%%%%...
@  %...#....&..$%@..
!  %...#..@$..@.%@.@
*@ %.&.#...&@...%@..
 @@%...#........%...
@@@%.@.%%%%%%%%%%.@@
@@@%.......@........
@@@%.....@.$.@......
@@@%.......&.@@.....";

            var board = new LevelFactory.LevelReader().GetBoardFromString(null, null, sourceBoard);

            var numberOfUpdates = 23;

            // act

            // update the board 
            for (int frame = 0; frame < numberOfUpdates - 1; frame++)
            {
                board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
                Assert.AreNotEqual(targetBoard, board.ToString(), true, $"should not be eq on {frame}" );
            }

            // assert

            // final update yields
            board.UpdateBoard(new Microsoft.Xna.Framework.GameTime());
            Assert.AreEqual(targetBoard, board.ToString());
        }
    }
}
