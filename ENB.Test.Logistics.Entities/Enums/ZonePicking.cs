using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Test.Logistics.Entities
{
    public enum ZonePicking
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates an AST  record.
        /// </summary>
        AST = 1,

        /// <summary>
        /// Indicates a BPX  record.
        /// </summary>
        BPX = 3,

         /// <summary>
         /// Indicates an ICX value.
         /// </summary>
        ICX = 4,

        /// <summary>
        /// Indicates a KEX  record.
        /// </summary>
        KEX = 5,

        /// <summary>
        /// Indicates a PIM  record.
        /// </summary>
        PIM = 6,
         /// <summary>
         /// Indicates a KDA  record.
         /// </summary>
        KDA = 7
    }
}
