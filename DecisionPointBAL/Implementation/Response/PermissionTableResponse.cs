using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Response
{
   public class PermissionTableResponse
    {
        /// <summary>
        /// get & set Funtional Permission
        /// </summary>
        public string FuntionalPermission { get; set; }
        /// <summary>
        /// get & set Table Id
        /// </summary>
        public int TableId { get; set; }
        /// <summary>
        /// get & set FunPermissionIds
        /// </summary>
        public string FunPermissionIds { get; set; }
        /// <summary>
        /// get & set TitleIds
        /// </summary>
        public string TitleIds { get; set; }
        /// <summary>
        /// get & set Title Id
        /// </summary>
        public int TitleId { get; set; }
        /// <summary>
        /// get & set Funtional Permission Id
        /// </summary>
        public int FunPerId { get; set; }
        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string TitleName { get; set; }
        /// <summary>
        /// get & set CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// get & set ModifiedBy
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// get & set CreatedCompanyId
        /// </summary>
        public int CreatedCompanyId { get; set; }
        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string TabName { get; set; }
        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string TabId { get; set; }
        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string TabUrl { get; set; }
        /// <summary>
        /// get & set Title Name
        /// </summary>
        public int TabType { get; set; }
        /// <summary>
        /// get & set IsMainTab
        /// </summary>
        public bool IsMainTab { get; set; }
        /// <summary>
        /// get & set MainTabName
        /// </summary>
        public string MainTabName { get; set; }
        /// <summary>
        /// get & set TabSectionName
        /// </summary>
        public string TabSectionName { get; set; }
    }
}
