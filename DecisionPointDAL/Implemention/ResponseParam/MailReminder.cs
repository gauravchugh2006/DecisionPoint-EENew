using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{
  public  class MailReminder
    {
        public int ID { get; set; }
        public int flow { get; set; }
        public string EmailID { get; set; }
        public string BusinessName { get; set; }
        public string name { get; set; }
        public string Fname { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public int SenderId { get; set; }
        public int Count { get; set; }
        public string Catagory { get; set; }
        public int DocId { get; set; }
        public int TblId { get; set; }
        public string UserType { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
