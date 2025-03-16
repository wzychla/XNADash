using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class PlasmBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Plasm;
            }
        }

        public override bool OthersFallFrom
        {
            get
            {
                return true;
            }
        }
    }
}
