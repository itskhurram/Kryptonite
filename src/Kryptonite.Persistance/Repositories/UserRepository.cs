using Kryptonite.Common;
using Kryptonite.Domain.Entities;
using Kryptonite.Domain.Interfaces;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System.Data;
using System.Data.Common;

namespace Kryptonite.Persistance.Repositories {
    public class UserRepository : IUserRepository {
        private readonly SqlDatabase _sqlDatabase;
        public UserRepository(SqlDatabase sqlDatabase) {
            _sqlDatabase = sqlDatabase;
        }

        #region SQL Procedures
        protected const string PROC_USER_LOGIN = "[dbo].[Proc_User_Login]";
        #endregion SQL Procedures

        #region Parameters
        protected const string USERID = "user_id";
        protected const string LOGINNAME = "login_name";
        protected const string FIRSTNAME = "first_name";
        protected const string LASTNAME = "last_name";
        protected const string LOGINPASSWORD = "login_password";
        protected const string ISACTIVE = "is_active";
        protected const string CREATEDBY = "created_by";
        protected const string CREATEDDATE = "created_date";
        protected const string UPDATEDBY = "updated_by";
        protected const string UPDATEDDATE = "updated_date";
        #endregion Parameters

        #region Functions
        private static User Mapper(IDataReader reader) {
            var userAccount = new User();
            if (reader[USERID] != null && reader[USERID] != DBNull.Value) {
                userAccount.UserId = Conversion.ToInt(reader[USERID]);
            }
            if (reader[LOGINNAME] != null && reader[LOGINNAME] != DBNull.Value) {
                userAccount.LoginName = Conversion.ToString(reader[LOGINNAME]);
            }
            return userAccount;
        }
        public Task<User> Login(string loginName, string loginPassword) {
            try {
                User userAccount = new();
                using (DbCommand dbCommand = _sqlDatabase.GetStoredProcCommand(PROC_USER_LOGIN)) {
                    _sqlDatabase.AddInParameter(dbCommand, LOGINNAME, DbType.String, loginName);
                    _sqlDatabase.AddInParameter(dbCommand, LOGINPASSWORD, DbType.String, loginPassword);
                    using var reader = _sqlDatabase.ExecuteReader(dbCommand);
                    if (reader.Read())
                        userAccount = Mapper(reader);
                }
                return Task.FromResult(userAccount);
            }
            finally { }
        }
        #endregion Functions
    }
}
