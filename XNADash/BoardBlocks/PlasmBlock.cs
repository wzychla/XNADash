using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class PlasmBlock : BaseAnimatedBlock
    {
        protected override int MaxAnimationState
        {
            get
            {
                return 8;
            }
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                switch ( this.AnimationState )
                {
                    case 0:
                        return GameTexture.Plasm0;
                    case 1:
                        return GameTexture.Plasm1;
                    case 2:
                        return GameTexture.Plasm2;
                    case 3:
                        return GameTexture.Plasm3;
                    case 4:
                        return GameTexture.Plasm4;
                    case 5:
                        return GameTexture.Plasm5;
                    case 6:
                        return GameTexture.Plasm6;
                    case 7:
                        return GameTexture.Plasm7;
                    default:
                        throw new ArgumentException("invalid state");
                }
            }
        }

        public override bool OthersFallFrom
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            return BlockConsts.PLASMA.ToString();
        }
    }
}
