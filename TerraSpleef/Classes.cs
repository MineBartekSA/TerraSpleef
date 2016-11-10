using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TShockAPI;
using TShockAPI.DB;
using MySql.Data.MySqlClient;

namespace TerraSpleef
{
    public class SQLDialogs
    {
        //Areas table
        public int ID { get; set; }
        public string name { get; set; }
        public float stX { get; set; }
        public float stY { get; set; }
        public float spX { get; set; }
        public float spY { get; set; }
        //Chest Table
        public int CID { get; set; }
        public float CX { get; set; }
        public float CY { get; set; }

        //Functions
        public void Tables()
        {
            //Areas table
            var areasTable = new SqlTable("TerraSpleef-Areas",
                new SqlColumn("ID", MySqlDbType.Int32) { Primary = true, AutoIncrement = true, NotNull = true, Unique = true },
                new SqlColumn("name", MySqlDbType.Text) { Unique = true },
                new SqlColumn("stX", MySqlDbType.Int32),
                new SqlColumn("stY", MySqlDbType.Int32),
                new SqlColumn("spX", MySqlDbType.Int32),
                new SqlColumn("spY", MySqlDbType.Int32)
                );
            //Chest table
            var chestTable = new SqlTable("TerraSpleef-Chest",
                new SqlColumn("ID", MySqlDbType.Int32) { Unique = true, Primary = true, AutoIncrement = true, NotNull = true },
                new SqlColumn("areaID", MySqlDbType.Int32),
                new SqlColumn("X", MySqlDbType.Int32),
                new SqlColumn("Y", MySqlDbType.Int32)
                );

            var SQLWriter = new SqlTableCreator(TShock.DB, TShock.DB.GetSqlType() == SqlType.Sqlite ? (IQueryBuilder)new SqliteQueryCreator() : new MysqlQueryCreator());
            SQLWriter.EnsureTableStructure(areasTable);
            SQLWriter.EnsureTableStructure(chestTable);
        }

        public List<string> getNames()
        {
            List<string> list = new List<string>();
            var reader = TShock.DB.QueryReader("SELECT name FROM TerraSpleef-Areas");

            while (reader.Read())
            {
                list.Add(reader.Get<string>("name"));
            }

            return list;
        }

        public static SQLDialogs readArea(string name)
        {
            var reader = TShock.DB.QueryReader("SELECT * FROM TerraSpleef-Areas WHERE name='" + name + "'");
            var area = new SQLDialogs();

            while (reader.Read())
            {
                area = new SQLDialogs
                {
                    ID = reader.Get<int>("ID"),
                    name = name,
                    stX = reader.Get<int>("stX"),
                    stY = reader.Get<int>("stY"),
                    spX = reader.Get<int>("spX"),
                    spY = reader.Get<int>("spY")
                };
            }
            return area;
        }

        public void saveArea()
        {

        }
    }

    public class spleef
    {
        //Area info
        public int ID { get; set; }
        public string name { get; set; }
        public int aPlayer { get; set; }
        public List <TSPlayer> lPlayers { get; set; }
        public List <gHand> gameHandler { get; set; }
        //Area cords
        public float stX { get; set; }
        public float stY { get; set; }
        public float spX { get; set; }
        public float spY { get; set; }

        //Functions

    }
    public class gHand
    {
        public int place { get; set; }
        public TSPlayer player { get; set; }
        public bool isSpec { get; set; }
    }
    public class gAutoStart
    {
        public spleef game { get; set; }

        public void gameAutoStart()
        {
            int numerOfPlayers = 0;

            while (numerOfPlayers != game.aPlayer)
            {
                if(numerOfPlayers != game.lPlayers.Count)
                {
                    numerOfPlayers = game.lPlayers.Count;
                    for(int i = 0; i <= game.lPlayers.Count; i++)
                    {
                        game.lPlayers[i].SendInfoMessage("There is " + numerOfPlayers + " of " + game.aPlayer + " players in this spleef");
                    }
                }
            }

            

        }
    }
}
