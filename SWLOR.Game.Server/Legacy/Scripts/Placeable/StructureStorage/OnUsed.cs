﻿using System;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;

namespace SWLOR.Game.Server.Legacy.Scripts.Placeable.StructureStorage
{
    public class OnUsed: IScript
    {
        public void SubscribeEvents()
        {
        }

        public void UnsubscribeEvents()
        {
        }

        public void Main()
        {
            NWPlayer oPC = (NWScript.GetLastUsedBy());
            NWPlaceable container = (NWScript.OBJECT_SELF);
            var structureID = new Guid(container.GetLocalString("PC_BASE_STRUCTURE_ID"));
            
            if (!BasePermissionService.HasStructurePermission(oPC, structureID, StructurePermission.CanAccessStructureInventory))
            {
                oPC.FloatingText("You do not have permission to access this structure.");
                return;
            }
            
            DialogService.StartConversation(oPC, container, "StructureStorage");
        }
    }
}