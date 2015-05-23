using DecisionPointDAL.Implemention.ResponseParam;
using System.Collections.Generic;

namespace DecisionPointBAL.Implementation.Response
{
    public class MyDashBoardResponse
    {
        public IList<UserDashBoardResponse> IncomFromCompCommAlerts { get; set; }

        public IList<UserDashBoardResponse> IncomFromOutCompCommAlerts { get; set; }

        public IList<UserDashBoardResponse> CompanyBasedCommAlerts { get; set; }

        public IList<ProfileAlertResponse> ProfileDetailsAlerts { get; set; }

        public IList<ReceiverReqDocResponse> JCRDetailsAlerts { get; set; }
        public IList<CreateContractResponse> ContractsAlerts { get; set; }
        public IList<InviteResponse> IncomInviteAlerts { get; set; }

        public IList<InviteResponse> OutgoICVendorInviteAlerts { get; set; }
        public IList<InviteResponse> OutgoStaffInviteAlerts { get; set; }
    }
    public class ProfileAlertResponse
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
