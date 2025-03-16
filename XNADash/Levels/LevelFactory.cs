using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using XNADash.BoardBlocks;

namespace XNADash.Levels
{
    public class LevelFactory
    {
        class LevelReader
        {
            public LevelReader()
            {
            }

            List<DashBoard> _levels;
            public List<DashBoard> Levels
            {
                get
                {
                    if ( _levels == null )
                    {
                        _levels = new List<DashBoard>();

                        string HLFilePath  = Path.Combine( DashGame.ExecutableDirectory, "Levels\\LEVELS.HL" );
                        string XMLFilePath = Path.Combine( DashGame.ExecutableDirectory, "Levels\\LevelInfo.xml" );

                        if ( File.Exists( HLFilePath ) )
                        {
                            using ( FileStream fs = new FileStream( HLFilePath, FileMode.Open ) )
                            using ( StreamReader sr = new StreamReader( fs ) )
                            {
                                string author   = null;
                                string line     = null;
                                bool readsLevel = false;
                                int  levelNumber = 0;
                                List<string> RowData = new List<string>();

                                while ( ( line = sr.ReadLine() ) != null )
                                {
                                    if ( readsLevel )
                                        RowData.Add( line );
                                    if ( line.StartsWith( "author: " ) )
                                        author = line.Replace( "author: ", "" ).Trim();
                                    if ( line.StartsWith( "{" ) )
                                        readsLevel = true;
                                    if ( line.StartsWith( "}" ) )
                                    {
                                        string LevelName = string.Format( "HeartLight {0}", ++levelNumber );

                                        _levels.Add( this.GetBoardFromString( LevelName, author, RowData.ToArray() ) );

                                        readsLevel = false;
                                        RowData.Clear();
                                    }
                                }
                            }
                        }
                        else
                        {
                            XmlDocument xd = new XmlDocument();
                            using ( FileStream fs = new FileStream( XMLFilePath, FileMode.Open ) )
                                xd.Load( fs );

                            foreach ( XmlNode levelNode in xd.SelectNodes( "//levelInfo/level" ) )
                            {
                                string LevelName   = levelNode.Attributes["Name"].Value;
                                string LevelAuthor = levelNode.Attributes["Author"].Value;
                                string[] RowData   = levelNode.SelectNodes( "row" ).OfType<XmlNode>().Select( n => n.Attributes["data"].Value ).ToArray();

                                _levels.Add( this.GetBoardFromString( LevelName, LevelAuthor, RowData ) );
                            }
                        }
                    }

                    return _levels;
                }
            }

            private DashBoard GetBoardFromString( 
                string LevelName, string LevelAuthor, string[] RowData )
            {
                DashBoard board = new DashBoard();

                board.LevelName   = LevelName;
                board.LevelAuthor = LevelAuthor;

                int rowNumber = 0;
                foreach ( string rowData in RowData )
                {
                    int colNumber = 0;
                    foreach ( char cellData in rowData.ToCharArray() )
                    {
                        switch ( cellData )
                        {
                            case '$':
                                board.AddBlock( new HeartBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '@':
                                board.AddBlock( new StoneBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '.':
                                board.AddBlock( new GrassBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '*':
                                board.AddBlock( new PlayerBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '!':
                                board.AddBlock( new ExitBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '0':
                                board.AddBlock( new BalloonBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '&' : case 'B':
                                board.AddBlock( new BombBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '=':
                                board.AddBlock( new PlasmBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '#' : 
                                board.AddBlock( new WallBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '%':
                                board.AddBlock( new SolidWallBlock() { Y = rowNumber, X = colNumber } );
                                break;

                            case '<':
                                board.AddBlock( new TunnelBlock(TunnelBlock.TunelOrientation.ToLeft) { Y = rowNumber, X = colNumber } );
                                break;

                            case '>':
                                board.AddBlock( new TunnelBlock( TunnelBlock.TunelOrientation.ToRight ) { Y = rowNumber, X = colNumber } );
                                break;

                        }

                        colNumber++;
                    }

                    rowNumber++;
                }

                return board;
            }
        }

        LevelReader reader = new LevelReader();

        public LevelFactory()
        {
            this.Reset();
        }

        private static LevelFactory _instance;
        public static LevelFactory Instance
        {
            get
            {
                if ( _instance == null )
                    _instance = new LevelFactory();

                return _instance;
            }
        }

        public void Reset()
        {
            reader = new LevelReader();
        }

        public IEnumerable<DashBoard> Levels        
        {
            get
            {
                return reader.Levels;
            }
        }
    }
}
