using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TShockAPI;
using TShockAPI.DB;

namespace TerraSpleef
{
    public class SQLDialogs
    {
        //Areas table
        int ID { get; set; }
        string name { get; set; }
        float stX { get; set; }
        float stY { get; set; }
        float spX { get; set; }
        float spY { get; set; }
        //Chest Table
        int CID { get; set; }
        float CX { get; set; }
        float CY { get; set; }

        //Functions

    }

    public class spleef
    {
        //Area info
        int ID { get; set; }
        string name { get; set; }
        int aPlayer { get; set; }
        List <string> lPlayers { get; set; }
        //Area cords
        float stX { get; set; }
        float stY { get; set; }
        float spX { get; set; }
        float spY { get; set; }

        //Functions

    }
}
