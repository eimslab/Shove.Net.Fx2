using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

using Shove.Database;

namespace DAL
{
    /*
    Program Name: Shove.DAL.30 for MySQL
    Program Version: 3.0
    Writer By: 3km.shovesoft.shove (zhou changjun)
    Release Time: 2009.7.16

    System Request: Shove.dll, MySql.Data.dll, MySql.Data.Entity.dll, MySql.Web.dll
    All Rights saved.
    */


    // Please Add a Key in Web.config File's appSetting section, Exemple:
    // <add key="ConnectionString" value="server=localhost;user id=root;password=;database=test;prot=3306;" />


    public class Tables
    {
        public class api : MySQL.TableBase
        {
            public MySQL.Field Id;
            public MySQL.Field FromUserName;
            public MySQL.Field ToUserName;
            public MySQL.Field Conten;
            public MySQL.Field DateTime;
            public MySQL.Field MsgType;

            public api()
            {
                TableName = "api";

                Id = new MySQL.Field(this, "Id", "Id", MySqlDbType.Int32, true);
                FromUserName = new MySQL.Field(this, "FromUserName", "FromUserName", MySqlDbType.VarChar, false);
                ToUserName = new MySQL.Field(this, "ToUserName", "ToUserName", MySqlDbType.VarChar, false);
                Conten = new MySQL.Field(this, "Conten", "Conten", MySqlDbType.VarChar, false);
                DateTime = new MySQL.Field(this, "DateTime", "DateTime", MySqlDbType.DateTime, false);
                MsgType = new MySQL.Field(this, "MsgType", "MsgType", MySqlDbType.VarChar, false);
            }
        }

        public class a_employee : MySQL.TableBase
        {
            public MySQL.Field id;
            public MySQL.Field E_Name;
            public MySQL.Field sectionId;
            public MySQL.Field stationId;
            public MySQL.Field createTime;
            public MySQL.Field fingerprintNo;
            public MySQL.Field state;
            public MySQL.Field remark;
            public MySQL.Field TrminalId;

            public a_employee()
            {
                TableName = "a_employee";

                id = new MySQL.Field(this, "id", "id", MySqlDbType.Int32, true);
                E_Name = new MySQL.Field(this, "E_Name", "E_Name", MySqlDbType.VarChar, false);
                sectionId = new MySQL.Field(this, "sectionId", "sectionId", MySqlDbType.Int32, false);
                stationId = new MySQL.Field(this, "stationId", "stationId", MySqlDbType.Int32, false);
                createTime = new MySQL.Field(this, "createTime", "createTime", MySqlDbType.DateTime, false);
                fingerprintNo = new MySQL.Field(this, "fingerprintNo", "fingerprintNo", MySqlDbType.VarChar, false);
                state = new MySQL.Field(this, "state", "state", MySqlDbType.Int32, false);
                remark = new MySQL.Field(this, "remark", "remark", MySqlDbType.VarChar, false);
                TrminalId = new MySQL.Field(this, "TrminalId", "TrminalId", MySqlDbType.Int32, false);
            }
        }

        public class a_option : MySQL.TableBase
        {
            public MySQL.Field id;
            public MySQL.Field key;
            public MySQL.Field value;

            public a_option()
            {
                TableName = "a_option";

                id = new MySQL.Field(this, "id", "id", MySqlDbType.Int32, true);
                key = new MySQL.Field(this, "key", "key", MySqlDbType.VarChar, false);
                value = new MySQL.Field(this, "value", "value", MySqlDbType.VarChar, false);
            }
        }

        public class a_originaldata : MySQL.TableBase
        {
            public MySQL.Field O_id;
            public MySQL.Field O_terminalId;
            public MySQL.Field E_Id;
            public MySQL.Field DATATime;
            public MySQL.Field O_fingerprintNO;
            public MySQL.Field O_analyze;

            public a_originaldata()
            {
                TableName = "a_originaldata";

                O_id = new MySQL.Field(this, "O_id", "O_id", MySqlDbType.Int32, true);
                O_terminalId = new MySQL.Field(this, "O_terminalId", "O_terminalId", MySqlDbType.VarChar, false);
                E_Id = new MySQL.Field(this, "E_Id", "E_Id", MySqlDbType.Int32, false);
                DATATime = new MySQL.Field(this, "DATATime", "DATATime", MySqlDbType.DateTime, false);
                O_fingerprintNO = new MySQL.Field(this, "O_fingerprintNO", "O_fingerprintNO", MySqlDbType.VarChar, false);
                O_analyze = new MySQL.Field(this, "O_analyze", "O_analyze", MySqlDbType.VarChar, false);
            }
        }

        public class a_section : MySQL.TableBase
        {
            public MySQL.Field sectionId;
            public MySQL.Field Name;

            public a_section()
            {
                TableName = "a_section";

                sectionId = new MySQL.Field(this, "sectionId", "sectionId", MySqlDbType.Int32, true);
                Name = new MySQL.Field(this, "Name", "Name", MySqlDbType.VarChar, false);
            }
        }

