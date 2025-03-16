using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class BoomBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Boom;
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

        public override bool CanBeConsumed
        {
            get
            {
                return true;
            }
        }

        const int MAXFRAMES = 3;
        int Frame = 0;
        public override void ApplyPhysics()
        {
            if ( Frame < MAXFRAMES )
                Frame++;
            else
            {
                Frame = 0;
                this.Board.RemoveBlock( this );
            }

            return;
        }
    }
}
