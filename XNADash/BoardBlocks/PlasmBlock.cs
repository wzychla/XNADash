using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class PlasmBlock : BaseBlock
    {
        private int _textureState;

        const int MAXSTATE = 8;

        public override void ApplyPhysics(GameTime gameTime)
        {
            base.ApplyPhysics(gameTime);

            this._textureState++;

            if ( this._textureState >= MAXSTATE )
            {
                this._textureState = 0;
            }
        }
        protected override GameTexture BlockTexture
        {
            get
            {
                switch ( this._textureState )
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
    }
}
