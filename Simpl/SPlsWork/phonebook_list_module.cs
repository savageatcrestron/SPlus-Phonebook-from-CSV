using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_PHONEBOOK_LIST_MODULE
{
    public class UserModuleClass_PHONEBOOK_LIST_MODULE : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput SEARCH;
        Crestron.Logos.SplusObjects.DigitalInput CLEAR_SEARCH;
        Crestron.Logos.SplusObjects.StringInput NAMETOFIND;
        Crestron.Logos.SplusObjects.StringInput REMOTEFILE;
        Crestron.Logos.SplusObjects.StringInput LOCALFILE;
        Crestron.Logos.SplusObjects.StringInput FTPUSERNAME;
        Crestron.Logos.SplusObjects.StringInput FTPPASSWORD;
        Crestron.Logos.SplusObjects.DigitalOutput SEARCHINPROGRESS;
        Crestron.Logos.SplusObjects.StringOutput TOCONSOLE;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> FIRST_NAME;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> LAST_NAME;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> PHONENUMBER;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> EMAIL;
        CrestronString [] GLOBALSTORAGE;
        PHONELIST [] CURRENTLIST;
        private void PRINTDATATOPANEL (  SplusExecutionContext __context__, CrestronString XNAME ) 
            { 
            ushort X = 0;
            ushort MATCHCOUNT = 0;
            
            CrestronString FINDTHIS;
            FINDTHIS  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 30, this );
            
            
            __context__.SourceCodeLine = 35;
            MATCHCOUNT = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 37;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (XNAME == ""))  ) ) 
                { 
                __context__.SourceCodeLine = 39;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)30; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( X  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (X  >= __FN_FORSTART_VAL__1) && (X  <= __FN_FOREND_VAL__1) ) : ( (X  <= __FN_FORSTART_VAL__1) && (X  >= __FN_FOREND_VAL__1) ) ; X  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 41;
                    FIRST_NAME [ X]  .UpdateValue ( CURRENTLIST [ X] . PLFNAME  ) ; 
                    __context__.SourceCodeLine = 42;
                    LAST_NAME [ X]  .UpdateValue ( CURRENTLIST [ X] . PLLNAME  ) ; 
                    __context__.SourceCodeLine = 43;
                    PHONENUMBER [ X]  .UpdateValue ( CURRENTLIST [ X] . PLNUMBER  ) ; 
                    __context__.SourceCodeLine = 44;
                    EMAIL [ X]  .UpdateValue ( CURRENTLIST [ X] . PLEMAIL  ) ; 
                    __context__.SourceCodeLine = 39;
                    } 
                
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 48;
                ushort __FN_FORSTART_VAL__2 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__2 = (ushort)30; 
                int __FN_FORSTEP_VAL__2 = (int)1; 
                for ( X  = __FN_FORSTART_VAL__2; (__FN_FORSTEP_VAL__2 > 0)  ? ( (X  >= __FN_FORSTART_VAL__2) && (X  <= __FN_FOREND_VAL__2) ) : ( (X  <= __FN_FORSTART_VAL__2) && (X  >= __FN_FOREND_VAL__2) ) ; X  += (ushort)__FN_FORSTEP_VAL__2) 
                    { 
                    __context__.SourceCodeLine = 50;
                    FIRST_NAME [ X]  .UpdateValue ( ""  ) ; 
                    __context__.SourceCodeLine = 51;
                    LAST_NAME [ X]  .UpdateValue ( ""  ) ; 
                    __context__.SourceCodeLine = 52;
                    PHONENUMBER [ X]  .UpdateValue ( ""  ) ; 
                    __context__.SourceCodeLine = 53;
                    EMAIL [ X]  .UpdateValue ( ""  ) ; 
                    __context__.SourceCodeLine = 55;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (CURRENTLIST[ X ].PLFNAME == XNAME) ) || Functions.TestForTrue ( Functions.BoolToInt (CURRENTLIST[ X ].PLLNAME == XNAME) )) ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 57;
                        Trace( "match found to xName") ; 
                        __context__.SourceCodeLine = 58;
                        FIRST_NAME [ MATCHCOUNT]  .UpdateValue ( CURRENTLIST [ X] . PLFNAME  ) ; 
                        __context__.SourceCodeLine = 59;
                        LAST_NAME [ MATCHCOUNT]  .UpdateValue ( CURRENTLIST [ X] . PLLNAME  ) ; 
                        __context__.SourceCodeLine = 60;
                        PHONENUMBER [ MATCHCOUNT]  .UpdateValue ( CURRENTLIST [ X] . PLNUMBER  ) ; 
                        __context__.SourceCodeLine = 61;
                        EMAIL [ MATCHCOUNT]  .UpdateValue ( CURRENTLIST [ X] . PLEMAIL  ) ; 
                        __context__.SourceCodeLine = 62;
                        MATCHCOUNT = (ushort) ( (MATCHCOUNT + 1) ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 48;
                    } 
                
                } 
            
            
            }
            
        private void PARSEDATA (  SplusExecutionContext __context__, CrestronString READFROMFILE ) 
            { 
            ushort C1 = 0;
            ushort C2 = 0;
            ushort C3 = 0;
            ushort C4 = 0;
            ushort C5 = 0;
            ushort C6 = 0;
            ushort C7 = 0;
            ushort EOL = 0;
            ushort COUNT = 0;
            
            CrestronString SINGLELINE;
            CrestronString CHEWDATA;
            SINGLELINE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
            CHEWDATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
            
            
            __context__.SourceCodeLine = 73;
            COUNT = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 74;
            CHEWDATA  .UpdateValue ( READFROMFILE  ) ; 
            __context__.SourceCodeLine = 76;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "\u000d\u000a" , CHEWDATA , 1 ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 79;
                SINGLELINE  .UpdateValue ( Functions.Remove ( "\u000d\u000a" , CHEWDATA , 1)  ) ; 
                __context__.SourceCodeLine = 80;
                Trace( "single line = {0}", SINGLELINE ) ; 
                __context__.SourceCodeLine = 104;
                C1 = (ushort) ( Functions.Find( "," , SINGLELINE ) ) ; 
                __context__.SourceCodeLine = 105;
                Trace( "c1 = {0:d}", (short)C1) ; 
                __context__.SourceCodeLine = 106;
                C2 = (ushort) ( Functions.Find( "," , SINGLELINE , (C1 + 1) ) ) ; 
                __context__.SourceCodeLine = 107;
                Trace( "c2 = {0:d}", (short)C2) ; 
                __context__.SourceCodeLine = 108;
                C3 = (ushort) ( Functions.Find( "," , SINGLELINE , (C2 + 1) ) ) ; 
                __context__.SourceCodeLine = 109;
                Trace( "c3 = {0:d}", (short)C3) ; 
                __context__.SourceCodeLine = 110;
                C4 = (ushort) ( Functions.Find( "\u000d\u000a" , SINGLELINE , (C3 + 1) ) ) ; 
                __context__.SourceCodeLine = 112;
                CURRENTLIST [ COUNT] . PLFNAME  .UpdateValue ( Functions.Mid ( SINGLELINE ,  (int) ( 1 ) ,  (int) ( (C1 - 1) ) )  ) ; 
                __context__.SourceCodeLine = 113;
                CURRENTLIST [ COUNT] . PLLNAME  .UpdateValue ( Functions.Mid ( SINGLELINE ,  (int) ( (C1 + 1) ) ,  (int) ( ((C2 - C1) - 1) ) )  ) ; 
                __context__.SourceCodeLine = 114;
                CURRENTLIST [ COUNT] . PLNUMBER  .UpdateValue ( Functions.Mid ( SINGLELINE ,  (int) ( (C3 + 1) ) ,  (int) ( ((C4 - C3) - 1) ) )  ) ; 
                __context__.SourceCodeLine = 116;
                COUNT = (ushort) ( (COUNT + 1) ) ; 
                __context__.SourceCodeLine = 76;
                } 
            
            __context__.SourceCodeLine = 118;
            PRINTDATATOPANEL (  __context__ , "") ; 
            
            }
            
        private int FILLBUFCARTRIDGE (  SplusExecutionContext __context__, ushort _NFILEHANDLE , ushort ROUND , int _POINTER ) 
            { 
            ushort X = 0;
            ushort STEPCOUNT = 0;
            ushort _ERRCODE = 0;
            
            CrestronString _SBUF;
            CrestronString SEARCHING;
            CrestronString [] TEMPTEXT;
            _SBUF  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
            SEARCHING  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
            TEMPTEXT  = new CrestronString[ 51 ];
            for( uint i = 0; i < 51; i++ )
                TEMPTEXT [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
            
            
            __context__.SourceCodeLine = 126;
            _ERRCODE = (ushort) ( FileSeek( (short)( _NFILEHANDLE ) , (uint)( _POINTER ) , (ushort)( 1 ) ) ) ; 
            __context__.SourceCodeLine = 127;
            Trace( "error code from fileseek = {0:d}", (short)_ERRCODE) ; 
            __context__.SourceCodeLine = 129;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( FileRead( (short)( _NFILEHANDLE ) , _SBUF , (ushort)( 4096 ) ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 131;
                TEMPTEXT [ ROUND ]  .UpdateValue ( _SBUF  ) ; 
                __context__.SourceCodeLine = 132;
                X = (ushort) ( 1 ) ; 
                __context__.SourceCodeLine = 133;
                STEPCOUNT = (ushort) ( 1 ) ; 
                __context__.SourceCodeLine = 134;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 4096 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)1; 
                int __FN_FORSTEP_VAL__1 = (int)Functions.ToLongInteger( -( 1 ) ); 
                for ( X  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (X  >= __FN_FORSTART_VAL__1) && (X  <= __FN_FOREND_VAL__1) ) : ( (X  <= __FN_FORSTART_VAL__1) && (X  >= __FN_FOREND_VAL__1) ) ; X  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 136;
                    SEARCHING  .UpdateValue ( Functions.Mid ( TEMPTEXT [ ROUND ] ,  (int) ( X ) ,  (int) ( STEPCOUNT ) )  ) ; 
                    __context__.SourceCodeLine = 137;
                    Trace( "currently searching = {0}", SEARCHING ) ; 
                    __context__.SourceCodeLine = 138;
                    if ( Functions.TestForTrue  ( ( Functions.Find( "\u000d\u000a" , SEARCHING , 1 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 140;
                        Trace( "found round {0:d} lastEOL @ {1:d}", (short)ROUND, (short)X) ; 
                        __context__.SourceCodeLine = 141;
                        _POINTER = (int) ( (_POINTER + X) ) ; 
                        __context__.SourceCodeLine = 142;
                        GLOBALSTORAGE [ ROUND ]  .UpdateValue ( Functions.Left ( TEMPTEXT [ ROUND ] ,  (int) ( X ) )  ) ; 
                        __context__.SourceCodeLine = 143;
                        break ; 
                        } 
                    
                    __context__.SourceCodeLine = 145;
                    STEPCOUNT = (ushort) ( (STEPCOUNT + 1) ) ; 
                    __context__.SourceCodeLine = 134;
                    } 
                
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 149;
                Trace( "file is done") ; 
                __context__.SourceCodeLine = 150;
                _POINTER = (int) ( 2147483647 ) ; 
                } 
            
            __context__.SourceCodeLine = 153;
            return (int)( _POINTER) ; 
            
            }
            
        private void HANDLELOCALFILE (  SplusExecutionContext __context__, CrestronString LOCALFILELOCATION ) 
            { 
            ushort LASTEOL = 0;
            ushort STEPCOUNT = 0;
            ushort X = 0;
            ushort ROUND = 0;
            
            short NFILEHANDLE = 0;
            short READERR = 0;
            
            int POINTER = 0;
            
            CrestronString SEARCHING;
            CrestronString [] TEMPTEXT;
            CrestronString SBUF;
            SEARCHING  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
            SBUF  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
            TEMPTEXT  = new CrestronString[ 51 ];
            for( uint i = 0; i < 51; i++ )
                TEMPTEXT [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
            
            
            __context__.SourceCodeLine = 163;
            StartFileOperations ( ) ; 
            __context__.SourceCodeLine = 165;
            try 
                { 
                __context__.SourceCodeLine = 167;
                NFILEHANDLE = (short) ( FileOpen( LOCALFILELOCATION ,(ushort) Functions.BoolToInt ( (Functions.TestForTrue ( 0 ) || Functions.TestForTrue ( 32768 )) ) ) ) ; 
                __context__.SourceCodeLine = 169;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NFILEHANDLE >= 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 171;
                    ROUND = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 172;
                    POINTER = (int) ( 1 ) ; 
                    __context__.SourceCodeLine = 174;
                    while ( Functions.TestForTrue  ( ( Functions.BoolToInt (POINTER != 2147483647))  ) ) 
                        { 
                        __context__.SourceCodeLine = 176;
                        POINTER = (int) ( FILLBUFCARTRIDGE( __context__ , (ushort)( NFILEHANDLE ) , (ushort)( ROUND ) , (int)( POINTER ) ) ) ; 
                        __context__.SourceCodeLine = 177;
                        ROUND = (ushort) ( (ROUND + 1) ) ; 
                        __context__.SourceCodeLine = 174;
                        } 
                    
                    __context__.SourceCodeLine = 221;
                    Trace( "last pointer found @ {0:d}", (int)POINTER) ; 
                    __context__.SourceCodeLine = 230;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileClose( (short)( NFILEHANDLE ) ) != 0))  ) ) 
                        { 
                        __context__.SourceCodeLine = 232;
                        Trace( "Error closing file\r\n") ; 
                        } 
                    
                    } 
                
                } 
            
            catch (Exception __splus_exception__)
                { 
                SimplPlusException __splus_exceptionobj__ = new SimplPlusException(__splus_exception__, this );
                
                __context__.SourceCodeLine = 238;
                Trace( "Error in File Operation. Check file location and name.") ; 
                
                }
                
                __context__.SourceCodeLine = 241;
                EndFileOperations ( ) ; 
                
                }
                
            private void HANDLEREMOTEFILE (  SplusExecutionContext __context__, CrestronString REMOTEFILELOCATION ) 
                { 
                CrestronString CMDTOCONSOLE;
                CMDTOCONSOLE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 400, this );
                
                
                __context__.SourceCodeLine = 249;
                MakeString ( CMDTOCONSOLE , "fget {0} {1} {2}:{3}\u000d", REMOTEFILELOCATION , LOCALFILE , FTPUSERNAME , FTPPASSWORD ) ; 
                __context__.SourceCodeLine = 251;
                TOCONSOLE  .UpdateValue ( CMDTOCONSOLE  ) ; 
                
                }
                
            private void INITIALIZE (  SplusExecutionContext __context__ ) 
                { 
                
                __context__.SourceCodeLine = 256;
                SEARCHINPROGRESS  .Value = (ushort) ( 1 ) ; 
                __context__.SourceCodeLine = 257;
                HANDLELOCALFILE (  __context__ , LOCALFILE) ; 
                __context__.SourceCodeLine = 258;
                SEARCHINPROGRESS  .Value = (ushort) ( 0 ) ; 
                
                }
                
            object SEARCH_OnPush_0 ( Object __EventInfo__ )
            
                { 
                Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
                try
                {
                    SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                    
                    __context__.SourceCodeLine = 263;
                    SEARCHINPROGRESS  .Value = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 264;
                    PRINTDATATOPANEL (  __context__ , NAMETOFIND) ; 
                    __context__.SourceCodeLine = 265;
                    SEARCHINPROGRESS  .Value = (ushort) ( 0 ) ; 
                    
                    
                }
                catch(Exception e) { ObjectCatchHandler(e); }
                finally { ObjectFinallyHandler( __SignalEventArg__ ); }
                return this;
                
            }
            
        object CLEAR_SEARCH_OnPush_1 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 270;
                SEARCHINPROGRESS  .Value = (ushort) ( 1 ) ; 
                __context__.SourceCodeLine = 271;
                PRINTDATATOPANEL (  __context__ , "") ; 
                __context__.SourceCodeLine = 272;
                SEARCHINPROGRESS  .Value = (ushort) ( 0 ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    public override object FunctionMain (  object __obj__ ) 
        { 
        try
        {
            SplusExecutionContext __context__ = SplusFunctionMainStartCode();
            
            __context__.SourceCodeLine = 277;
            WaitForInitializationComplete ( ) ; 
            __context__.SourceCodeLine = 278;
            while ( Functions.TestForTrue  ( ( 1)  ) ) 
                { 
                __context__.SourceCodeLine = 280;
                try 
                    { 
                    __context__.SourceCodeLine = 282;
                    INITIALIZE (  __context__  ) ; 
                    } 
                
                catch (Exception __splus_exception__)
                    { 
                    SimplPlusException __splus_exceptionobj__ = new SimplPlusException(__splus_exception__, this );
                    
                    
                    }
                    
                    __context__.SourceCodeLine = 278;
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            return __obj__;
            }
            
        
        public override void LogosSplusInitialize()
        {
            SocketInfo __socketinfo__ = new SocketInfo( 1, this );
            InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
            _SplusNVRAM = new SplusNVRAM( this );
            GLOBALSTORAGE  = new CrestronString[ 51 ];
            for( uint i = 0; i < 51; i++ )
                GLOBALSTORAGE [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
            CURRENTLIST  = new PHONELIST[ 31 ];
            for( uint i = 0; i < 31; i++ )
            {
                CURRENTLIST [i] = new PHONELIST( this, true );
                CURRENTLIST [i].PopulateCustomAttributeList( false );
                
            }
            
            SEARCH = new Crestron.Logos.SplusObjects.DigitalInput( SEARCH__DigitalInput__, this );
            m_DigitalInputList.Add( SEARCH__DigitalInput__, SEARCH );
            
            CLEAR_SEARCH = new Crestron.Logos.SplusObjects.DigitalInput( CLEAR_SEARCH__DigitalInput__, this );
            m_DigitalInputList.Add( CLEAR_SEARCH__DigitalInput__, CLEAR_SEARCH );
            
            SEARCHINPROGRESS = new Crestron.Logos.SplusObjects.DigitalOutput( SEARCHINPROGRESS__DigitalOutput__, this );
            m_DigitalOutputList.Add( SEARCHINPROGRESS__DigitalOutput__, SEARCHINPROGRESS );
            
            NAMETOFIND = new Crestron.Logos.SplusObjects.StringInput( NAMETOFIND__AnalogSerialInput__, 30, this );
            m_StringInputList.Add( NAMETOFIND__AnalogSerialInput__, NAMETOFIND );
            
            REMOTEFILE = new Crestron.Logos.SplusObjects.StringInput( REMOTEFILE__AnalogSerialInput__, 250, this );
            m_StringInputList.Add( REMOTEFILE__AnalogSerialInput__, REMOTEFILE );
            
            LOCALFILE = new Crestron.Logos.SplusObjects.StringInput( LOCALFILE__AnalogSerialInput__, 100, this );
            m_StringInputList.Add( LOCALFILE__AnalogSerialInput__, LOCALFILE );
            
            FTPUSERNAME = new Crestron.Logos.SplusObjects.StringInput( FTPUSERNAME__AnalogSerialInput__, 30, this );
            m_StringInputList.Add( FTPUSERNAME__AnalogSerialInput__, FTPUSERNAME );
            
            FTPPASSWORD = new Crestron.Logos.SplusObjects.StringInput( FTPPASSWORD__AnalogSerialInput__, 30, this );
            m_StringInputList.Add( FTPPASSWORD__AnalogSerialInput__, FTPPASSWORD );
            
            TOCONSOLE = new Crestron.Logos.SplusObjects.StringOutput( TOCONSOLE__AnalogSerialOutput__, this );
            m_StringOutputList.Add( TOCONSOLE__AnalogSerialOutput__, TOCONSOLE );
            
            FIRST_NAME = new InOutArray<StringOutput>( 30, this );
            for( uint i = 0; i < 30; i++ )
            {
                FIRST_NAME[i+1] = new Crestron.Logos.SplusObjects.StringOutput( FIRST_NAME__AnalogSerialOutput__ + i, this );
                m_StringOutputList.Add( FIRST_NAME__AnalogSerialOutput__ + i, FIRST_NAME[i+1] );
            }
            
            LAST_NAME = new InOutArray<StringOutput>( 30, this );
            for( uint i = 0; i < 30; i++ )
            {
                LAST_NAME[i+1] = new Crestron.Logos.SplusObjects.StringOutput( LAST_NAME__AnalogSerialOutput__ + i, this );
                m_StringOutputList.Add( LAST_NAME__AnalogSerialOutput__ + i, LAST_NAME[i+1] );
            }
            
            PHONENUMBER = new InOutArray<StringOutput>( 30, this );
            for( uint i = 0; i < 30; i++ )
            {
                PHONENUMBER[i+1] = new Crestron.Logos.SplusObjects.StringOutput( PHONENUMBER__AnalogSerialOutput__ + i, this );
                m_StringOutputList.Add( PHONENUMBER__AnalogSerialOutput__ + i, PHONENUMBER[i+1] );
            }
            
            EMAIL = new InOutArray<StringOutput>( 50, this );
            for( uint i = 0; i < 50; i++ )
            {
                EMAIL[i+1] = new Crestron.Logos.SplusObjects.StringOutput( EMAIL__AnalogSerialOutput__ + i, this );
                m_StringOutputList.Add( EMAIL__AnalogSerialOutput__ + i, EMAIL[i+1] );
            }
            
            
            SEARCH.OnDigitalPush.Add( new InputChangeHandlerWrapper( SEARCH_OnPush_0, false ) );
            CLEAR_SEARCH.OnDigitalPush.Add( new InputChangeHandlerWrapper( CLEAR_SEARCH_OnPush_1, false ) );
            
            _SplusNVRAM.PopulateCustomAttributeList( true );
            
            NVRAM = _SplusNVRAM;
            
        }
        
        public override void LogosSimplSharpInitialize()
        {
            
            
        }
        
        public UserModuleClass_PHONEBOOK_LIST_MODULE ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
        
        
        
        
        const uint SEARCH__DigitalInput__ = 0;
        const uint CLEAR_SEARCH__DigitalInput__ = 1;
        const uint NAMETOFIND__AnalogSerialInput__ = 0;
        const uint REMOTEFILE__AnalogSerialInput__ = 1;
        const uint LOCALFILE__AnalogSerialInput__ = 2;
        const uint FTPUSERNAME__AnalogSerialInput__ = 3;
        const uint FTPPASSWORD__AnalogSerialInput__ = 4;
        const uint SEARCHINPROGRESS__DigitalOutput__ = 0;
        const uint TOCONSOLE__AnalogSerialOutput__ = 0;
        const uint FIRST_NAME__AnalogSerialOutput__ = 1;
        const uint LAST_NAME__AnalogSerialOutput__ = 31;
        const uint PHONENUMBER__AnalogSerialOutput__ = 61;
        const uint EMAIL__AnalogSerialOutput__ = 91;
        
        [SplusStructAttribute(-1, true, false)]
        public class SplusNVRAM : SplusStructureBase
        {
        
            public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
            
            
        }
        
        SplusNVRAM _SplusNVRAM = null;
        
        public class __CEvent__ : CEvent
        {
            public __CEvent__() {}
            public void Close() { base.Close(); }
            public int Reset() { return base.Reset() ? 1 : 0; }
            public int Set() { return base.Set() ? 1 : 0; }
            public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
        }
        public class __CMutex__ : CMutex
        {
            public __CMutex__() {}
            public void Close() { base.Close(); }
            public void ReleaseMutex() { base.ReleaseMutex(); }
            public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
        }
         public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
    }
    
    [SplusStructAttribute(-1, true, false)]
    public class PHONELIST : SplusStructureBase
    {
    
        [SplusStructAttribute(0, false, false)]
        public CrestronString  PLFNAME;
        
        [SplusStructAttribute(1, false, false)]
        public CrestronString  PLLNAME;
        
        [SplusStructAttribute(2, false, false)]
        public CrestronString  PLNUMBER;
        
        [SplusStructAttribute(3, false, false)]
        public CrestronString  PLEMAIL;
        
        
        public PHONELIST( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
        {
            PLFNAME  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 30, Owner );
            PLLNAME  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 30, Owner );
            PLNUMBER  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 30, Owner );
            PLEMAIL  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, Owner );
            
            
        }
        
    }
    
}
