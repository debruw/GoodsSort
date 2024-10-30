using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


    public static class NumberHelper
    {
        #region Variables

        public static Dictionary<int,string> numExponentialTable;

        static string periodString = ".";
        static string whiteSpaceString = " ";
        static string dashString = "-";
        static string zeroString = "0";
        static StringBuilder builder = new StringBuilder( );
        static string[ ] optimizedNumberToStringArray;

        #endregion

        /// <summary>
        /// Converts a float to string using the scientific connotation (K,M,B,T,aa,etc)
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>The converted string</returns>
        public static string ToStringScientific( this float number )
        {
            InitializeToString( );

            return FormatNumber( number );
        }

        /// <summary>
        /// Converts a float to string using the scientific connotation (K,M,B,T,aa,etc)
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>The converted string</returns>
        public static string FormatNumber( float num )
        {
            int maxAllowed = 3;
            int decimals = 0;

            if( numExponentialTable == null )
            {
                numExponentialTable = new Dictionary<int , string>( );
                numExponentialTable.Add( 3 , "K" );
                numExponentialTable.Add( 6 , "M" );
                numExponentialTable.Add( 9 , "B" );
                numExponentialTable.Add( 12 , "T" );
                numExponentialTable.Add( 15 , "aa" );
                numExponentialTable.Add( 18 , "bb" );
                numExponentialTable.Add( 21 , "cc" );
                numExponentialTable.Add( 24 , "dd" );
                numExponentialTable.Add( 27 , "ee" );
                numExponentialTable.Add( 30 , "ff" );
                numExponentialTable.Add( 33 , "gg" );
                numExponentialTable.Add( 36 , "hh" );
                numExponentialTable.Add( 39 , "ii" );
                numExponentialTable.Add( 42 , "jj" );
                numExponentialTable.Add( 45 , "kk" );
                numExponentialTable.Add( 48 , "ll" );
                numExponentialTable.Add( 51 , "mm" );
                numExponentialTable.Add( 54 , "nn" );
                numExponentialTable.Add( 57 , "oo" );
                numExponentialTable.Add( 60 , "pp" );
                numExponentialTable.Add( 63 , "qq" );
                numExponentialTable.Add( 66 , "rr" );
                numExponentialTable.Add( 69 , "ss" );
                numExponentialTable.Add( 72 , "tt" );
            }

            builder.Length = 0;
            bool negative = num < 0;
            float n = Mathf.Abs ( Mathf.Floor (num) );

            int mult = (int)Mathf.Floor ( Mathf.Log10(n) );
            if( mult < maxAllowed )
            {
                if( decimals == 0 )
                    return GetNumberOptimized( ( int ) n );
                else
                {
                    int aux = decimals * 10;
                    return builder.Append( GetNumberOptimized( ( int ) n ) ).Append( periodString ).Append( GetNumberOptimized( ( ( int ) ( num * aux ) ) % aux ) ).ToString( );
                }

            }
            else
            {
                mult = Mathf.FloorToInt( mult / maxAllowed ) * maxAllowed;
                int left =  Mathf.FloorToInt( n/Mathf.Pow(10,mult) );
                int right = Mathf.FloorToInt( 100 * ( n / Mathf.Pow( 10 , mult ) - left) );

                if( Mathf.Floor( Mathf.Log10( left ) + 1 ) >= 4 ) return builder.Append( ( negative ? dashString : string.Empty ) ).Append( GetNumberOptimized( left ) ).Append( whiteSpaceString ).Append( numExponentialTable[ mult ] ).ToString( );
                else return builder.Append( ( negative ? dashString : string.Empty ) ).Append( GetNumberOptimized( left ) ).Append( periodString ).Append( ( right < 10 ? zeroString : string.Empty ) ).Append( right + whiteSpaceString + numExponentialTable[ mult ] ).ToString( );
            }
        }

        private static void InitializeToString( )
        {
            if( optimizedNumberToStringArray == null )
            {
                optimizedNumberToStringArray = new string[ 1000 ];
                for( int i = 0 ; i < optimizedNumberToStringArray.Length ; i++ )
                {
                    optimizedNumberToStringArray[ i ] = i.ToString( );
                }
            }
        }

        private static string GetNumberOptimized( int number )
        {
            if( number >= 0 && number < optimizedNumberToStringArray .Length )
                return optimizedNumberToStringArray[ number ];

            return string.Empty;
        }
    }
