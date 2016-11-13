using System;
using System.Collections.Generic;
using System.Threading;
using TShockAPI;
using TShockAPI.DB;
using TerrariaApi.Server;
using Terraria;
using System.IO;
using System.Linq;
using TerraSpleef.Extensions;

namespace TerraSpleef
{
    public class TerraSpleef : TerrariaPlugin
    {
        //Vars
        public SQLDialogs sqld = new SQLDialogs();
        public List<spleef> activeSpleefs = new List<spleef>();
        //Create vars
        public bool isCreating;
        public spleef createArea = new spleef();

        //Load stage
        public override string Name
        {
            get { return "TerraSpleef"; }
        }
        public override Version Version
        {
            get { return new Version(1, 0, 0); }
        }
        public override string Author
        {
            get { return "MineBartekSA"; }
        }
        public override string Description
        {
            get { return "Spleef minigame plugin!"; }
        }
        public TerraSpleef(Main game) : base(game)
        {
            //Nothing!
        }
        public override void Initialize()
        {
            sqld.Tables();
            //Hooks
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.NetGetData.Register(this, OnGetData);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //UnHooks
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.NetGetData.Deregister(this, OnGetData);
            }
            base.Dispose(disposing);
        }
        //End of loading stage



        //Voids



        //Commands
        void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command(spleefCMD, "terraspleef", "tspleef", "ts")
            {
                HelpText = "A TerraSpleef base command"
            });

        }
        //End of Commands



        //Commands exe voids
        void spleefCMD(CommandArgs args)
        {
            string basePar = args.Parameters[0];
            TSPlayer play = args.Player;
            spleef area = new spleef();
            SQLDialogs sqd = new SQLDialogs();
            TerraSpleef TS = new TerraSpleef(Game);

            switch (basePar)
            {
                case "info":
                    {

                        break;
                    }

                case "start":
                    {
                        string aName = args.Parameters[1];

                        if (aName == "")
                        {
                            play.SendErrorMessage("Wrong command usage!");
                            play.SendErrorMessage("/tspleef start <arena name> <number of players>");
                            break;
                        }

                        List<string> lNamesArea = sqd.getNames();

                        foreach (string a in lNamesArea)
                        {
                            if (a == aName)
                            {
                                area.name = aName;
                            }
                        }

                        if (area.name == null)
                        {
                            play.SendErrorMessage("Wrong area name!");
                            play.SendErrorMessage("/tspleef start <arena name> <number of players>");
                            break;
                        }

                        if (args.Parameters[2] != null)
                        {
                            int aPlay;
                            try
                            {
                                aPlay = Int32.Parse(args.Parameters[2]);
                            }
                            catch (FormatException exe)
                            {
                                play.SendErrorMessage("Wrong number!");
                                play.SendErrorMessage("/tspleef start <arena name> <number of players>");
                                TShock.Log.Error("There is an exception :");
                                TShock.Log.Error(exe.ToString());
                                break;
                            }
                            if (aPlay > 5)
                            {
                                play.SendErrorMessage("Max number of player is 5");
                                break;
                            }
                            area.aPlayer = aPlay;
                        }
                        else if (args.Parameters[2] == null)
                        {
                            area.aPlayer = 5;
                            play.SendInfoMessage("Setting 5 players in area");
                        }

                        SQLDialogs readed = SQLDialogs.readArea(area.name);
                        area.ID = readed.ID;
                        area.stX = readed.stX;
                        area.stY = readed.stY;
                        area.spX = readed.spX;
                        area.spY = readed.spY;

                        area.lPlayers.Add(play);

                        play.Teleport(area.stX, area.stY);

                        activeSpleefs.Add(area);

                        TSPlayer.All.SendInfoMessage("There a new spleef starts!");
                        TSPlayer.All.SendInfoMessage("To join typer /tspleef join " + area.name);

                        gAutoStart auto = new gAutoStart()
                        {
                            game = area
                        };

                        new Thread(new ThreadStart(auto.gameAutoStart)).Start();

                        break;
                    }

                case "join":
                    {
                        string name = args.Parameters[1];

                        foreach (var aArea in activeSpleefs)
                        {
                            if (aArea.name == name)
                            {
                                play.Teleport(aArea.stX, aArea.stY);
                            }
                        }

                        break;
                    }

                case "create": // /tspleef create name ... /tspleef create point1 ... /tspleef create point2 ... /tspleef create spawnpos  ... /tspleef create specpos ... /tspleef create save
                    {
                        string para = args.Parameters[1];
                        PlayerInfo info = args.Player.GetPlayerInfo();

                        if (para == "point1" && isCreating)
                        {
                            info.Point = 1;
                            args.Player.SendInfoMessage("Modify a block to set point 1.");
                            break;
                        }
                        else if (para == "point2" && isCreating)
                        {
                            info.Point = 2;
                            args.Player.SendInfoMessage("Modify a block to set point 2.");
                            break;
                        }
                        else if (para == "spawnpos" && isCreating)
                        {
                            float playerX = args.Player.X;
                            float playerY = args.Player.Y;

                            createArea.stX = playerX;
                            createArea.stY = playerY;

                            args.Player.SendInfoMessage("You set a spawn point for new spleef area");
                            break;
                        }
                        else if (para == "specpos" && isCreating)
                        {
                            float playerX = args.Player.X;
                            float playerY = args.Player.Y;

                            createArea.spX = playerX;
                            createArea.spY = playerY;

                            args.Player.SendInfoMessage("You set a spactate point for new spleef area");
                            break;
                        }
                        else if (para == "save" && isCreating)
                        {
                            args.Player.SendInfoMessage("Saving area " + createArea.name + " in progress ...");

                        }
                        else if (!isCreating)
                        {
                            string name = args.Parameters[2];
                            createArea.name = name;
                            args.Player.SendInfoMessage("Name of this are will be: " + createArea.name);
                            break;
                        }
                        else
                        {
                            if(isCreating)
                            {
                                args.Player.SendErrorMessage("Someone is alredy creating a spleef are please wait");
                                args.Player.SendErrorMessage("Or you type a wrong command");
                                break;
                            }
                            args.Player.SendErrorMessage("You type a wrong command or you don't start creating area!");
                            args.Player.SendErrorMessage("Please use a valid command or /tspleef create <name of area>");
                            break;
                        }

                        break;
                    }

                case "commands":
                    {
                        play.SendInfoMessage("");
                        break;
                    }

                default:
                    {
                        play.SendInfoMessage("TerraSpleef " + TS.Version);
                        play.SendInfoMessage("Use /tspleef commands to see all the avaible commands");
                        break;
                    }
                    //Switch end
            }
        }
        //End of Commands exe voids



        //Other voids
        void OnGetData(GetDataEventArgs args)
        {
            switch (args.MsgID)
            {
                #region Packet 17 - Tile

                case PacketTypes.Tile:
                    PlayerInfo info = TShock.Players[args.Msg.whoAmI].GetPlayerInfo();
                    if (info.Point != 0)
                    {
                        using (var reader = new BinaryReader(new MemoryStream(args.Msg.readBuffer, args.Index, args.Length)))
                        {
                            reader.ReadByte();
                            int x = reader.ReadInt16();
                            int y = reader.ReadInt16();
                            if (x >= 0 && y >= 0 && x < Main.maxTilesX && y < Main.maxTilesY)
                            {
                                if (info.Point == 1)
                                {
                                    info.X = x;
                                    info.Y = y;
                                    TShock.Players[args.Msg.whoAmI].SendInfoMessage("Set point 1.");
                                }
                                else if (info.Point == 2)
                                {
                                    info.X2 = x;
                                    info.Y2 = y;
                                    TShock.Players[args.Msg.whoAmI].SendInfoMessage("Set point 2.");
                                }
                                info.Point = 0;
                                args.Handled = true;
                                TShock.Players[args.Msg.whoAmI].SendTileSquare(x, y, 3);
                            }
                        }
                    }
                    return;
                    #endregion
            }
            //End of Other voids
        }
    }
}