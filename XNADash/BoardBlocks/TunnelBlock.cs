using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public class TunnelBlock : BaseBlock
    {
        public enum TunelOrientation { ToLeft, ToRight };

        public TunelOrientation Orientation { get; private set; }

        public Directions PointsTo
        {
            get
            {
                return this.Orientation == TunelOrientation.ToLeft ? Directions.W : Directions.E;
            }
        }

        public TunnelBlock( TunelOrientation Orientation )
        {
            this.Orientation = Orientation;
        }

        public override bool CanBeConsumed
        {
            get
            {
                BaseBlock neighbour = this.GetNeighbour( this.PointsTo );

                if ( neighbour == null || neighbour.CanBeConsumed )
                    return true;

                return false;
            }
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                switch ( this.Orientation )
                {
                    case TunelOrientation.ToLeft:
                        return GameTexture.LTunnel;
                    case TunelOrientation.ToRight:
                        return GameTexture.RTunnel;
                    default :
                        throw new Exception();
                }
            }
        }
    }
}
