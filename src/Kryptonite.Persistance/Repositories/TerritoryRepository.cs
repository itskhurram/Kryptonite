using Kryptonite.Common;
using Kryptonite.Domain.Entities;
using Kryptonite.Domain.Interfaces;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System.Data;
using System.Data.Common;

namespace Kryptonite.Persistance.Repositories {
    public class TerritoryRepository : ITerritoryRepository {
        private readonly SqlDatabase _sqlDatabase;
        public TerritoryRepository(SqlDatabase sqlDatabase) {
            _sqlDatabase = sqlDatabase;
        }

        #region SQL Procedures
        protected const string PROC_DIVISION_GETALL = "[dbo].[Proc_Division_GetAll]";
        #endregion SQL Procedures

        #region Parameters
        protected const string DIVISIONID = "division_id";
        protected const string PROVINCEID = "province_id";
        protected const string DIVISIONNAME = "division_name";
        protected const string ISACTIVE = "is_active";
        protected const string CREATEDBY = "created_by";
        protected const string CREATEDDATE = "created_date";
        protected const string UPDATEDBY = "updated_by";
        protected const string UPDATEDDATE = "updated_date";
        #endregion Parameters

        #region Functions
        private static Division Mapper(IDataReader reader) {
            var division = new Division();
            if (reader[DIVISIONID] != null && reader[DIVISIONID] != DBNull.Value) {
                division.DivisionId = Conversion.ToByte(reader[DIVISIONID]);
            }
            if (reader[PROVINCEID] != null && reader[PROVINCEID] != DBNull.Value) {
                division.ProvinceId = Conversion.ToByte(reader[PROVINCEID]);
            }
            if (reader[DIVISIONNAME] != null && reader[DIVISIONNAME] != DBNull.Value) {
                division.DivisionName = Conversion.ToString(reader[DIVISIONNAME]);
            }
            if (reader[ISACTIVE] != null && reader[ISACTIVE] != DBNull.Value) {
                division.IsActive = Conversion.ToBool(reader[ISACTIVE]);
            }
            if (reader[CREATEDBY] != null && reader[CREATEDBY] != DBNull.Value) {
                division.CreatedBy = Conversion.ToInt(reader[CREATEDBY]);
            }
            if (reader[CREATEDDATE] != null && reader[CREATEDDATE] != DBNull.Value) {
                division.CreatedDate = Conversion.ToDateTime(reader[CREATEDDATE]);
            }
            if (reader[UPDATEDBY] != null && reader[UPDATEDBY] != DBNull.Value) {
                division.UpdateBy = Conversion.ToInt(reader[UPDATEDBY]);
            }
            if (reader[UPDATEDDATE] != null && reader[UPDATEDDATE] != DBNull.Value) {
                division.UpdateDate = Conversion.ToDateTime(reader[UPDATEDDATE]);
            }
            return division;
        }
        public Task<IList<Division>> GetDivisions(bool? isActive = null) {
            IList<Division> divisionList = new List<Division>();
            using (DbCommand dbCommand = _sqlDatabase.GetStoredProcCommand(PROC_DIVISION_GETALL)) {
                _sqlDatabase.AddInParameter(dbCommand, ISACTIVE, DbType.Boolean, isActive== null ? DBNull.Value : isActive);
                using var reader = _sqlDatabase.ExecuteReader(dbCommand);
                while (reader.Read())
                    divisionList.Add(Mapper(reader));
            }
            return Task.FromResult(divisionList);
        }
        #endregion Functions
    }
}
