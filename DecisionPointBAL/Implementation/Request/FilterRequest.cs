﻿

// ******************************************************************************************************************************
//                                                 class:FilterRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 29, 2014     |Sumit Saurav           | This class holds the interaction logic for FilterRequest.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Request
{
    public class FilterRequest
    {
        #region << Public Variables >>
        public string CompanyId { get; set; }
        public int type { get; set; }
        public string rolefilter { get; set; }
        public string servicefilter { get; set; }
        public string titlefilter { get; set; }
        public string locationfilter { get; set; }
        public bool isActive { get; set; }
        public string typefilter { get; set; }
        public string filtertype { get; set; }
        public string uType { get; set; }
        public int DocId { get; set; }
        public int UserId { get; set; } 

        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public FilterRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}