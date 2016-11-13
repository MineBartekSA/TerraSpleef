using System;
using Terraria;

namespace TerraSpleef
{
    class PlayerInfo
    {
        private int x = -1;
        private int x2 = -1;
        private int y = -1;
        private int y2 = -1;

        public const string KEY = "TerraSpleef_Data";

        public int Point = 0;
        public int X
        {
            get { return x; }
            set { x = Math.Max(0, value); }
        }
        public int X2
        {
            get { return x2; }
            set { x2 = Math.Min(value, Main.maxTilesX - 1); }
        }
        public int Y
        {
            get { return y; }
            set { y = Math.Max(0, value); }
        }
        public int Y2
        {
            get { return y2; }
            set { y2 = Math.Min(value, Main.maxTilesY - 1); }
        }
    }
}
