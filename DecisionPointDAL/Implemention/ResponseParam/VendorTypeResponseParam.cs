using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointDAL.Implemention.ResponseParam
{
   public class VendorTypeResponseParam
    {
       /// <summary>
       /// get and set VendorType Name
       /// </summary>
       public string VendorTypeName {get; set;}
       /// <summary>
       /// get and set VendorType CompanyId
       /// </summary>
       public string UserCompanyId { get; set; }
       /// <summary>
       /// get and set VendorType CompanyId
       /// </summary>
       public string CreatorCompanyId { get; set; }
       /// <summary>
       /// get and set IsUserBased
       /// </summary>
       public bool IsUserBased { get; set; }
       /// <summary>
       /// get and set VendorType tblId
       /// </summary>
       public int tblId { get; set; }
      
       /// <summary>
       /// get and set VendorTypeId
       /// </summary>
       public int VendorTypeId { get; set; }
    }
}
