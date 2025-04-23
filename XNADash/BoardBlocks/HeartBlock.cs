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
    public class HeartBlock : BaseBlock
    {
        private int _textureState;
        private int _textureIncrement;

        const int MAXSTATE = 3;

        public HeartBlock()
        {
            this._textureState     = LocalRandom.Next(0, MAXSTATE);
            this._textureIncrement = 1;
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            base.ApplyPhysics(gameTime);

            if ( (_textureState == 0 && _textureIncrement == -1 ) ||
                ( _textureState == MAXSTATE && _textureIncrement == 1 )
                )
            {
                _textureIncrement = -_textureIncrement;
            }

            this._textureState += _textureIncrement;
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                switch (this._textureState)
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

    }
}
