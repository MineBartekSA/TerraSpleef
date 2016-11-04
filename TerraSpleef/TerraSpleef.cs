using System;
using System.Collections.Generic;
using System.Threading;
using TShockAPI;
using TShockAPI.DB;
using TerrariaApi.Server;
using Terraria;

namespace TerraSpleef
{
    public class TerraSpleef : TerrariaPlugin
    {
        //Vars

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

            //Hooks
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //UnHooks
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
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

            switch(basePar)
            {
                case "info":
                    {

                        break;
                    }

                case "start":
                    {
                        string aName = args.Parameters[1];

                        if(aName == "")
                        {
                            play.SendErrorMessage("Wrong command usage!");
                            play.SendErrorMessage("/tspleef start <arena name> <number of players>");
                            break;
                        }

                        List<string> lNamesArea = sqd.getNames();

                        foreach (string a in lNamesArea)
                        {
                            if(a == aName)
                            {
                                area.name = aName;
                            }
                        }

                        if(area.name == null)
                        {
                            play.SendErrorMessage("Wrong area name!");
                            break;
                        }



                        break;
                    }

                case "create": // /tspleef create name ... /tspleef create spawnpos  ... /tspleef create specpos ... /tspleef create save
                    {

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
        //End of Other voids
    }
}
