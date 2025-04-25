using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;
using XNADash.Sound;
using Microsoft.Xna.Framework;

namespace XNADash.BoardBlocks
{
    public class BombBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                if (this.explodeCount == 0)
                {
                    return GameTexture.Bomb0;
                }
                else
                {
                    return GameTexture.Bomb1;
                }
            }
        }

        private bool _isSubjectToPhysics = true;
        public override bool IsSubjectToPhysics
        {
            get
            {
                return _isSubjectToPhysics;
            }
            set
            {
                _isSubjectToPhysics = value;
            }
        }

        public override bool CanBePushed
        {
            get
            {
                return true;
            }
        }

        const int EXPLODEMAX = 2;
        private int explodeCount = 0;

        private void CountDown()
        {
            if ( this.explodeCount > 0 )
                this.explodeCount++;    
        }

        public bool MustExplode
        {
            get
            {
                return this.explodeCount >= EXPLODEMAX;
            }
            set
            {
                if (value == true)
                {
                    this.explodeCount = 1;
                }
            }
        }

        public void Explode()
        {
            SoundFactory.Instance.RegisterEffect( SoundType.Bomb );

            this.ExplodeNeighbour( Directions.None, true );
            this.ExplodeNeighbour( Directions.S, false );
            this.ExplodeNeighbour( Directions.E, false);
            this.ExplodeNeighbour( Directions.W, false);
            this.ExplodeNeighbour( Directions.N, false);
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            if ( 
                 this.MustExplode ||
                 (
                   this.IsFalling &&
                   this.GetNeighbour( Directions.S ) != null &&
                   this.GetNeighbour( Directions.S ).TriggersExplosion &&
                   !this.GetNeighbour( Directions.S ).IsFalling
                  )
                )
            {
                this.Explode();
            }

            this.CountDown();

            base.ApplyPhysics(gameTime);

            return;
        }

        public override string ToString()
        {
            return BlockConsts.BOMB.ToString();
        }
    }
}
