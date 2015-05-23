using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ******************************************************************************************************************************
//                                                 class:CommContentRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 29, 2014     |Sumit Saurav            | This class holds the interaction logic for CommContentRequest.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Request
{
    public class CommContentRequest
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        public int docId { get; set; }
        public string files { get; set; }
        public string filetype { get; set; }
        public int userId { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string scormname { get; set; }
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public CommContentRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
