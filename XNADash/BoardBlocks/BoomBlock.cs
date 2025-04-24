using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class BoomBlock : BaseAnimatedBlock
    {
        protected override int MaxAnimationState
        {
            get
            {
                return 6;
            }
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                switch ( this.AnimationState )
                {
                    case 0:
                        return GameTexture.Boom0;
                    case 1:
                        return GameTexture.Boom1;
                    case 2:
                        return GameTexture.Boom2;
                    case 3:
                        return GameTexture.Boom3;
                    case 4:
                        return GameTexture.Boom2;
                    case 5:
                        return GameTexture.Boom1;
                    case 6:
                        return GameTexture.Boom0;
                    default:
                        throw new ArgumentException("invalid state");
                }
            }
        }

        public override bool CanExplode
        {
            get
            {
                return false;
            }
        }

        public override bool IsSubjectToPhysics
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

        public override bool CanBeConsumed
        {
            get
            {
                return true;
            }
        }

        protected override void AnimationUpdate()
        {
            this.AnimationState++;
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            this.AnimationUpdate();

            if (this.AnimationState > this.MaxAnimationState)
            {
                this.Board.RemoveBlock(this);
            }
        }
    }
}
