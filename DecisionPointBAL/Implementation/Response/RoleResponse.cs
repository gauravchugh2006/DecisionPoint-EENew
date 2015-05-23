
namespace DecisionPointBAL.Implementation.Response
{
    public class RoleResponse
    {
        #region Role
        /// <summary>
        /// get role id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// get role name
        /// </summary>
        public string RoleName { get; set; }
        #endregion

        #region Flow
        /// <summary>
        /// get flow Id
        /// </summary>
        public int flowId { get; set; }

        /// <summary>
        /// get flow Name
        /// </summary>
        public string flowName { get; set; }
        #endregion

        #region Permission
        /// <summary>
        /// get permission Id
        /// </summary>
        public int permissionId { get; set; }

        /// <summary>
        /// get permission Name
        /// </summary>
        public string permissionName { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// get & set permission Id
        /// </summary>
        public int serviceId { get; set; }

        /// <summary>
        /// get & set permission Name
        /// </summary>
        public string serviceName { get; set; }
        #endregion

        #region Client
        /// <summary>
        /// get & set permission Id
        /// </summary>
        public int clientId { get; set; }

        /// <summary>
        /// get & set permission Name
        /// </summary>
        public string clientName { get; set; }
        #endregion
    }
}
