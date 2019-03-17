using System.Data;
using System.Data.SqlClient;

namespace Olive.CodeBuilder.Core
{
    public class SqlDbObject
    {
        private string _dbconnectStr;
        private SqlConnection connect;



        public SqlDbObject(string DbConnectStr)
        {
            this.connect = new SqlConnection(DbConnectStr);
            this._dbconnectStr = DbConnectStr;
            this.connect.ConnectionString = DbConnectStr;
        }

        public DataTable GetTables(string DbName)
        {

            string sql = @"SELECT
                                    obj.name name,obj.type,
                                    schem.name schemname,
                                    idx.rows,
                                    CAST
                                    (
                                        CASE 
                                            WHEN (SELECT COUNT(1) FROM sys.indexes WHERE object_id= obj.OBJECT_ID AND is_primary_key=1) >=1 THEN 1
                                            ELSE 0
                                        END 
                                    AS BIT) HasPrimaryKey                                         
                                    from sys.objects obj 
                                    INNER JOIN dbo.sysindexes idx on obj.object_id=idx.id and idx.indid<=1
                                    INNER JOIN sys.schemas schem ON obj.schema_id=schem.schema_id
                                    where( obj.type='U' or obj.type='V') order by obj.name";
            SqlCommand command = connect.CreateCommand();
            command.CommandText = sql;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public DataTable GetTablesInfo(string DbName)
        {
            return GetTables(DbName);
        }


        public DataTable GetColumnInfoList(string DbName, string TableName)
        {
            string sql = string.Format(@" WITH indexCTE AS
                                    (
                                        SELECT 
                                        ic.column_id,
                                        ic.index_column_id,
                                        ic.object_id    
                                        FROM sys.indexes idx
                                        INNER JOIN sys.index_columns ic ON idx.index_id = ic.index_id AND idx.object_id = ic.object_id
                                        WHERE  idx.object_id =OBJECT_ID('{0}') AND idx.is_primary_key=1
                                    )
                                    select
                                    colm.column_id ColumnID,
                                    CAST(CASE WHEN indexCTE.column_id IS NULL THEN '' ELSE '√' END AS varchar) isPK,
                                    colm.name ColumnName,
                                    systype.name TypeName,
                                    CASE WHEN colm.is_identity =0 then '' else '√' END  IsIdentity,
                                    CASE WHEN colm.is_nullable =0 then '' else '√' END  cisNull,
                                    cast(colm.max_length as int) ByteLength,
                                    (
                                        case 
                                            when systype.name='nvarchar' and colm.max_length>0 then colm.max_length/2 
                                            when systype.name='nchar' and colm.max_length>0 then colm.max_length/2
                                            when systype.name='ntext' and colm.max_length>0 then colm.max_length/2 
                                            else colm.max_length
                                        end
                                    ) CharLength,
                                    cast(colm.precision as int) Precision,
                                    cast(colm.scale as int) Scale,
                                    prop.value DeText
                                    from sys.columns colm
                                    inner join sys.types systype on colm.system_type_id=systype.system_type_id and colm.user_type_id=systype.user_type_id
                                    left join sys.extended_properties prop on colm.object_id=prop.major_id and colm.column_id=prop.minor_id
                                    LEFT JOIN indexCTE ON colm.column_id=indexCTE.column_id AND colm.object_id=indexCTE.object_id                                        
                                    where colm.object_id=OBJECT_ID('{0}')
                                    order by colm.column_id", TableName);
            SqlCommand command = connect.CreateCommand();
            command.CommandText = sql;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                row["TypeName"] = DbType2CType(row["TypeName"].ToString());
            }
            return dt;
        }

        public string DbType2CType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;
            dbtype = dbtype.ToLower();
            string csharpType = "object";
            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "numeric": csharpType = "decimal"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallint": csharpType = "short"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
        }
    }

}
