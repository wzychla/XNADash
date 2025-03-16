using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNADash.BoardBlocks
{
    public class NullBlock : BaseBlock
    {
        public override bool TriggersExplosion
        {
            get
            {
                return false;
            }
        }

        public override bool CanExplode
        {
            get
            {
                return false;
            }
        }
    }
}
