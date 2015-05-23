using DecisionPointCAL.Common;
using DecisionPointDAL.Implemention.RequestParam;
using DecisionPointDAL.Implemention.ResponseParam;
using DecisionPointDAL.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointDAL.Implemention.RepositoryClasses
{
    public class AdditionalRequirementRep
    {
        #region Global Variable
        int icTypeId = 0;
        int userId = 0;
        string companyId = string.Empty;
        DateTime currentdate = System.DateTime.Now.Date;
        #endregion
        #region Global Object
        DecisionPointEntities objDecisionPointEntities = null;
        #endregion
        /// <summary>
        /// Usd for Save Additiona Credentials from JCR
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 May 2015</CreatedDate>
        public int SaveAdditionalReqFromJCR(LicenseInsuranceRequestParam objLicInsRequestParam)
        {
            int addReqId = 0;
            try
            {
                foreach (var client in objLicInsRequestParam.ClientIds)
                {
                    foreach (var icType in objLicInsRequestParam.ICTypeIds)
                    {
                        using (objDecisionPointEntities = new DecisionPointEntities())
                        {
                            userId = Convert.ToInt32(client.Split(char.Parse(Shared.Colon))[0], CultureInfo.InvariantCulture);
                            companyId = client.Split(char.Parse(Shared.Colon))[1];
                            icTypeId = Convert.ToInt32(icType, CultureInfo.InvariantCulture);
                            var chkAddAkDetails = (from reqdoc in objDecisionPointEntities.DP_AdditionalReqMaster
                                                   where reqdoc.CreatorCompanyId == objLicInsRequestParam.CompanyId
                                                   && reqdoc.UserId == userId &&
                                                   reqdoc.CompanyId == companyId &&
                                                   reqdoc.ICTypeId == icTypeId
                                                   select reqdoc).FirstOrDefault();
                            if (chkAddAkDetails != null)
                            {
                                chkAddAkDetails.ICTypeId = Convert.ToInt32(icType, CultureInfo.InvariantCulture);
                                chkAddAkDetails.IsStaffTitle = objLicInsRequestParam.IsStaffTitle;
                                chkAddAkDetails.UserId = Convert.ToInt32(client.Split(char.Parse(Shared.Colon))[0], CultureInfo.InvariantCulture);
                                chkAddAkDetails.CompanyId = client.Split(char.Parse(Shared.Colon))[1];
                                chkAddAkDetails.CreatorCompanyId = objLicInsRequestParam.CompanyId;
                                chkAddAkDetails.CreatedBy = objLicInsRequestParam.UserId;
                                chkAddAkDetails.CreatedDate = currentdate;
                                chkAddAkDetails.IsActive = true;
                                chkAddAkDetails.IsDeleted = false;
                                chkAddAkDetails.Review = objLicInsRequestParam.Review;
                                chkAddAkDetails.Title = objLicInsRequestParam.Title;
                                chkAddAkDetails.UploadedBy = objLicInsRequestParam.UploadedBy;
                                chkAddAkDetails.ExpirationDate = objLicInsRequestParam.ExpirationDate;
                                addReqId = chkAddAkDetails.Id;
                                //delete ack details as per req doc Id
                                var resack = (from reqdocack in objDecisionPointEntities.DP_AdditionalReqAck
                                              where reqdocack.AddReqId == chkAddAkDetails.Id
                                              select reqdocack).ToList();
                                foreach (var item in resack)
                                {
                                    objDecisionPointEntities.DP_AdditionalReqAck.Remove(item);
                                }
                                //remove uploaded documents by child user[Who received the JCR]
                                var insUploadDocs = (from profLicUploadDoc in objDecisionPointEntities.DP_AdditionalReqUploadDocs
                                                     where profLicUploadDoc.AddReqMapId == objLicInsRequestParam.LicInsMapId
                                                     select profLicUploadDoc).ToList();
                                if (!object.Equals(insUploadDocs, null))
                                {
                                    foreach (var item in insUploadDocs)
                                    {
                                        objDecisionPointEntities.DP_AdditionalReqUploadDocs.Remove(item);
                                    }
                                }
                            }
                            else
                            {
                                //Insert req doc details for sender in database
                                DP_AdditionalReqMaster objDPAdditionalReqMaster = new DP_AdditionalReqMaster()
                                {
                                    ICTypeId = Convert.ToInt32(icType, CultureInfo.InvariantCulture),
                                    IsStaffTitle = objLicInsRequestParam.IsStaffTitle,
                                    UserId = Convert.ToInt32(client.Split(char.Parse(Shared.Colon))[0], CultureInfo.InvariantCulture),
                                    CompanyId = client.Split(char.Parse(Shared.Colon))[1],
                                    CreatorCompanyId = objLicInsRequestParam.CompanyId,
                                    CreatedBy = objLicInsRequestParam.UserId,
                                    CreatedDate = currentdate,
                                    IsActive = true,
                                    IsDeleted = false,
                                    Review = objLicInsRequestParam.Review,
                                    Title = objLicInsRequestParam.Title,
                                    UploadedBy = objLicInsRequestParam.UploadedBy,
                                    ExpirationDate = objLicInsRequestParam.ExpirationDate

                                };
                                objDecisionPointEntities.DP_AdditionalReqMaster.Add(objDPAdditionalReqMaster);
                                objDecisionPointEntities.SaveChanges();
                                addReqId = objDPAdditionalReqMaster.Id;
                            }


                            #region DocUpload and Remove

                            List<string> DocLoclist = new List<string>();
                            if (!string.IsNullOrEmpty(objLicInsRequestParam.UploadedDoc))
                            {
                                DocLoclist = objLicInsRequestParam.UploadedDoc.Split(';').ToList();
                            }

                            foreach (var item in DocLoclist)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    string[] str = item.Split(char.Parse(Shared.Hash));
                                    var DocLocation = str[0];
                                    int docSequence = Convert.ToInt32(str[1]);
                                    DP_AdditionalReqUploadDocs objDP_AdditionalReqUploadDocs = new DP_AdditionalReqUploadDocs()
                                    {
                                        AddReqMapId = objLicInsRequestParam.LicInsMapId,
                                        DocLoc = DocLocation,
                                        DocSeqNo = docSequence,
                                        UserId = objLicInsRequestParam.UserId,
                                        CompanyId = objLicInsRequestParam.CompanyId,
                                        AddReqId = addReqId
                                    };
                                    objDecisionPointEntities.DP_AdditionalReqUploadDocs.Add(objDP_AdditionalReqUploadDocs);
                                }

                            }
                            #endregion
                            if (!object.Equals(objLicInsRequestParam.Acknowleagement, null))
                            {
                                //Save Req doc Ack in database
                                foreach (var item in objLicInsRequestParam.Acknowleagement)
                                {
                                    DP_AdditionalReqAck objDPAdditionalReqAck = new DP_AdditionalReqAck()
                                    {
                                        AddReqId = addReqId,
                                        IsDeleted = false,
                                        Ackknow = item
                                    };
                                    objDecisionPointEntities.DP_AdditionalReqAck.Add(objDPAdditionalReqAck);
                                }
                                
                            }
                            objDecisionPointEntities.SaveChanges();
                        }
                        //Sent Professional License to Recipients
                        SentAdditionalReqTorecipients(addReqId, objLicInsRequestParam, icType);
                    }

                }

            }

            catch
            {
                throw;
            }
            return addReqId;
        }
        /// <summary>
        /// Used for Save Additional Credentials from profile
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        ///  /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 May 2015</CreatedDate>
        public int SaveAdditionalReqFromProfile(LicenseInsuranceRequestParam objLicInsRequestParam)
        {
            int addReqId = 0;
            try
            {

                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    userId = objLicInsRequestParam.UserId;
                    companyId = objLicInsRequestParam.CompanyId;
                    icTypeId = objLicInsRequestParam.ICTypeId;
                    var chkAddAkDetails = (from reqdoc in objDecisionPointEntities.DP_AdditionalReqMaster
                                           where reqdoc.CreatorCompanyId == objLicInsRequestParam.CompanyId
                                           && reqdoc.UserId == userId &&
                                           reqdoc.CompanyId == companyId &&
                                           reqdoc.ICTypeId == icTypeId
                                           select reqdoc).FirstOrDefault();
                    if (chkAddAkDetails != null)
                    {
                        chkAddAkDetails.ICTypeId = icTypeId;
                        chkAddAkDetails.UserId = userId;
                        chkAddAkDetails.CompanyId = companyId;
                        chkAddAkDetails.CreatorCompanyId = objLicInsRequestParam.CompanyId;
                        chkAddAkDetails.CreatedBy = objLicInsRequestParam.UserId;
                        chkAddAkDetails.CreatedDate = currentdate;
                        chkAddAkDetails.IsActive = true;
                        chkAddAkDetails.IsDeleted = false;
                        chkAddAkDetails.Review = objLicInsRequestParam.Review;
                        chkAddAkDetails.Title = objLicInsRequestParam.Title;
                        chkAddAkDetails.UploadedBy = objLicInsRequestParam.UploadedBy;
                        addReqId = chkAddAkDetails.Id;
                        //delete ack details as per req doc Id
                        var resack = (from reqdocack in objDecisionPointEntities.DP_AdditionalReqAck
                                      where reqdocack.AddReqId == chkAddAkDetails.Id
                                      select reqdocack).ToList();
                        foreach (var item in resack)
                        {
                            objDecisionPointEntities.DP_AdditionalReqAck.Remove(item);
                        }
                        //remove uploaded documents by child user[Who received the JCR]
                        var insUploadDocs = (from profLicUploadDoc in objDecisionPointEntities.DP_AdditionalReqUploadDocs
                                             where profLicUploadDoc.AddReqMapId == objLicInsRequestParam.LicInsMapId
                                             select profLicUploadDoc).ToList();
                        if (!object.Equals(insUploadDocs, null))
                        {
                            foreach (var item in insUploadDocs)
                            {
                                objDecisionPointEntities.DP_AdditionalReqUploadDocs.Remove(item);
                            }
                        }
                    }
                    else
                    {
                        //Insert req doc details for sender in database
                        DP_AdditionalReqMaster objDPAdditionalReqMaster = new DP_AdditionalReqMaster()
                        {
                            ICTypeId = icTypeId,
                            UserId = userId,
                            CompanyId = companyId,
                            CreatorCompanyId = objLicInsRequestParam.CompanyId,
                            CreatedBy = objLicInsRequestParam.UserId,
                            CreatedDate = currentdate,
                            IsActive = true,
                            IsDeleted = false,
                            Review = objLicInsRequestParam.Review,
                            Title = objLicInsRequestParam.Title,
                            UploadedBy = objLicInsRequestParam.UploadedBy

                        };
                        objDecisionPointEntities.DP_AdditionalReqMaster.Add(objDPAdditionalReqMaster);
                        objDecisionPointEntities.SaveChanges();
                        addReqId = objDPAdditionalReqMaster.Id;
                    }


                    #region DocUpload and Remove

                    List<string> DocLoclist = new List<string>();
                    if (!string.IsNullOrEmpty(objLicInsRequestParam.UploadedDoc))
                    {
                        DocLoclist = objLicInsRequestParam.UploadedDoc.Split(';').ToList();
                    }

                    foreach (var item in DocLoclist)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            string[] str = item.Split(char.Parse(Shared.Hash));
                            var DocLocation = str[0];
                            int docSequence = Convert.ToInt32(str[1]);
                            DP_AdditionalReqUploadDocs objDP_AdditionalReqUploadDocs = new DP_AdditionalReqUploadDocs()
                            {
                                AddReqMapId = objLicInsRequestParam.LicInsMapId,
                                DocLoc = DocLocation,
                                DocSeqNo = docSequence,
                                UserId = objLicInsRequestParam.UserId,
                                CompanyId = objLicInsRequestParam.CompanyId,
                                AddReqId = addReqId
                            };
                            objDecisionPointEntities.DP_AdditionalReqUploadDocs.Add(objDP_AdditionalReqUploadDocs);
                        }

                    }
                    #endregion

                }
                //Sent Professional License to Recipients
                SentAdditionalReqTorecipients(addReqId, objLicInsRequestParam, Convert.ToString(icTypeId, CultureInfo.InvariantCulture));


            }

            catch
            {
                throw;
            }
            return addReqId;
        }
        /// <summary>
        /// Used for Sent the created Reqiure documents with title to staff and IC as per services 
        /// </summary>
        /// <param name="profLicId"></param>
        /// <param name="objLicenseInsuranceRequestParam"></param>
        /// <createdby>bobi</createdby>
        /// <createddate>3 june 2014</createddate>
        private void SentAdditionalReqTorecipients(int addReqId, LicenseInsuranceRequestParam objLicenseInsuranceRequestParam, string icTypeId)
        {

            try
            {
                if (objLicenseInsuranceRequestParam.OperationType.Equals(1))
                {
                    SaveAdditionalReqToMappingFromJCR(addReqId, objLicenseInsuranceRequestParam, icTypeId);
                }
                else if (objLicenseInsuranceRequestParam.OperationType.Equals(2))
                {
                    SaveAdditionalReqToMappingFromProfile(addReqId, objLicenseInsuranceRequestParam, icTypeId);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for Save Additional Credentials details in mapping from JCR
        /// </summary>
        /// <param name="addReqId"></param>
        /// <param name="objLicenseInsuranceRequestParam"></param>
        /// <param name="icTypeId"></param>
        /// <createdby>bobi</createdby>
        /// <createddate>3 june 2014</createddate>
        private void SaveAdditionalReqToMappingFromJCR(int addReqId, LicenseInsuranceRequestParam objLicenseInsuranceRequestParam, string icTypeId)
        {
            FilterRequestParam objFilterRequestParam = null;
            try
            {
                objFilterRequestParam = new FilterRequestParam();
                objFilterRequestParam.CompanyId = objLicenseInsuranceRequestParam.CompanyId;
                objFilterRequestParam.vendortypefilter = icTypeId;

                //get IC list of company
                objFilterRequestParam.type = 1;
                IEnumerable<ICResponseParam> IClist = GetICdetailforLicIns(objFilterRequestParam);
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    foreach (var IC in IClist)
                    {
                        var addReqMappingDetails = (from docrec in objDecisionPointEntities.DP_AdditionalReqMapping
                                                    where docrec.AddReqId == addReqId && docrec.UserId == IC.Id && docrec.CompanyId == IC.CompanyId
                                                    select docrec).FirstOrDefault();
                        if (addReqMappingDetails == null)
                        {
                            DP_AdditionalReqMapping objDPAdditionalReqMapping = new DP_AdditionalReqMapping()
                            {
                                AddReqId = addReqId,
                                CompanyId = IC.CompanyId,
                                UserId = IC.Id,
                                Status = Shared.Pending
                            };
                            objDecisionPointEntities.DP_AdditionalReqMapping.Add(objDPAdditionalReqMapping);
                        }
                        else
                        {
                            addReqMappingDetails.AddReqId = addReqId;
                            addReqMappingDetails.CompanyId = IC.CompanyId;
                            addReqMappingDetails.UserId = IC.Id;
                        }
                    }
                    objDecisionPointEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for Save Additional Credentials details in mapping from profile page
        /// </summary>
        /// <param name="addReqId"></param>
        /// <param name="objLicenseInsuranceRequestParam"></param>
        /// <param name="icTypeId"></param>
        /// <createdby>bobi</createdby>
        /// <createddate>3 june 2014</createddate>
        private void SaveAdditionalReqToMappingFromProfile(int addReqId, LicenseInsuranceRequestParam objLicenseInsuranceRequestParam, string icTypeId)
        {
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    var addReqMappingDetails = (from docrec in objDecisionPointEntities.DP_AdditionalReqMapping
                                                where docrec.AddReqId == addReqId && docrec.UserId == objLicenseInsuranceRequestParam.UserId && docrec.CompanyId == objLicenseInsuranceRequestParam.CompanyId
                                                select docrec).FirstOrDefault();
                    if (addReqMappingDetails == null)
                    {
                        DP_AdditionalReqMapping objDPAdditionalReqMapping = new DP_AdditionalReqMapping()
                        {
                            AddReqId = addReqId,
                            CompanyId = objLicenseInsuranceRequestParam.CompanyId,
                            UserId = objLicenseInsuranceRequestParam.UserId,
                            Status = Shared.InProgress

                        };
                        if (object.Equals(objLicenseInsuranceRequestParam.CompletedDate, null))
                        {
                            objDPAdditionalReqMapping.CompletionDate = currentdate;
                        }
                        objDecisionPointEntities.DP_AdditionalReqMapping.Add(objDPAdditionalReqMapping);
                    }
                    else
                    {
                        addReqMappingDetails.AddReqId = addReqId;
                        addReqMappingDetails.CompanyId = objLicenseInsuranceRequestParam.CompanyId;
                        addReqMappingDetails.UserId = objLicenseInsuranceRequestParam.UserId;
                        addReqMappingDetails.Status = Shared.InProgress;
                        if (object.Equals(objLicenseInsuranceRequestParam.CompletedDate, null))
                        {
                            addReqMappingDetails.CompletionDate = currentdate;
                        }
                    }

                    objDecisionPointEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get IC detail  for license insurance
        /// </summary>
        /// <param name="filterRequestParam">FilterRequestParam</param>
        /// <returns>ICResponseParam</returns>         
        /// <createdBy>Sumit Saurav</createdBy>
        /// <createdDate>Aug 26 2013</createdDate>
        public IEnumerable<ICResponseParam> GetICdetailforLicIns(FilterRequestParam filterRequestParam)
        {
            IEnumerable<ICResponseParam> finallist = null;
            try
            {
                int vendorTypeId = Convert.ToInt32(filterRequestParam.vendortypefilter);
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    // entity query for fetch individual reqiured documents record  as per user Id
                    var query = (from staff in objDecisionPointEntities.DP_ComapnyVendorMapping
                                 join user in objDecisionPointEntities.DP_User
                                 on staff.VendorId equals user.Id
                                 join profile in objDecisionPointEntities.DP_Profile
                                 on user.Id equals profile.UserId
                                 join VTM in objDecisionPointEntities.DP_VendorTypeMapping
                                 on new { a = (int)(staff.VendorId == null ? 0 : staff.VendorId), b = staff.CompanyId } equals new { a = VTM.UserId, b = VTM.CreaterCompanyId }
                                 into VT
                                 from FVT in VT.DefaultIfEmpty()
                                 join VenMaster in objDecisionPointEntities.DP_VendorType
                                on FVT.VendorTypeId equals VenMaster.Id
                                into Vmaster
                                 from FVenmasterT in Vmaster.DefaultIfEmpty()
                                 // 
                                 where staff.CompanyId == filterRequestParam.CompanyId && user.IsActive == true && staff.IsActive == true && profile.UserType == Shared.IC
                                 && FVT.IsDefault == true
                                 //&& staff.Status == 1 && user.IsRegistered == true
                                 select new ICResponseParam
                                 {
                                     Fname = profile.Firstname,
                                     Lname = profile.LastName,
                                     EmailId = user.EmailId,
                                     Phone = profile.CellNumber == null ? string.Empty : profile.CellNumber,
                                     Id = user.Id,
                                     IsActive = (bool)(user.IsActive == null ? false : user.IsActive),
                                     BusinessName = profile.BusinessName == null ? string.Empty : profile.BusinessName,
                                     CompanyId = user.CompanyId,
                                     InvitationStatus = (bool)(staff.Invitationstatus == null ? false : staff.Invitationstatus),
                                     VTId = FVenmasterT.Id == null ? 0 : FVenmasterT.Id,
                                     VType = FVenmasterT.VendorType == null ? string.Empty : FVenmasterT.VendorType
                                 }).Distinct().ToList().OrderBy(x => x.Lname).Where(x => x.VTId == vendorTypeId);
                    finallist = query;

                }

                return finallist;
            }
            catch
            {
                throw;
            }
        }
    }
}
