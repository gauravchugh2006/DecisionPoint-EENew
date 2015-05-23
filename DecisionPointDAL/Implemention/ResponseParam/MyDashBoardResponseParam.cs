using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointDAL.Implemention.ResponseParam
{
   public class MyDashBoardResponseParam
    {

       public IList<UserDashBoardResponseParam> IncomFromCompCommAlerts { get; set; }

       public IList<UserDashBoardResponseParam> IncomFromOutCompCommAlerts { get; set; }

       public IList<UserDashBoardResponseParam> CompanyBasedCommAlerts { get; set; }

       public IList<ProfileAlertResponseParam> ProfileDetailsAlerts { get; set; }

       public IList<ReceiverReqDocResponseParam> JCRDetailsAlerts { get; set; }
       public IList<CreateContractResponseParam> ContractsAlerts { get; set; }

       public IList<InviteResponseParam> IncomInviteAlerts { get; set; }

       public IList<InviteResponseParam> OutgoICVendorInviteAlerts { get; set; }
       public IList<InviteResponseParam> OutgoStaffInviteAlerts { get; set; }
    }
   public class ProfileAlertResponseParam
   {
       private int defaultCoverageAreadetail = 0;
       private int defaultServiecdetail = 0;
       private int defaulttitledetail = 0;
       private int defaultstaffdetail = 0;
       private int defaulticdetail = 0;
       public int CoverageAreadetail { get { return defaultCoverageAreadetail; } set { defaultCoverageAreadetail = value; } }
       public int Serviecdetail { get { return defaultServiecdetail; } set { defaultServiecdetail = value; } }
       public int Staffdetail { get { return defaultstaffdetail; } set { defaultstaffdetail = value; } }
       public int Titledetail { get { return defaulttitledetail; } set { defaulttitledetail = value; } }
       public int ICdetail { get { return defaulticdetail; } set { defaulticdetail = value; } }
   }
}
