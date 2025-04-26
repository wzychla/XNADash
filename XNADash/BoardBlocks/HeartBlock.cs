using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using XNADash.Textures;
using XNADash.Utils;

namespace XNADash.BoardBlocks
{
    // 
    public class HeartBlock : BaseBouncyAnimatedBlock
    {
        protected override int MaxAnimationState
        {
            get
            {
                return 3;
            }
        }

        public HeartBlock()
        {
            this.AnimationState     = LocalRandom.NextInclusive(0, this.MaxAnimationState);
            this.AnimationIncrement = 1;
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                switch (this.AnimationState)
                {
                    case 0:
                        return GameTexture.Heart0;
                    case 1:
                        return GameTexture.Heart1;
                    case 2:
                        return GameTexture.Heart2;
                    case 3:
                        return GameTexture.Heart3;
                    default:
                        throw new ArgumentException("invalid state");
                }
            }
        }

        public override bool CanBeConsumed
        {
            get
            {
                return true;
            }
        }



        public override bool IsSubjectToPhysics
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            return BlockConsts.HEART.ToString();
        }
    }
}
