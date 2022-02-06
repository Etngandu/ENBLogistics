using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Test.Logistics.Entities
{
  public  enum TypePicking
    {
         /// <summary>
         /// Determines the type of picking.
         /// </summary>
    
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a  Pick_to_Light type.
        /// </summary>
        [Display(Name = "Pick to Light")]
        Pick_to_Light = 1,

        /// <summary>
        /// Indicates a Scanner type.
        /// </summary>
        Scanner = 2,

        /// <summary>
        /// Indicates a  Voice_Direct_Picking type.
        /// </summary>
        [Display(Name = "Voice Direct Picking")]
        Voice_Direct_Picking = 3,

        /// <summary>
        /// Indicates a  Visual_RF_Scanning type.
        /// </summary>
        [Display(Name = "Visual RF Scanning")]
        Visual_RF_Scanning = 4
    }
}

