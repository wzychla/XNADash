using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNADash.BoardBlocks
{
    public class OutOfBoardBlock : BaseBlock
    {
        public override bool CanExplode
        {
            get
            {
                return false;
            }
        }
    }
}
