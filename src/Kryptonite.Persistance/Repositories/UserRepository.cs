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
            User userAccount = new() {
                UserId = (reader[USERID] != DBNull.Value) ? Conversion.ToInt(reader[USERID]) : 0,
                LoginName = (reader[LOGINNAME] != DBNull.Value) ? Conversion.ToString(reader[LOGINNAME]) : string.Empty,
                FirstName = (reader[FIRSTNAME] != DBNull.Value) ? Conversion.ToString(reader[FIRSTNAME]) : string.Empty,
                LastName = (reader[LASTNAME] != DBNull.Value) ? Conversion.ToString(reader[LASTNAME]) : string.Empty,
                IsActive = (reader[ISACTIVE] != DBNull.Value) ? Conversion.ToBool(reader[ISACTIVE]) : false,
                CreatedBy = (reader[CREATEDBY] != DBNull.Value) ? Conversion.ToInt(reader[CREATEDBY]) : 0,
                CreatedDate = (reader[CREATEDDATE] != DBNull.Value) ? Conversion.ToDateTime(reader[CREATEDDATE]) : DateTime.MinValue,
                UpdateBy = (reader[UPDATEDBY] != DBNull.Value) ? Conversion.ToInt(reader[UPDATEDBY]) : 0,
                UpdateDate = (reader[UPDATEDDATE] != DBNull.Value) ? Conversion.ToDateTime(reader[UPDATEDDATE]) : DateTime.MinValue
            };
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
