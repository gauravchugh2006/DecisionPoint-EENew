using System;

namespace DecisionPointBAL.Implementation.Response
{
    public class UserTrainingMaterialsResponse
    {
        public int? DocID
        {
            get;
            set;
        }

        public string FileLocation
        {
            get;
            set;
        }

        public string FileType
        {
            get;
            set;
        }

        public string FileTitle
        {
            get;
            set;
        }

        public int? DocMsgID
        {
            get;
            set;
        }

        public int? CreatedBy
        {
            get;
            set;
        }

        public DateTime? CreatedDate
        {
            get;
            set;
        }

        public int? ModiifiedBy
        {
            get;
            set;
        }

        public DateTime? ModifiedDate
        {
            get;
            set;
        }

        public bool? status
        {
            get;
            set;
        }
        public Nullable<DateTime> CompletionDate { get; set; }
        public Nullable<DateTime> ViewDate { get; set; }
        public int TimeSpan { get; set; }
    }
}
