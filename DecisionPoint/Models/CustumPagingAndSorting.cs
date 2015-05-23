using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DecisionPoint.Models
{
    public static class CustumPagingAndSorting
    {
        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey>
            (this IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector,
             bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByWithDirection<TSource, TKey>
            (this IQueryable<TSource> source,
             Expression<Func<TSource, TKey>> keySelector,
             bool descending)
        {
            return descending ? source.OrderByDescending(keySelector)
                              : source.OrderBy(keySelector);
        }
        /// <summary>
        /// Used for custome paging and sorting
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="Dir"></param>
        /// <returns></returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>8 july 2014</createddate>
        public static IList<Companies> GetCompantiesPage(int pageNumber, int pageSize, string sort, bool Dir, IList<Companies> companies)
        {
            IList<Companies> finallist = null;
            try
            {
               
                switch (sort)
                {
                    case "CompanyID":
                        finallist = companies.OrderByWithDirection(x=>x.CompanyID,Dir).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    case "BusinessName":
                        finallist = companies.OrderByWithDirection(x => x.BusinessName, Dir).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    case "ContactName":
                        finallist = companies.OrderByWithDirection(x => x.ContactName, Dir).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    case "UserType":
                        finallist = companies.OrderByWithDirection(x => x.UserType, Dir).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    case "Phone":
                        finallist = companies.OrderByWithDirection(x => x.Phone, Dir).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        break;

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