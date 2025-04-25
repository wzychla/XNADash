using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using XNADash.Textures;
using XNADash.Sound;
using Microsoft.Xna.Framework;

namespace XNADash.BoardBlocks
{
    public abstract class BaseBlock
    {
        public int X;
        public int Y;
        public bool Moved = false;

        public DashBoard Board { get; set; }

        protected bool _canBeConsumed = false;
        public virtual bool CanBeConsumed
        {
            get
            {
                return _canBeConsumed;
            }
        }

        public virtual bool IsSubjectToPhysics
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public virtual bool CanBePushed
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanExplode
        {
            get
            {
                return true;
            }
        }
        public virtual bool TriggersExplosion
        {
            get
            {
                return true;
            }
        }

        public virtual bool OthersFallFrom
        {
            get
            {
                return false;
            }
        }

        protected virtual GameTexture BlockTexture
        {
            get
            {
                return GameTexture.Empty;
            }
        }

        public virtual Texture2D Texture
        {
            get
            {
                return TextureFactory.Instance.GetTexture( this.BlockTexture );
            }
        }

        public BaseBlock GetNeighbour( Directions Direction )
        {
            return GetNeighbour( GetNeighbourDeltaX( Direction ), GetNeighbourDeltaY( Direction ) );
        }

        public BaseBlock GetNeighbour( int dX, int dY )
        {
            int NewX = this.X + dX;
            int NewY = this.Y + dY;

            if (NewX < 0 || NewX > this.Board.BoardSizeX - 1 ||
                NewY < 0 || NewY > this.Board.BoardSizeY - 1
                )
            {
                return new OutOfBoardBlock() { Board = this.Board };
            }

            return 
                this.Board.Blocks
                    .Where(
                        b =>
                            b.X == NewX &&
                            b.Y == NewY
                        )
                    .FirstOrDefault();
        }

        public void MoveTo( Directions Direction )
        {
            this.X += GetNeighbourDeltaX( Direction );
            this.Y += GetNeighbourDeltaY( Direction );
            this.Moved = true;
        }

        protected int GetNeighbourDeltaX( Directions Direction )
        {
            switch ( Direction )
            {
                case Directions.NW:
                case Directions.W:
                case Directions.SW:
                    return -1;

                case Directions.NE:
                case Directions.E:
                case Directions.SE:
                    return +1;

                default:
                    return 0;
            }
        }

        protected int GetNeighbourDeltaY( Directions Direction )
        {
            switch ( Direction )
            {
                case Directions.NW:
                case Directions.N:
                case Directions.NE: 
                    return -1; 

                case Directions.SW: 
                case Directions.S: 
                case Directions.SE:
                    return +1;

                default:
                    return 0;
            }
        }

        public bool IsFalling = false;

        public bool IsAlmostFalling
        {
            get
            {
                return delayFallE > 0 || delayFallW > 0;
            }
        }

        private int delayFallW = 0;
        private int delayFallE = 0;
        const int DELAYFALL = 2;

