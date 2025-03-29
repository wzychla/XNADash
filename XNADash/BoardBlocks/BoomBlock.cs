using Microsoft.Xna.Framework;
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
                return
                    this.IsBig
                    ? GameTexture.Boom
                    : GameTexture.BoomSmall;
            }
        }

        public bool IsBig { get; set; }

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

        const int MAXFRAMES = 6;
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
