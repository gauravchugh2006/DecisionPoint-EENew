using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{
  public class MailMatrixResponseParam
    {

        public string Recevier { get; set; }
        public string RecevierEmail { get; set; }
        public string RecevierPassword { get; set; }
        public string Sender { get; set; }
        public int Flow { get; set; }
        public int SenderId { get; set; }
        public string Category { get; set; }
        public int TblId { get; set; }
        public int DocId { get; set; }
        public int RecevierId { get; set; }
        public int Type { get; set; }
        public string UserType { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
