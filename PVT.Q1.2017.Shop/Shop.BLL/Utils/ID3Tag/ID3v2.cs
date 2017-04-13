namespace Shop.Common.Helpers
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    using mp3info;

    /// <summary>
    ///     Summary description for ID3v2.
    /// </summary>
    public class ID3v2
    {
        /// <summary>
        /// </summary>
        private byte[] ba;

        /// <summary>
        /// </summary>
        private BinaryReader br;

        /// <summary>
        /// </summary>
        private bool ebUpdate;

        /// <summary>
        /// </summary>
        private bool ecCrc;

        /// <summary>
        /// </summary>
        private bool edRestrictions;

        /// <summary>
        /// </summary>
        private ulong extCCrc;

        /// <summary>
        /// </summary>
        private byte extDRestrictions;

        // ext header 

        /// <summary>
        /// </summary>
        private ulong extHeaderSize;

        /// <summary>
        /// </summary>
        private int extNumFlagBytes;

        /// <summary>
        /// </summary>
        private bool faUnsynchronisation;

        /// <summary>
        /// </summary>
        private bool fbExtendedHeader;

        /// <summary>
        /// </summary>
        private bool fcExperimentalIndicator;

        /// <summary>
        /// </summary>
        private bool fdFooter;

        /// <summary>
        /// </summary>
        private bool fileOpen;

        /// <summary>
        /// </summary>
        private ArrayList frames;

        /// <summary>
        /// </summary>
        private Hashtable framesHash;

        /// <summary>
        /// </summary>
        private bool hasTag;

        /// <summary>
        /// </summary>
        private ulong headerSize;

        // id3v2 header
        /// <summary>
        /// </summary>
        private int MajorVersion;

        /// <summary>
        /// </summary>
        private int MinorVersion;

        private byte[] trackFile;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ID3v2" /> class.
        /// </summary>
        /// <param name="trackTrackFile"></param>
        public ID3v2(byte[] trackTrackFile)
        {
            this.InitializeComponents();
            this.trackFile = trackTrackFile;
        }

        /// <summary>
        ///     Gets or sets the album.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        ///     Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether e b_ update 1.
        /// </summary>
        public bool EB_Update1
        {
            get
            {
                return this.ebUpdate;
            }

            set
            {
                this.ebUpdate = value;
            }
        }

        /// <summary>
        ///     Gets or sets the frames hash.
        /// </summary>
        public Hashtable FramesHash
        {
            get
            {
                return this.framesHash;
            }

            set
            {
                this.framesHash = value;
            }
        }

        /// <summary>
        ///     Gets or sets the genre.
        /// </summary>
        public string Genre { get; set; }

        // public bool header;

        // song info
        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the total tracks.
        /// </summary>
        public string totalTracks { get; set; }

        /// <summary>
        ///     Gets or sets the track.
        /// </summary>
        public string Track { get; set; }

        /// <summary>
        ///     Gets or sets the year.
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// </summary>
        public void Read()
        {
            var fs = new MemoryStream(this.trackFile);
            this.br = new BinaryReader(fs);

            this.ReadHeader();

            if (this.hasTag)
            {
                if (this.fbExtendedHeader)
                {
                    this.ReadExtendedHeader();
                }

                this.ReadFrames();

                if (this.fdFooter)
                {
                    this.ReadFooter();
                }

                this.ParseCommonHeaders();
            }

            fs.Close();
            this.br.Close();
        }

        /// <summary>
        /// </summary>
        private void InitializeComponents()
        {
            this.MajorVersion = 0;
            this.MinorVersion = 0;

            this.faUnsynchronisation = false;
            this.fbExtendedHeader = false;
            this.fcExperimentalIndicator = false;

            this.fileOpen = false;

            this.frames = new ArrayList();
            this.framesHash = new Hashtable();

            this.Album = string.Empty;
            this.Artist = string.Empty;
            this.Comment = string.Empty;
            this.extCCrc = 0;
            this.ebUpdate = false;
            this.ecCrc = false;
            this.edRestrictions = false;
            this.extDRestrictions = 0;
            this.extHeaderSize = 0;
            this.extNumFlagBytes = 0;
            this.fileOpen = false;
            this.hasTag = false;
            this.headerSize = 0;
            this.Title = string.Empty;
            this.totalTracks = string.Empty;
            this.Track = string.Empty;
            this.Year = string.Empty;
        }

        /// <summary>
        /// </summary>
        private void ParseCommonHeaders()
        {
            if (this.MajorVersion == 2)
            {
                if (this.framesHash.Contains("TT2"))
                {
                    var bytes = ((id3v2Frame)this.framesHash["TT2"]).frameContents;
                    var sb = new StringBuilder();
                    byte textEncoding;

                    for (var i = 1; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        // this.Title = myString.Substring(1);
                        this.Title = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TP1"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TP1"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Artist = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TAL"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TAL"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Album = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TYE"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TYE"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Year = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TRK"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TRK"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Track = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TCO"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TCO"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Genre = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("COM"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["COM"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Comment = sb.ToString();
                    }
                }
            }
            else
            {
                // $id3info["majorversion"] > 2
                if (this.framesHash.Contains("TIT2"))
                {
                    var bytes = ((id3v2Frame)this.framesHash["TIT2"]).frameContents;
                    var sb = new StringBuilder();
                    byte textEncoding;

                    for (var i = 1; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        // this.Title = myString.Substring(1);
                        this.Title = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TPE1"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TPE1"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Artist = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TALB"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TALB"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Album = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TYER"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TYER"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Year = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TRCK"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TRCK"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Track = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("TCON"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["TCON"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Genre = sb.ToString();
                    }
                }

                if (this.framesHash.Contains("COMM"))
                {
                    var sb = new StringBuilder();
                    var bytes = ((id3v2Frame)this.framesHash["COMM"]).frameContents;
                    byte textEncoding;

                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i == 0)
                        {
                            // read the text encoding.
                            textEncoding = bytes[i];
                        }
                        else
                        {
                            sb.Append(Convert.ToChar(bytes[i]));
                        }

                        this.Comment = sb.ToString();
                    }
                }
            }

            var trackHolder = this.Track.Split('/');
            this.Track = trackHolder[0];
            if (trackHolder.Length > 1) this.totalTracks = trackHolder[1];
        }

        /// <summary>
        /// </summary>
        private void ReadExtendedHeader()
        {
            // 			Extended header size   4 * %0xxxxxxx
            // 			Number of flag bytes       $01
            // 			Extended Flags             $xx
            // 			Where the 'Extended header size' is the size of the whole extended header, stored as a 32 bit synchsafe integer.
            // read teh size
            // this code is courtesy of Daniel E. White w/ minor modifications by me  Thanx Dan
            // Dan Code 
            var extHeaderSize = this.br.ReadChars(4); // I use this to read the bytes in from the file
            var bytes = new int[4]; // for bit shifting
            ulong newSize = 0; // for the final number

            // The ID3v2 tag size is encoded with four bytes
            // where the most significant bit (bit 7)
            // is set to zero in every byte,
            // making a total of 28 bits.
            // The zeroed bits are ignored
            // Some bit grinding is necessary.  Hang on.
            bytes[3] = extHeaderSize[3] | ((extHeaderSize[2] & 1) << 7);
            bytes[2] = ((extHeaderSize[2] >> 1) & 63) | ((extHeaderSize[1] & 3) << 6);
            bytes[1] = ((extHeaderSize[1] >> 2) & 31) | ((extHeaderSize[0] & 7) << 5);
            bytes[0] = (extHeaderSize[0] >> 3) & 15;

            newSize = (10 + (ulong)bytes[3]) | ((ulong)bytes[2] << 8) | ((ulong)bytes[1] << 16)
                      | ((ulong)bytes[0] << 24);

            // End Dan Code
            this.extHeaderSize = newSize;

            // next we read the number of flag bytes
            this.extNumFlagBytes = Convert.ToInt32(this.br.ReadByte());

            // read the flag byte(s) and set the flags
            var extFlags = BitReader.ToBitBools(this.br.ReadBytes(this.extNumFlagBytes));

            this.ebUpdate = extFlags[1];
            this.ecCrc = extFlags[2];
            this.edRestrictions = extFlags[3];

            // check for flags
            if (this.ebUpdate)
            {
                // this tag has no data but will have a null byte so we need to read it in
                // Flag data length      $00
                this.br.ReadByte();
            }

            if (this.ecCrc)
            {
                // Flag data length       $05
                // Total frame CRC    5 * %0xxxxxxx
                // read the first byte and check to make sure it is 5.  if not the header is corrupt
                // we will still try to process but we may be funked.
                var extC_FlagDataLength = Convert.ToInt32(this.br.ReadByte());
                if (extC_FlagDataLength == 5)
                {
                    extHeaderSize = this.br.ReadChars(5); // I use this to read the bytes in from the file
                    bytes = new int[4]; // for bit shifting
                    newSize = 0; // for the final number

                    // The ID3v2 tag size is encoded with four bytes
                    // where the most significant bit (bit 7)
                    // is set to zero in every byte,
                    // making a total of 28 bits.
                    // The zeroed bits are ignored
                    // Some bit grinding is necessary.  Hang on.
                    bytes[4] = extHeaderSize[4] | ((extHeaderSize[3] & 1) << 7);
                    bytes[3] = ((extHeaderSize[3] >> 1) & 63) | ((extHeaderSize[2] & 3) << 6);
                    bytes[2] = ((extHeaderSize[2] >> 2) & 31) | ((extHeaderSize[1] & 7) << 5);
                    bytes[1] = ((extHeaderSize[1] >> 3) & 15) | ((extHeaderSize[0] & 15) << 4);
                    bytes[0] = (extHeaderSize[0] >> 4) & 7;

                    newSize = (10 + (ulong)bytes[4]) | ((ulong)bytes[3] << 8) | ((ulong)bytes[2] << 16)
                              | ((ulong)bytes[1] << 24) | ((ulong)bytes[0] << 32);

                    this.extHeaderSize = newSize;
                }
            }

            if (this.edRestrictions)
            {
                // Flag data length       $01
                // Restrictions           %ppqrrstt

                // advance past flag data lenght byte
                this.br.ReadByte();

                this.extDRestrictions = this.br.ReadByte();
            }
        }

        /// <summary>
        /// </summary>
        private void ReadFooter()
        {
            // bring in the first three bytes.  it must be ID3 or we have no tag
            // TODO add logic to check the end of the file for "3D1" and other
            // possible starting spots
            var id3start = new string(this.br.ReadChars(3));

            // check for a tag
            if (!id3start.Equals("3DI"))
            {
                // TODO we are fucked.  not really we just don't ahve a tag
                // and we need to bail out gracefully.
                // throw id3v23ReaderException;
            }
            else
            {
                // we have a tag
                this.hasTag = true;
            }

            // read id3 version.  2 bytes:
            // The first byte of ID3v2 version is it's major version,
            // while the second byte is its revision number
            this.MajorVersion = Convert.ToInt32(this.br.ReadByte());
            this.MinorVersion = Convert.ToInt32(this.br.ReadByte());

            // here is where we get fancy.  I am useing silisoft's php code as 
            // a reference here.  we are going to try and parse for 2.2, 2.3 and 2.4
            // in one pass.  hold on!!
            if (this.hasTag && this.MajorVersion <= 4)
            {
                // probably won't work on higher versions
                // (%ab000000 in v2.2, %abc00000 in v2.3, %abcd0000 in v2.4.x)
                // read next byte for flags
                var boolar = BitReader.ToBitBool(this.br.ReadByte());

                // set the flags
                if (this.MajorVersion == 2)
                {
                    this.faUnsynchronisation = boolar[0];
                    this.fbExtendedHeader = boolar[1];
                }
                else if (this.MajorVersion == 3)
                {
                    // set the flags
                    this.faUnsynchronisation = boolar[0];
                    this.fbExtendedHeader = boolar[1];
                    this.fcExperimentalIndicator = boolar[2];
                }
                else if (this.MajorVersion == 4)
                {
                    // set the flags
                    this.faUnsynchronisation = boolar[0];
                    this.fbExtendedHeader = boolar[1];
                    this.fcExperimentalIndicator = boolar[2];
                    this.fdFooter = boolar[3];
                }

                // read teh size
                // this code is courtesy of Daniel E. White w/ minor modifications by me  Thanx Dan
                // Dan Code 
                var tagSize = this.br.ReadChars(4); // I use this to read the bytes in from the file
                var bytes = new int[4]; // for bit shifting
                ulong newSize = 0; // for the final number

                // The ID3v2 tag size is encoded with four bytes
                // where the most significant bit (bit 7)
                // is set to zero in every byte,
                // making a total of 28 bits.
                // The zeroed bits are ignored
                // Some bit grinding is necessary.  Hang on.
                bytes[3] = tagSize[3] | ((tagSize[2] & 1) << 7);
                bytes[2] = ((tagSize[2] >> 1) & 63) | ((tagSize[1] & 3) << 6);
                bytes[1] = ((tagSize[1] >> 2) & 31) | ((tagSize[0] & 7) << 5);
                bytes[0] = (tagSize[0] >> 3) & 15;

                newSize = (10 + (ulong)bytes[3]) | ((ulong)bytes[2] << 8) | ((ulong)bytes[1] << 16)
                          | ((ulong)bytes[0] << 24);

                // End Dan Code
                this.headerSize = newSize;
            }
        }

        /// <summary>
        /// </summary>
        private void ReadFrames()
        {
            var f = new id3v2Frame();
            do
            {
                f = f.ReadFrame(this.br, this.MajorVersion);

                // check if we have hit the padding.
                if (f.padding)
                {
                    // we hit padding.  lets advance to end and stop reading.
                    this.br.BaseStream.Position = Convert.ToInt64(this.headerSize);
                    break;
                }

                this.frames.Add(f);
                this.framesHash.Add(f.frameName, f);

                /*
                                else 
                                {
                                    // figure out which type it is
                                    if (f.frameName.StartsWith("T"))
                                    {
                                        if (f.frameName.Equals("TXXX"))
                                        {
                                            ProcessTXXX(f);
                                        }
                                        else 
                                        {
                                            ProcessTTYPE(f);
                                        }
                                    }
                                    else
                                    {
                                        if (f.frameName.StartsWith("W"))
                                        {
                                            if (f.frameName.Equals("WXXX"))
                                            {
                                                ProcessWXXX(f);
                                            }
                                            else 
                                            {
                                                ProcessWTYPE(f);
                                            }
                                        }
                                        else
                                        {
                                            // if it isn't  a muliple reader case (above) then throw it into the switch to process
                                            switch (f.frameName)
                                            {
                                            
                                                case "IPLS":
                                                    ProcessIPLS(f);
                                                    break;
                                                case "MCDI":
                                                    ProcessMCDI(f);
                                                    break;
                                                case "UFID":
                                                    ProcessUFID(f);
                                                    break;
                                                case "COMM":
                                                    ProcessCOMM(f);
                                                    break;
                                                    
                                                default:
                                                    frames.Add(f.frameName, f.frameContents);
                                                    AddItemToList(f.frameName, "non text");
                                                    break;
                                            }
                                }
                            
                        }
                
                
                            }*/
            }
            while (this.br.BaseStream.Position <= Convert.ToInt64(this.headerSize));
        }

        /// <summary>
        /// </summary>
        private void ReadHeader()
        {
            // bring in the first three bytes.  it must be ID3 or we have no tag
            // TODO add logic to check the end of the file for "3D1" and other
            // possible starting spots
            var id3start = new string(this.br.ReadChars(3));

            // check for a tag
            if (!id3start.Equals("ID3"))
            {
                // TODO we are fucked.
                // throw id3v2ReaderException;
                this.hasTag = false;
            }
            else
            {
                this.hasTag = true;

                // read id3 version.  2 bytes:
                // The first byte of ID3v2 version is it's major version,
                // while the second byte is its revision number
                this.MajorVersion = Convert.ToInt32(this.br.ReadByte());
                this.MinorVersion = Convert.ToInt32(this.br.ReadByte());

                // read next byte for flags
                var boolar = BitReader.ToBitBool(this.br.ReadByte());

                // set the flags
                this.faUnsynchronisation = boolar[0];
                this.fbExtendedHeader = boolar[1];
                this.fcExperimentalIndicator = boolar[2];

                // read teh size
                // this code is courtesy of Daniel E. White w/ minor modifications by me  Thanx Dan
                // Dan Code 
                var tagSize = this.br.ReadChars(4); // I use this to read the bytes in from the file
                var bytes = new int[4]; // for bit shifting
                ulong newSize = 0; // for the final number

                // The ID3v2 tag size is encoded with four bytes
                // where the most significant bit (bit 7)
                // is set to zero in every byte,
                // making a total of 28 bits.
                // The zeroed bits are ignored
                // Some bit grinding is necessary.  Hang on.
                bytes[3] = tagSize[3] | ((tagSize[2] & 1) << 7);
                bytes[2] = ((tagSize[2] >> 1) & 63) | ((tagSize[1] & 3) << 6);
                bytes[1] = ((tagSize[1] >> 2) & 31) | ((tagSize[0] & 7) << 5);
                bytes[0] = (tagSize[0] >> 3) & 15;

                newSize = (10 + (ulong)bytes[3]) | ((ulong)bytes[2] << 8) | ((ulong)bytes[1] << 16)
                          | ((ulong)bytes[0] << 24);

                // End Dan Code
                this.headerSize = newSize;
            }
        }
    }
}