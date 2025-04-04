using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNADash.BoardBlocks
{
    public class NullBlock : BaseBlock
    {
        public override bool TriggersExplosion
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
                return true;
            }
        }

        const int MAXFRAMES = 0;
        private int Frame = 0;
        public override void ApplyPhysics(GameTime gameTime)
        {
            if (Frame < MAXFRAMES)
                Frame++;
            else
            {
                this.Board.RemoveBlock(this);
            }

            return;
        }
    }
}
