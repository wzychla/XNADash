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
            if (Texture != GameTexture.None)
            {
                if (!_textures.ContainsKey(Texture))
                {
                    _textures.Add(Texture, Texture2D.FromStream(_device, File.Open(Path.Combine(DashGame.ExecutableDirectory, string.Format("Textures\\{0}.png", Texture)), FileMode.Open)));
                }

                return _textures[Texture];
            }
            else
            {
                return null;
            }
        }
    }

    public enum GameTexture 
    { 
        None,
        Empty, Grass, Wall, SolidWall, Stone, 
        Heart0, Heart1, Heart2, Heart3, 
        Exit, ExitOpen, ExitWithPlayer,
        Player, 
        Bomb0, Bomb1,
        Boom0, Boom1, Boom2, Boom3,
        Plasm0, Plasm1, Plasm2, Plasm3, Plasm4, Plasm5, Plasm6, Plasm7,
        Balloon0, Balloon1, Balloon2,
        RTunnel0, RTunnel1, RTunnel2, RTunnel3,
        LTunnel0, LTunnel1, LTunnel2, LTunnel3,
        PRTunnel, PLTunnel
    };
}
