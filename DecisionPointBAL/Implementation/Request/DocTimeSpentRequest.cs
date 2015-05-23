using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ******************************************************************************************************************************
//                                                 class:DocTimeSpentRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 29, 2014     |sumit saurav            | This class holds the interaction logic for DocTimeSpentRequest.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Request
{
    public class DocTimeSpentRequest
    {
        #region << Public Variables >>

        public int userid { get; set; }

        public int docid { get; set; }

        public int DeleiverUserId { get; set; }

        public int timespan { get; set; }

        public int Completion { get; set; }

        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public DocTimeSpentRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
