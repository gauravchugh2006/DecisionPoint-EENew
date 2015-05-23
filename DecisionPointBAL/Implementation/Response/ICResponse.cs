
using System;
namespace DecisionPointBAL.Implementation.Response
{
    //*****************************************************************************************************************************************
  //   Class used for set the document details
  //*****************************************************************************************************************************************
  public class ICResponse
  {
      #region Internalstaff & vendors
      /// <summary>
      /// get & set id
      /// </summary>
      public int? Id { get; set; }
      /// <summary>
      /// get & set name
      /// </summary>
      public string fname { get; set; }
      /// <summary>
      /// get & set name
      /// </summary>
      public string mname { get; set; }
      /// <summary>
      /// get & set name
      /// </summary>
      public string lname { get; set; }
      /// <summary>
      /// get & set email id
      /// </summary>
      public string emailId { get; set; }

      /// <summary>
      /// get & set role
      /// </summary>
      public string service { get; set; }
      /// <summary>
      /// get & set role
      /// </summary>
      public string zipcode { get; set; }
      /// <summary>
      /// get & set phone
      /// </summary>
      public string phone { get; set; }
      /// <summary>
      /// get & set InvitationStatus
      /// </summary>
      public bool InvitationStatus { get; set; }


      /// <summary>
      /// get & set isactive
      /// </summary>
      public bool IsActive { get; set; }
      /// <summary>
      /// get & set businessName
      /// </summary>
      public string businessName { get; set; }
      /// <summary>
      /// get & set companyid
      /// </summary>
      public string companyid { get; set; }
      /// <summary>
      /// get & set VTId
      /// </summary>
      public int VTId { get; set; }/// <summary>
      /// get & set VType
      /// </summary>
      public string VType { get; set; }
      /// <summary>
      /// get & set BGStatus
      /// </summary>
      public string BGStatus { get; set; }
      public string Source { get; set; }
      public int? ICUserId { get; set; }
      public string ICCompanyId { get; set; }
      public string SterlingOrderId { get; set; }
      public bool IsRegistred { get; set; }
      /// <summary>
      /// get & set LastInviteMailDate
      /// </summary>
      public DateTime? LastInviteMailDate { get; set; }
      #endregion
  }
}
