﻿

// ******************************************************************************************************************************
//                                                 class:CategoryRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 29, 2014     |Sumit Saurav          | This class holds the interaction logic for CategoryRequest.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Request
{
    public class CategoryRequest
    {
        #region << Public Variables >>

        public string categoryName { get; set; }
        public string CompanyId { get; set; }
        public int UserId { get; set; }
        public int sourceId { get; set; }
        public int groupId { get; set; }
        public int categoryId { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public CategoryRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