        public virtual void ApplyPhysics( GameTime gameTime )
        {
            if ( !this.IsSubjectToPhysics ) return;

            // czy pod spodem coś jest?
            BaseBlock blockAtS = this.GetNeighbour( Directions.S );
            if (blockAtS == null)
            {
                this.MoveTo(Directions.S);
                IsFalling = true;

                return;
            }
            else
            {
                // spadł na gracza
                if (blockAtS is PlayerBlock && this.IsFalling)
                {
                    blockAtS.ExplodeNeighbour(Directions.None, true );
                    SoundFactory.Instance.RegisterEffect(SoundType.Stone);
                }
                // falling heart doesn't cause bombs to explode
                if (blockAtS is BombBlock && this.IsFalling && !(this is HeartBlock))
                {
                    ((BombBlock)blockAtS).Explode();
                    SoundFactory.Instance.RegisterEffect(SoundType.Stone);
                }

                // stoi na kamieniu
                if (
                     (blockAtS.IsSubjectToPhysics && 
                     !blockAtS.IsFalling && 
                      blockAtS is not BalloonBlock 
                      ) 
                      ||
                     (blockAtS.OthersFallFrom)
                    )
                {
                    // może spaść w prawo - lewo?
                    if ( 
                         this.GetNeighbour(Directions.W) == null &&
                         this.GetNeighbour(Directions.SW) == null &&
                         (this.GetNeighbour(Directions.NW) == null || !this.GetNeighbour(Directions.NW).IsSubjectToPhysics)
                        )
                    {
                        delayFallW++;

                        if (delayFallW >= DELAYFALL)
                        {
                            this.MoveTo(Directions.W);
                            IsFalling = true;
                            delayFallW = delayFallE = blockAtS.IsAlmostFalling ? 1 : 0;
                        }

                        return;
                    }
                    if (
                         this.GetNeighbour(Directions.E) == null &&
                         this.GetNeighbour(Directions.SE) == null &&
                         (this.GetNeighbour(Directions.NE) == null || !this.GetNeighbour(Directions.NE).IsSubjectToPhysics)
                        )
                    {
                        delayFallE++;

                        if (delayFallE >= DELAYFALL)
                        {
                            this.MoveTo(Directions.E);
                            IsFalling = true;
                            delayFallW = delayFallE = blockAtS.IsAlmostFalling ? 1 : 0;
                        }

                        return;
                    }
                }
            }

            IsFalling = false;

            return;
        }

        public void ExplodeNeighbour( Directions Direction, bool Primary )
        {
            BaseBlock neighbour = this.GetNeighbour( Direction );
            if ( neighbour != null &&
                 neighbour.CanExplode
                )
            {
                if (  neighbour is BombBlock &&
                    ( Direction == Directions.W || Direction == Directions.E || Direction == Directions.S || Direction == Directions.N )
                    )
                {
                    BombBlock bombNeighbour          = neighbour as BombBlock;
                    bombNeighbour.MustExplode        = true;
                    bombNeighbour.Moved              = true;
                    bombNeighbour.IsSubjectToPhysics = false;
                }
                else 
                {
                    this.Board.RemoveBlock( neighbour );
                    this.Board.AddBlock(
                        new BoomBlock() 
                        { 
                            X = neighbour.X, 
                            Y = neighbour.Y 
                        });
                }
            }
            else if ( neighbour == null )
            {
                this.Board.AddBlock(
                    new BoomBlock()
                    {
                        X = this.X + this.GetNeighbourDeltaX(Direction),
                        Y = this.Y + this.GetNeighbourDeltaY(Direction)
                    });
            }

        }
    }

    /// <summary>
    /// Base block with animated texture where the animation is a cycle
    /// </summary>
    public abstract class BaseAnimatedBlock : BaseBlock
    {
        protected int AnimationState;

        protected abstract int MaxAnimationState { get; }

        protected virtual void AnimationUpdate()
        {
            this.AnimationState++;

            if (this.AnimationState >= this.MaxAnimationState)
            {
                this.AnimationState = 0;
            }
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            base.ApplyPhysics(gameTime);

            this.AnimationUpdate();
        }

        public override string ToString()
        {
            return " ";
        }
    }

    /// <summary>
    /// Base block with animated texture but the animation goes up and down
    /// </summary>
    public abstract class BaseBouncyAnimatedBlock : BaseAnimatedBlock
    {
        protected int AnimationIncrement = 1;

        protected override void AnimationUpdate()
        {
            if ((this.AnimationState == 0 && this.AnimationIncrement == -1) ||
                (this.AnimationState == this.MaxAnimationState && this.AnimationIncrement == 1)
                )
            {
                this.AnimationIncrement = -this.AnimationIncrement;
            }

            this.AnimationState += this.AnimationIncrement;
        }
    }

    public enum Directions { None, NW, N, NE, W, E, SW, S, SE }
}
