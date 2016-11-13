using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TShockAPI;

namespace TerraSpleef.Extensions
{
    public static class TSPlayerExtensions
    {
        public static PlayerInfo GetPlayerInfo(this TSPlayer tsplayer)
        {
            if (!tsplayer.ContainsData(PlayerInfo.KEY))
                tsplayer.SetData(PlayerInfo.KEY, new PlayerInfo());

            return tsplayer.GetData<PlayerInfo>(PlayerInfo.KEY);
        }
    }
}
