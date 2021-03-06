﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Compression
{
    /// <summary>
    /// Exposes <see cref="ICompressor"/> classes to the factory. This class
    /// cannot be inherited.
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false )]
    public sealed class CompressorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressorAttribute"/>
        /// class.
        /// </summary>
        /// <param name="identifier">The unique identifier of the compressor.</param>
        /// <exception cref="ArgumentException">identifier is null or
        /// empty.</exception>
        public CompressorAttribute( string identifier )
        {
            if( string.IsNullOrEmpty( identifier ) )
            {
                throw new ArgumentException( "identifier" );
            }

            Identifier = identifier;
        }

        /// <summary>
        /// Gets the unique identifier for the compressor.
        /// </summary>
        public string Identifier
        {
            get;
            private set;
        }
    }
}
