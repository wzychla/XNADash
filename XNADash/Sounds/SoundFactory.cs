using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Microsoft.Xna.Framework.Media;

namespace XNADash.Sound
{
    public class SoundFactory
    {
        private SoundEffect currentEffect { get; set; }

        private static SoundFactory _instance;
        public static SoundFactory Instance
        {
            get
            {
                if ( _instance == null )
                    _instance = new SoundFactory();

                return _instance;
            }
        }

        public SoundFactory()
        {
            foreach ( SoundType value in Enum.GetValues( typeof( SoundType ) ) )
                GetEffect( value );
        }

        public string[] SongNames
        {
            get
            {
                return Directory.GetFiles( Path.Combine( DashGame.ExecutableDirectory, "Sounds" ), "*.mp3" );
            }
        }

        public void PlaySong( int CurrentLevelNumber )
        {
            if ( SongNames.Count() > 0 )
            {
                int SongIndex = CurrentLevelNumber % SongNames.Count();
                string SongFileName = string.Format( "file://{0}", SongNames[SongIndex] );

                Song song = Song.FromUri( "Song", new Uri( SongFileName ) );
                MediaPlayer.Play(  song );
            }
        }

        Dictionary<SoundType, SoundEffect> _effects = new Dictionary<SoundType, SoundEffect>();
        private SoundEffect GetEffect( SoundType Type )
        {
            if ( !_effects.ContainsKey( Type ) )
                using ( FileStream fs = File.Open( Path.Combine( DashGame.ExecutableDirectory, string.Format( "Sounds\\{0}.wav", Type ) ), FileMode.Open ) )
                    _effects.Add( Type, SoundEffect.FromStream( fs ) );

            return _effects[Type];
        }

        public void RegisterEffect( SoundType Type )
        {
            currentEffect = GetEffect( Type );
        }

        public void PlayEffect()
        {
            if ( currentEffect != null )
            {
                currentEffect.Play();
                currentEffect = null;
            }
        }
    }

    public enum SoundType { Heart, Stone, Exit, Bomb };
}
