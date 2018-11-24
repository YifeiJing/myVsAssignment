using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Example1
{
    class DataBaseControl
    {
        #region public members

        public Access access { get; set; }

        #endregion

        #region ctor

        public DataBaseControl()
        {
            access = new Access();
            
        }

        #endregion
    }

    class Access
    {
        #region public members

        public List<string> IDs = new List<string>();
        public List<string> Names = new List<string>();
        public List<int> Ages = new List<int>();
        public List<string> Genders = new List<string>();
        public List<int> Times = new List<int>();
        public List<string> Ranks = new List<string>();

        #endregion

        static string connectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\70400\\Documents\\Test.mdb";
        OleDbConnection oleDb = new OleDbConnection(connectionstring);

        /// <summary>
        /// Constructor
        /// </summary>
        public Access() 
        {
            oleDb.Open();
            Findrank();
        }

        //读取用户信息
        public void Findrow(string ID)
        {
            string sql = "select * from 表1 where ID='" + ID + "'";
            OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, oleDb); //创建适配对象
            DataTable dt = new DataTable(); //新建表对象
            dbDataAdapter.Fill(dt); //用适配对象填充表对象
            return;
        }
        //读取表1
        public void Find_sheet1()
        {
            string sql = "select * from 表1 ";
            OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, oleDb); //创建适配对象
            DataTable dt = new DataTable(); //新建表对象
            dbDataAdapter.Fill(dt); //用适配对象填充表对象
            string[] ID = new string[16];
            string[] Name = new string[16];
            int[] Age = new int[16];
            string[] Gender = new string[16];
            int[] Time = new int[16];
            string[] Rank = new string[16];
            for (int i = 0; i < 16; i++)
            {
                ID[i] = Convert.ToString(dt.Rows[i].ItemArray[0]);
                Name[i] = Convert.ToString(dt.Rows[i].ItemArray[1]);
                Age[i] = Convert.ToInt16(dt.Rows[i].ItemArray[2]);
                Gender[i] = Convert.ToString(dt.Rows[i].ItemArray[3]);
                Time[i] = Convert.ToInt16(dt.Rows[i].ItemArray[4]);
                Rank[i] = Convert.ToString(dt.Rows[i].ItemArray[5]);
            }
            IDs = ID.ToList<string>();
            Names = Name.ToList<string>();
            Ages = Age.ToList<int>();
            Genders = Gender.ToList<string>();
            Times = Time.ToList<int>();
            Ranks = Rank.ToList<string>();
        }
        //读取排行榜
        public void Findrank()
        {

            string sql = "select * from 排行榜 ORDER BY time";
            OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, oleDb); //创建适配对象
            DataTable dt = new DataTable(); //新建表对象
            dbDataAdapter.Fill(dt); //用适配对象填充表对象
            string[] Rank = new string[16];
            string[] ID = new string[16];
            string[] Name = new string[16];
            int[] Time = new int[16];
            for (int i = 0; i < 16; i++)
            {
                ID[i] = Convert.ToString(dt.Rows[i].ItemArray[1]);
                Name[i] = Convert.ToString(dt.Rows[i].ItemArray[2]);
                Time[i] = Convert.ToInt16(dt.Rows[i].ItemArray[3]);
                Rank[i] = Convert.ToString(dt.Rows[i].ItemArray[0]);
            }
            IDs = ID.ToList<string>();
            Names = Name.ToList<string>();
            Times = Time.ToList<int>();
            Ranks = Rank.ToList<string>();
        }
        //排行
        public void rank()
        {
            int[] ranking_time = new int[16];
            string[,] ranking_ID = new string[16, 2];
            string sql = "select ID,user_name,time from 表1 ORDER BY time ";
            OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, oleDb); //创建适配对象
            DataTable dt = new DataTable(); //新建表对象
            dbDataAdapter.Fill(dt); //用适配对象填充表对象
                                    //赋值给数组
            for (int i = 0; i < 16; i++)
            {
                ranking_ID[i, 0] = Convert.ToString(dt.Rows[i].ItemArray[0]);
                ranking_ID[i, 1] = Convert.ToString(dt.Rows[i].ItemArray[1]);
                ranking_time[i] = Convert.ToInt16(dt.Rows[i].ItemArray[2]);
            }
            //写入到排行榜中
            for (int i = 0; i < 16; i++)
            {
                Change_rank(ranking_time[i], ranking_ID[i, 0], ranking_ID[i, 1], Convert.ToString(i));
            }
            //刷新表1的rank
            for (int i = 0; i < 16; i++)
            {
                Change_sheet1_rank(ranking_ID[i, 0], Convert.ToString(i));
            }

        }
        //刷新排行榜
        public bool Change_rank(int rank_time, string rank_ID, string rank_name, string i)
        {
            string sql3 = "update 排行榜 set [time]='" + rank_time + "',user_name='" + rank_name + "',ID='" + rank_ID + "' WHERE rank='" + i + "'";
            OleDbCommand oleDbCommand = new OleDbCommand(sql3, oleDb);
            int j = oleDbCommand.ExecuteNonQuery();
            return j > 0;
        }
        //刷新表1时间
        public bool Change_sheet1_time(int rank_time, string ID)
        {
            string sql4 = "update 表1 set [time]='" + rank_time + "' where ID = '" + ID + "'";
            OleDbCommand oleDbCommand = new OleDbCommand(sql4, oleDb);
            int j = oleDbCommand.ExecuteNonQuery();
            return j > 0;
        }
        //刷新表1排名
        public bool Change_sheet1_rank(string ID, string rank)
        {
            string sql1 = "update 表1 set rank='" + rank + "' where ID = '" + ID + "'";
            OleDbCommand oleDbCommand = new OleDbCommand(sql1, oleDb);
            int j = oleDbCommand.ExecuteNonQuery();
            return j > 0;
        }
    }
}
