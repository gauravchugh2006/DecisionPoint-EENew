using System;

namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class CommDetailsResponseParam
    {
        #region DocumentDetailsTable

        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string DocTitle { get; set; }
        /// <summary>
        /// get & set Reference
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// get & set Group
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// get & set DueDate
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// get & set Effectivedate
        /// </summary>
        public DateTime? Effectivedate { get; set; }
        /// <summary>
        /// get & set Retake
        /// </summary>
        public string Retake { get; set; }
        /// <summary>
        /// get & set policyno
        /// </summary>
        public string policyno { get; set; }
        /// <summary>
        /// get & set HOC
        /// </summary>
        public string HOC { get; set; }
        /// <summary>
        /// get & set DaysOfCompletion
        /// </summary>
        public int DaysOfCompletion { get; set; }
        /// <summary>
        /// get & set Reference
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// get & set policyno
        /// </summary>
        public int versionno { get; set; }
        /// <summary>
        /// get & set Reqnewhire
        /// </summary>
        public bool Reqnewhirestaff { get; set; }
        /// <summary>
        /// get & set Reqnewhire
        /// </summary>
        public bool Reqnewhireic { get; set; }
        /// <summary>
        /// get & set Reqnewhire
        /// </summary>
        public bool Reqnewhirevendor { get; set; }
        /// <summary>
        /// get & set DocTitles
        /// </summary>
        public string DocTitles { get; set; }
        /// <summary>
        /// get & set VideoTitles
        /// </summary>
        public string VideoTitles { get; set; }
        /// <summary>
        /// get & set ScormTitles
        /// </summary>
        public string ScormTitles { get; set; }
        #endregion

        #region CommTestRules

        /// <summary>
        /// get & set RandQues
        /// </summary>
        public bool RandQues { get; set; }
        /// <summary>
        /// get & set RandAns
        /// </summary>
        public bool RandAns { get; set; }

        /// <summary>
        /// get & set ReqReTest
        /// </summary>
        public bool ReqReTest { get; set; }
        /// <summary>
        /// get & set ShowWrongeAns
        /// </summary>
        public bool ShowWrongeAns { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        public string Instruction { get; set; }
        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string PassingScore { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string Attempts { get; set; }
        /// <summary>
        /// get & set Testruleid
        /// </summary>
        public int Testruleid { get; set; }
        #endregion

        #region CommLinks

        /// <summary>
        /// get & set LinkURl
        /// </summary>
        public string LinkURl { get; set; }
        /// <summary>
        /// get & set LinkURl
        /// </summary>
        public int LinkId { get; set; }


        #endregion

        #region CommContents

        /// <summary>
        /// get & set Instruction
        /// </summary>
        public string FileLoc { get; set; }
        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string Filetitle { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string Filetype { get; set; }
        /// <summary>
        /// get & set Contentid
        /// </summary>
        public int Contentid { get; set; }
        #endregion

        #region Commreqaction

        /// <summary>
        /// get & set LinkURl
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// get & set Reqactionid
        /// </summary>
        public int Reqactionid { get; set; }

        #endregion

        #region CommRecipient

        /// <summary>
        /// get & set RecipientUserId
        /// </summary>
        public string RecipientUserId { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string fname { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string lname { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string vendor { get; set; }
        /// <summary>
        /// get & set email id
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string phone { get; set; }
        #endregion

        #region CommAssesment

        /// <summary>
        /// get & set RecipientUserId
        /// </summary>
        public int AssesmentId { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string Question { get; set; }

        #endregion

        #region CommAnswers

        /// <summary>
        /// get & set RecipientUserId
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public bool IsCorrect { get; set; }

        #endregion
    }
}
