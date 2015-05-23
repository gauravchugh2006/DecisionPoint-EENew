using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ******************************************************************************************************************************
//                                                 class:DSTInviteRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// 03 09, 2015     |Bobi           | This class holds the interaction logic for APIProductRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class APIProductRequestParam
    {
        /// <summary>
        /// get & set ProductId
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// get & set ProductName
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// get & set ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// get & set SubClientId
        /// </summary>
        public string SubClientId { get; set; }
    }
}
