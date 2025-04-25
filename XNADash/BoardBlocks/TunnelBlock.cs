using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using XNADash.Textures;

namespace XNADash.BoardBlocks
{
    public abstract class TunnelBlock : BaseAnimatedBlock
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
        protected override int MaxAnimationState
        {
            get
            {
                return 4;
            }
        }

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
                switch ( this.AnimationState )
                {
                    case 0:
                        return GameTexture.LTunnel0;
                    case 1:
                        return GameTexture.LTunnel1;
                    case 2:
                        return GameTexture.LTunnel2;
                    case 3:
                        return GameTexture.LTunnel3;
                    default:
                        throw new ArgumentException("invalid state");
                }
            }
        }

        public override string ToString()
        {
            return BlockConsts.LTUNNEL.ToString();
        }
    }

    public class TunnelRightBlock : TunnelBlock
    {
        protected override int MaxAnimationState
        {
            get
            {
                return 4;
            }
        }

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
                switch (this.AnimationState)
                {
                    case 0:
                        return GameTexture.RTunnel0;
                    case 1:
                        return GameTexture.RTunnel1;
                    case 2:
                        return GameTexture.RTunnel2;
                    case 3:
                        return GameTexture.RTunnel3;
                    default:
                        throw new ArgumentException("invalid state");
                }
            }
        }

        public override string ToString()
        {
            return BlockConsts.RTUNNEL.ToString();
        }
    }
}
