using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNADash.Textures;
using XNADash.Sound;
using Microsoft.Xna.Framework;

namespace XNADash.BoardBlocks
{
    public class PlayerBlock : BaseBlock
    {
        protected override GameTexture BlockTexture
        {
            get
            {
                if (this.Board.Completed) return GameTexture.None;

                return this.Tunnels == null ? GameTexture.Player : GameTexture.PTunnel;
            }
        }

        public void UpdatePosition( DashBoard Board, Directions Direction )
        {
            if ( this.IsSubjectToPhysics ) return; 

            BaseBlock block = this.GetNeighbour( Direction );

            if ( block == null || block.CanBeConsumed )
                this.MoveTo( Direction );
            if ( block != null && block.CanBeConsumed )
            {
                if ( block is ExitBlock )
                {
                    SoundFactory.Instance.RegisterEffect( SoundType.Exit );
                    return;
                }
                if ( block is HeartBlock )
                {
                    Board.HeartsEaten++;
                    SoundFactory.Instance.RegisterEffect( SoundType.Heart );
                }
                if ( block is BoomBlock )
                {
                    this.ExplodeNeighbour( Directions.None, true );
                }
                if ( block is TunnelBlock )
                {
                    this.Tunnels = ( (TunnelBlock)block ).Orientation;
                }
                Board.RemoveBlock( block );
            }

            // przypadki specjalne
            if ( block != null && block.CanBePushed &&
                 (
                     (
                       Direction == Directions.E &&                 
                       block.GetNeighbour( Directions.E ) == null
                      )
                      ||
                     (
                       Direction == Directions.W &&                 
                       block.GetNeighbour( Directions.W ) == null
                      )
                  )
                )
            {
                this.MoveTo( Direction );
                block.MoveTo( Direction );
            }
        }

        public TunnelBlock.TunelOrientation? Tunnels { get; set; }

        /// <summary>
        /// Gdy jest w tunelu to fizyka porusza nim sama
        /// </summary>
        public override void ApplyPhysics(GameTime gameTime)
        {
            if ( this.Tunnels != null )
            {
                Directions autoMoveDirection = Tunnels.Value == TunnelBlock.TunelOrientation.ToLeft ? Directions.W : Directions.E;
                BaseBlock neighbour = this.GetNeighbour( autoMoveDirection );

                this.Board.RemoveBlock( neighbour );
                this.Board.AddBlock(
                    this.Tunnels.Value == TunnelBlock.TunelOrientation.ToLeft
                    ? new TunnelLeftBlock() { X = this.X, Y = this.Y }
                    : new TunnelRightBlock() { X = this.X, Y = this.Y });
                this.MoveTo( autoMoveDirection );

                if ( !( neighbour is TunnelBlock ) )
                    this.Tunnels = null;
            }
        }

        public override bool IsSubjectToPhysics
        {
            get
            {
                return this.Tunnels != null;
            }
        }

        public override string ToString()
        {
            return BlockConsts.DWARF.ToString();
        }
    }
}
