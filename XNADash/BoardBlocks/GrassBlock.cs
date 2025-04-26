using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class GrassBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Grass;
            }
        }

        public override bool CanBeConsumed
        {
            get
            {
                return true;
            }
        }

        public override bool CanBeConsumedOverTheTunnel
        {
            get
            {
                return true;
            }
        }

        public override bool TriggersExplosion
        {
            get
            {
                return false;
            }
        }

        public override string ToString()
        {
            return BlockConsts.GRASS.ToString();
        }
    }
}
