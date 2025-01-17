﻿using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.NWN;
using SWLOR.Game.Server.ValueObject;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public class warp_to_wp
#pragma warning restore IDE1006 // Naming Styles
    {
        public static void Main()
        {
            using (new Profiler(nameof(warp_to_wp)))
            {
                NWPlayer player = _.GetPCSpeaker();
                NWObject talkingTo = _.OBJECT_SELF;

                string waypointTag = talkingTo.GetLocalString("DESTINATION");
                NWObject waypoint = _.GetWaypointByTag(waypointTag);

                player.AssignCommand(() => { _.ActionJumpToLocation(waypoint.Location); });
            }
        }
    }
}
