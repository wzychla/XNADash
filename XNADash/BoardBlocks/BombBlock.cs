using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;
using XNADash.Sound;

namespace XNADash.BoardBlocks
{
    public class BombBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Bomb;
            }
        }

        public override bool IsSubjectToPhysics
        {
            get
            {
                return true;
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
                if ( value == true )
                    this.explodeCount = 1;
            }
        }

        public void Explode()
        {
            SoundFactory.Instance.RegisterEffect( SoundType.Bomb );

            this.ExplodeNeighbour( Directions.None );
            this.ExplodeNeighbour( Directions.S );
            this.ExplodeNeighbour( Directions.E );
            this.ExplodeNeighbour( Directions.W );
            this.ExplodeNeighbour( Directions.N );
        }

        public override void ApplyPhysics()
        {
            if ( 
                 this.MustExplode ||
                 (
                   this.IsFalling &&
                   this.GetNeighbour( Directions.S ) != null &&
                   this.GetNeighbour( Directions.S ).TriggersExplosion 
                  )
                )
            {
                this.Explode();
            }

            this.CountDown();

            base.ApplyPhysics();

            return;
        }
    }
}
