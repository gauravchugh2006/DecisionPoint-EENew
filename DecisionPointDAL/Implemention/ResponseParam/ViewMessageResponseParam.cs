

// ******************************************************************************************************************************
//                                                 class:ViewMessageResponseParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 27, 2014     |Sumit saurav           | This class holds the interaction logic for ViewMessageResponseParam.cs
// ******************************************************************************************************************************

using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class ViewMessageResponseParam
    {
        #region << Public Variables >>
        #region ReplyMessage
        /// <summary>
        /// Used for get reply body of message
        /// </summary>
        public string replyBody { get; set; }
        /// <summary>
        /// Used for get message recipient person name
        /// </summary>
        public string replyto { get; set; }
        /// <summary>
        /// Used for get message delivered person name
        /// </summary>
        public string replyfrom { get; set; }
        /// <summary>
        /// Used for get date of message reply
        /// </summary>
        public DateTime? replysentdate { get; set; }
        /// <summary>
        /// get set messageStatus
        /// </summary>
        public string messageStatus { get; set; }
        #endregion
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public ViewMessageResponseParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
