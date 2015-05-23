
namespace DecisionPointBAL.Implementation.Request
{
    public class ServiceTranslationTableRequest
    {
        /// <summary>
        /// get & set parent company id
        /// </summary>
        public string ParentCompanyId { get; set; }
        /// <summary>
        /// get & set parent company id
        /// </summary>
        public string ChildCompanyId { get; set; }
        /// <summary>
        /// get & set parent company id
        /// </summary>
        public int CreatedByid { get; set; }
        /// <summary>
        /// get & set child company id
        /// </summary>
        public string MappedServices { get; set; }
    }
}
