﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNADash.BoardBlocks;
using XNADash.Textures;

namespace XNADash
{
    public class DashBoard
    {
        public const int DEFAULTBOARDSIZEX = 20;
        public const int DEFAULTBOARDSIZEY = 12;

        private int? _boardSizeX;
        public int BoardSizeX
        {
            get
            {
                return _boardSizeX != null ? _boardSizeX.Value : DEFAULTBOARDSIZEX;
            }
            set
            {
                _boardSizeX = value;
            }
        }

        private int? _boardSizeY;
        public int BoardSizeY
        {
            get
            {
                return _boardSizeY != null ? _boardSizeY.Value : DEFAULTBOARDSIZEY;
            }
            set
            {
                _boardSizeY = value;
            }
        }

        public const int BLOCKSIZE  = 32 * 2;

        public int HeartsToComplete { get; private set; }
        public int HeartsEaten { get; set; }

        public string LevelName { get; set; }
        public string LevelAuthor { get; set; }


        List<BaseBlock> _blocks = new List<BaseBlock>();
        public IEnumerable<BaseBlock> Blocks
        {
            get
            {
                return _blocks;
            }
        }

        public DashBoard()
        {
            HeartsToComplete = 0;
        }

        public bool MustRestart
        {
            get
            {
                if ( this.Blocks.Any( b => b is BoomBlock ) )
                    return false;

                if ( this.Blocks.All( b => !(b is PlayerBlock ) ) )
                    return true;

                if ( this.Blocks.All( b => !( b is ExitBlock ) ) )
                    return true;

                return false;
            }
        }

        public bool Completed
        {
            get
            {
                PlayerBlock player = this.Blocks.OfType<PlayerBlock>().FirstOrDefault();
                if ( player == null ) return false;

                return this.Blocks.Any( b => ( b is ExitBlock ) && b.X == player.X && b.Y == player.Y );
            }
        }

        public void AddBlock( BaseBlock Block )
        {
            Block.Board = this;

            if ( Block is HeartBlock )
                HeartsToComplete++;

            this._blocks.Add( Block );
        }

        public void RemoveBlock( BaseBlock Block )
        {
            this._blocks.Remove( Block );
        }

        public bool UpdateBoard( GameTime gameTime )
        {
            foreach (var block in Blocks)
            {
                block.Moved = false;
            }

            // kopia
            var _currentBlocks = Blocks.ToList();

            foreach (int col in Enumerable.Range(0, this.BoardSizeX))
            {
                foreach (int row in Enumerable.Range(0, this.BoardSizeY))
                {
                    var Block = _currentBlocks.Where(b => !b.Moved && b.Y == row && b.X == col).FirstOrDefault();
                    if (Block != null)
                    {
                        Block.ApplyPhysics(gameTime);
                    }
                }
            }

            return true;
        }

        public void ExplodePlayer()
        {
            PlayerBlock player = Blocks.OfType<PlayerBlock>().FirstOrDefault();

            if ( player != null )
                player.ExplodeNeighbour( Directions.None, true );
        }

        public bool UpdatePlayer( Directions Direction )
        {
            PlayerBlock player = Blocks.OfType<PlayerBlock>().FirstOrDefault();

            if ( player != null )
                player.UpdatePosition( this, Direction );

            if ( this.HeartsToComplete == this.HeartsEaten )
            {
                this.Blocks.OfType<ExitBlock>().ToList().ForEach( e => e.Open() );
            }

            return true;
        }

        public void DrawBoard( SpriteBatch spriteBatch, SpriteFont font )
        {
            foreach (var block in Blocks)
            {
                if (block.Texture != null)
                {
                    spriteBatch.Draw(block.Texture, new Rectangle(block.X * BLOCKSIZE, block.Y * BLOCKSIZE, BLOCKSIZE, BLOCKSIZE), Color.White);
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            // kopia
            var _currentBlocks = Blocks.ToList();

            foreach (int row in Enumerable.Range(0, this.BoardSizeY))
            {
                foreach (int col in Enumerable.Range(0, this.BoardSizeX))
                {
                    var Block = _currentBlocks.Where(b => b.Y == row && b.X == col).FirstOrDefault();
                    if (Block != null)
                    {
                        sb.Append(Block.ToString());
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }

                if (row < this.BoardSizeY - 1)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}
