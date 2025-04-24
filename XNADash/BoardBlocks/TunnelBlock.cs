using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public abstract class TunnelBlock : BaseBlock
    {
        public enum TunelOrientation { ToLeft, ToRight };

        public abstract TunelOrientation Orientation { get; }

        public abstract Directions PointsTo { get; }

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
    }

    public class TunnelLeftBlock : TunnelBlock
    {
        public override TunelOrientation Orientation
        {
            get
            {
                return TunelOrientation.ToLeft;
            }
        }

        public override Directions PointsTo
        {
            get
            {
                return Directions.W;
            }
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.LTunnel;
            }
        }
    }

    public class TunnelRightBlock : TunnelBlock
    {
        public override TunelOrientation Orientation
        {
            get
            {
                return TunelOrientation.ToRight;
            }
        }

        public override Directions PointsTo
        {
            get
            {
                return Directions.E;
            }
        }

        protected override GameTexture BlockTexture
        {
            get
            {
                return GameTexture.RTunnel;
            }
        }
    }
}
