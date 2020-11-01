using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.WeaponsmithGuild
{
    public class BatonMS4: AbstractQuest
    {
        public BatonMS4()
        {
            CreateQuest(326, "Weaponsmith Guild Task: 1x Baton MS4", "wpn_tsk_326")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "morningstar_4", 1, true)

                .AddRewardGold(385)
                .AddRewardGuildPoints(GuildType.WeaponsmithGuild, 79);
        }
    }
}