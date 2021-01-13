﻿using System.Linq;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using PerkType = SWLOR.Game.Server.Legacy.Enumeration.PerkType;

namespace SWLOR.Game.Server.Legacy.Scripts.Placeable.ScavengePoint
{
    public class OnDisturbed: IScript
    {
        public void SubscribeEvents()
        {
        }

        public void UnsubscribeEvents()
        {
        }

        public void Main()
        {
            NWPlayer oPC = (NWScript.GetLastDisturbed());
            if (!oPC.IsPlayer) return;

            NWItem oItem = (NWScript.GetInventoryDisturbItem());
            NWPlaceable point = (NWScript.OBJECT_SELF);
            var disturbType = NWScript.GetInventoryDisturbType();

            if (disturbType == DisturbType.Added)
            {
                ItemService.ReturnItem(oPC, oItem);
            }
            else
            {
                if (!point.InventoryItems.Any() && point.GetLocalInt("SCAVENGE_POINT_FULLY_HARVESTED") == 1)
                {
                    var seed = point.GetLocalString("SCAVENGE_POINT_SEED");
                    if (!string.IsNullOrWhiteSpace(seed))
                    {
                        NWScript.CreateObject(ObjectType.Item, seed, point.Location);

                        var perkLevel = PerkService.GetCreaturePerkLevel(oPC, PerkType.SeedPicker);
                        if (SWLOR.Game.Server.Service.Random.Next(100) + 1 <= perkLevel * 10)
                        {
                            NWScript.CreateObject(ObjectType.Item, seed, point.Location);
                        }
                    }

                    point.Destroy();
                }
            }
        }
    }
}