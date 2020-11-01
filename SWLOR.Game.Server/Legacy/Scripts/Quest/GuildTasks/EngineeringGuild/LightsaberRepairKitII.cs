using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.EngineeringGuild
{
    public class LightsaberRepairKitII: AbstractQuest
    {
        public LightsaberRepairKitII()
        {
            CreateQuest(433, "Engineering Guild Task: 1x Lightsaber Repair Kit II", "eng_tsk_433")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "ls_rep_2", 1, true)

                .AddRewardGold(220)
                .AddRewardGuildPoints(GuildType.EngineeringGuild, 48);
        }
    }
}