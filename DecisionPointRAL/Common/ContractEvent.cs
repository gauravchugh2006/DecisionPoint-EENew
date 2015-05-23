using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


// ******************************************************************************************************************************
//                                                 class:ContractEvent.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Dec. 24, 2014     |Sumit Saurav           | This class holds the interaction logic for ContractEvent.cs
// ******************************************************************************************************************************

namespace DecisionPointCAL.Common
{
    public class ContractEvent
    {
        #region << Public Variables >>
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string EventTitle { get; set; }

        public string Paragraphs { get; set; }

        public string Sections { get; set; }

        public string SubSections { get; set; }
        public int ContractEventId { get; set; }

        public DateTime? EventDate { get; set; }

        public int EventDateReminder { get; set; }
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Notes { get; set; }
        public string AssignedUserIds { get; set; }
        
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public ContractEvent()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
