using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace XNADash.Textures
{
    public class TextureFactory
    {
        Dictionary<GameTexture, Texture2D> _textures = new Dictionary<GameTexture,Texture2D>();

        private GraphicsDevice _device;
        public TextureFactory( GraphicsDevice Device )
        {
            this._device = Device;
            _instance = this;
        }

        private static TextureFactory _instance;
        public static TextureFactory Instance
        {
            get
            {
                if ( _instance == null )
                    throw new Exception( "TextureFactory is not initialized" );

                return _instance;
            }
        }

        public Texture2D GetTexture( GameTexture Texture )
        {
            if ( !_textures.ContainsKey( Texture ) )
                _textures.Add( Texture, Texture2D.FromStream( _device, File.Open( Path.Combine( DashGame.ExecutableDirectory, string.Format( "Textures\\{0}.png", Texture ) ), FileMode.Open ) ) );

            return _textures[Texture];
        }
    }

    public enum GameTexture 
    { 
        Empty, Grass, Wall, SolidWall, Stone, Heart, 
        Exit, ExitOpen, Player, Bomb, 
        Boom, 
        Plasm, Balloon, RTunnel, LTunnel, PTunnel };
}
