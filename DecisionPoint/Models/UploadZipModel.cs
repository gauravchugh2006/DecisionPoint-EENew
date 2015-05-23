using System.Collections.Generic;
using System.Web;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class UploadZipModel
    {
        private bool defaultValueForFirstRowHeader = true;

        public HttpPostedFileBase File { get; set; }

        public IList<string[]> UploadedZipList { get; set; }
        public IList<ZipResponse> ZipList { get; set; }
        public IList<ZipResponse> CompanyBasedZipList { get; set; }
        public string Seprator { get; set; }
        public bool FirstRowHeader { get { return defaultValueForFirstRowHeader; } set { defaultValueForFirstRowHeader = value; } }
    }
}