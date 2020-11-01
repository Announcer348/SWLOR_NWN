using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.ArmorsmithGuild
{
    public class AdditionalFuelTankSmall: AbstractQuest
    {
        public AdditionalFuelTankSmall()
        {
            CreateQuest(169, "Armorsmith Guild Task: 1x Additional Fuel Tank (Small)", "arm_tsk_169")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "ssfuel1", 1, true)

                .AddRewardGold(305)
                .AddRewardGuildPoints(GuildType.ArmorsmithGuild, 64);
        }
    }
}