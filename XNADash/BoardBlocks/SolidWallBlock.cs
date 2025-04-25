using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class SolidWallBlock : WallBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.SolidWall;
            }
        }

        public override bool OthersFallFrom
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

        public override string ToString()
        {
            return BlockConsts.METAL.ToString();
        }

    }
}
