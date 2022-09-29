using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCVacationManagement.Models.Entity;
using System.Web.Security;

namespace MVCVacationManagement.Security
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            DbMvcVacationManagementEntities3 db = new DbMvcVacationManagementEntities3();
            var adminInfo = db.TBLADMIN.FirstOrDefault(x => x.kullanici_adi == username);
            var musteriInfo = db.TBLMUSTERI.FirstOrDefault(x => x.kullanici_adi == username);
            var hotelYetkilisiInfo = db.TBLHOTELYETKILISI.FirstOrDefault(x => x.kullanici_adi == username);
            var turYetkilisiInfo = db.TBLTURYETKILISI.FirstOrDefault(x => x.kullanici_adi == username );

            if (adminInfo != null)
            {
                return new string[] { adminInfo.rol };
            }
            if (musteriInfo != null)
            {
                return new string[] { musteriInfo.rol };
            }
            if (hotelYetkilisiInfo != null)
            {
                return new string[] { hotelYetkilisiInfo.rol };
            }
            if (turYetkilisiInfo != null)
            {
                return new string[] { turYetkilisiInfo.rol };
            }
            else
            {
                return new string[] { };
            }

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}