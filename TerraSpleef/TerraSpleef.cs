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


        }
        //End of Commands exe voids



        //Other voids
        //End of Other voids
    }
}