        public class a_stationid : MySQL.TableBase
        {
            public MySQL.Field stationId;
            public MySQL.Field Name;

            public a_stationid()
            {
                TableName = "a_stationid";

                stationId = new MySQL.Field(this, "stationId", "stationId", MySqlDbType.Int32, true);
                Name = new MySQL.Field(this, "Name", "Name", MySqlDbType.VarChar, false);
            }
        }

        public class a_statistics : MySQL.TableBase
        {
            public MySQL.Field id;
            public MySQL.Field year;
            public MySQL.Field month;
            public MySQL.Field Remark;

            public a_statistics()
            {
                TableName = "a_statistics";

                id = new MySQL.Field(this, "id", "id", MySqlDbType.Int32, true);
                year = new MySQL.Field(this, "year", "year", MySqlDbType.VarChar, false);
                month = new MySQL.Field(this, "month", "month", MySqlDbType.VarChar, false);
                Remark = new MySQL.Field(this, "Remark", "Remark", MySqlDbType.VarChar, false);
            }
        }

        public class a_statistics_detail : MySQL.TableBase
        {
            public MySQL.Field ID;
            public MySQL.Field Statistics_id;
            public MySQL.Field E_ID;
            public MySQL.Field late1;
            public MySQL.Field Notcard;
            public MySQL.Field late2;
            public MySQL.Field late3;
            public MySQL.Field _24mail;
            public MySQL.Field Not24mail;
            public MySQL.Field thing;
            public MySQL.Field leave;
            public MySQL.Field DataTime;
            public MySQL.Field remark;
            public MySQL.Field stateId;

            public a_statistics_detail()
            {
                TableName = "a_statistics_detail";

                ID = new MySQL.Field(this, "ID", "ID", MySqlDbType.Int32, true);
                Statistics_id = new MySQL.Field(this, "Statistics_id", "Statistics_id", MySqlDbType.Int32, false);
                E_ID = new MySQL.Field(this, "E_ID", "E_ID", MySqlDbType.Int32, false);
                late1 = new MySQL.Field(this, "late1", "late1", MySqlDbType.Int32, false);
                Notcard = new MySQL.Field(this, "Notcard", "Notcard", MySqlDbType.Int32, false);
                late2 = new MySQL.Field(this, "late2", "late2", MySqlDbType.Int32, false);
                late3 = new MySQL.Field(this, "late3", "late3", MySqlDbType.Int32, false);
                _24mail = new MySQL.Field(this, "24mail", "_24mail", MySqlDbType.Int32, false);
                Not24mail = new MySQL.Field(this, "Not24mail", "Not24mail", MySqlDbType.Int32, false);
                thing = new MySQL.Field(this, "thing", "thing", MySqlDbType.Int32, false);
                leave = new MySQL.Field(this, "leave", "leave", MySqlDbType.Int32, false);
                DataTime = new MySQL.Field(this, "DataTime", "DataTime", MySqlDbType.DateTime, false);
                remark = new MySQL.Field(this, "remark", "remark", MySqlDbType.VarChar, false);
                stateId = new MySQL.Field(this, "stateId", "stateId", MySqlDbType.Int32, false);
            }
        }

        public class a_user : MySQL.TableBase
        {
            public MySQL.Field ID;
            public MySQL.Field UserName;
            public MySQL.Field UserPwd;

            public a_user()
            {
                TableName = "a_user";

                ID = new MySQL.Field(this, "ID", "ID", MySqlDbType.Int32, true);
                UserName = new MySQL.Field(this, "UserName", "UserName", MySqlDbType.VarChar, false);
                UserPwd = new MySQL.Field(this, "UserPwd", "UserPwd", MySqlDbType.VarChar, false);
            }
        }

        public class e_details : MySQL.TableBase
        {
            public MySQL.Field id;
            public MySQL.Field FingerprintNO;
            public MySQL.Field E_Id;
            public MySQL.Field termian;

            public e_details()
            {
                TableName = "e_details";

                id = new MySQL.Field(this, "id", "id", MySqlDbType.Int32, true);
                FingerprintNO = new MySQL.Field(this, "FingerprintNO", "FingerprintNO", MySqlDbType.VarChar, false);
                E_Id = new MySQL.Field(this, "E_Id", "E_Id", MySqlDbType.Int32, false);
                termian = new MySQL.Field(this, "termian", "termian", MySqlDbType.Int32, false);
            }
        }

        public class terminal : MySQL.TableBase
        {
            public MySQL.Field Id;
            public MySQL.Field Name;

            public terminal()
            {
                TableName = "terminal";

                Id = new MySQL.Field(this, "Id", "Id", MySqlDbType.Int32, true);
                Name = new MySQL.Field(this, "Name", "Name", MySqlDbType.VarChar, false);
            }
        }
    }

    public class Views
    {
    }

    public class Functions
    {
    }

    public class Procedures
    {
    }
}
