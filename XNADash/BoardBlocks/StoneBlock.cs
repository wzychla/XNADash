using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class StoneBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Stone;
            }
        }

        public override bool IsSubjectToPhysics
        {
            get
            {
                return true;
            }
        }

        public override bool CanBePushed
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            return BlockConsts.STONE.ToString();
        }
    }
}
