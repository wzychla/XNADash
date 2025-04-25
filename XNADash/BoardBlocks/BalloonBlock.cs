using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;
using XNADash.Utils;

namespace XNADash.BoardBlocks
{
    public class BalloonBlock : BaseBouncyAnimatedBlock
    {
        protected override int MaxAnimationState
        {
            get
            {
                return 2;
            }
        }

        public BalloonBlock()
        {
            this.AnimationState     = LocalRandom.NextInclusive(0, this.MaxAnimationState);
            this.AnimationIncrement = 1;
        }
        protected override GameTexture BlockTexture
        {
            get
            {
                switch ( this.AnimationState )
                {
                    case 0:
                        return GameTexture.Balloon0;
                    case 1:
                        return GameTexture.Balloon1;
                    case 2:
                        return GameTexture.Balloon2;
                    default:
                        throw new ArgumentException("invalid state");
                }
            }
        }

        public override bool CanBePushed
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

        public override void ApplyPhysics(GameTime gameTime)
        {
            this.AnimationUpdate();

            BaseBlock s1 = this.GetNeighbour( Directions.S );
            BaseBlock n1 = this.GetNeighbour( Directions.N );

            // spad w dół - dwa nad nim i nic pod spodem
            if (s1 == null && n1 != null && !(n1 is BalloonBlock) && !(n1 is BoomBlock))
            {
                BaseBlock n2 = n1.GetNeighbour(Directions.N);
                if (n2 != null && !(n2 is BalloonBlock) && !(n2 is BoomBlock))
                {
                    if (n1.IsSubjectToPhysics && n2.IsSubjectToPhysics && !n2.IsFalling)
                    {
                        n1.MoveTo(Directions.S);
                        this.MoveTo(Directions.S);
                    }

                    return;
                }
            }

            // ruch do góry - nic nad nim
            if ( n1 == null )
            {
                this.MoveTo( Directions.N );             

                return;
            }

            // ruch do góry - jeden nad nim i nic jeszcze wyżej (unosi coś)
            if ( n1 != null )
            {
                BaseBlock n2 = n1.GetNeighbour( Directions.N );

                if ( 
                     ( n1.IsSubjectToPhysics || n1 is PlayerBlock ) && 
                      !(n1 is BoomBlock) &&
                       n2 == null 
                    )
                {
                    if ( n1 is BombBlock b )
                    {
                        b.IsFalling = false;
                    }

                    n1.MoveTo( Directions.N );
                    this.MoveTo( Directions.N );

                    return;
                }
            }
        }

        public override string ToString()
        {
            return BlockConsts.BALLOON.ToString();
        }
    }
}
