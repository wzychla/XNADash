using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class BalloonBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Balloon;
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
    }
}
