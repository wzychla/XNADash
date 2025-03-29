using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class ExitBlock : BaseBlock
    {
        bool _opened = false;

        protected override GameTexture BlockTexture
        {
            get
            {
                if (this.Board.Completed)
                {
                    return GameTexture.ExitWithPlayer;
                }
                else
                {
                    return _opened ? GameTexture.ExitOpen : GameTexture.Exit;
                }
            }
        }

        public override bool CanExplode
        {
            get
            {
                return !this.Board.Completed;
            }
        }

        public void Open()
        {
            this._opened = true;
            this._canBeConsumed = true;
        }
    }
}
