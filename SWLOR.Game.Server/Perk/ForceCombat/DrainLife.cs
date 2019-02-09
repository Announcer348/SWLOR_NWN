﻿using NWN;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Service.Contracts;
using static NWN.NWScript;

namespace SWLOR.Game.Server.Perk.ForceCombat
{
    public class DrainLife: IPerk
    {
        private readonly INWScript _;
        private readonly ISkillService _skill;
        private readonly ICombatService _combat;
        private readonly IPerkService _perk;
        private readonly IRandomService _random;
        private readonly IPlayerStatService _stat;

        public DrainLife(
            INWScript script,
            ISkillService skill,
            ICombatService combat,
            IPerkService perk,
            IRandomService random,
            IPlayerStatService stat)
        {
            _ = script;
            _skill = skill;
            _combat = combat;
            _perk = perk;
            _random = random;
            _stat = stat;
        }

        public bool CanCastSpell(NWPlayer oPC, NWObject oTarget)
        {
            if (_.GetDistanceBetween(oPC, oTarget) > 15.0f)
                return false;

            // Must be used on creatures which are organic.
            if (oTarget.IsCreature)
            {
                NWCreature creature = oTarget.Object;

                if (creature.RacialType == (int) CustomRaceType.Robot)
                {
                    return false;
                }
            }
            else return false;

            return true;
        }

        public string CannotCastSpellMessage(NWPlayer oPC, NWObject oTarget)
        {
            if (_.GetDistanceBetween(oPC, oTarget) > 15.0f)
                return "Target out of range.";

            // Must be used on creatures which are organic.
            if (oTarget.IsCreature)
            {
                NWCreature creature = oTarget.Object;

                if (creature.RacialType == (int) CustomRaceType.Robot)
                {
                    return "This force ability may only be used on organic targets.";
                }
            }
            else return "This force ability may only be used on organic targets.";

            return string.Empty;
        }


        public int FPCost(NWPlayer oPC, int baseFPCost, int spellFeatID)
        {
            return baseFPCost;
        }

        public float CastingTime(NWPlayer oPC, float baseCastingTime, int spellFeatID)
        {
            return baseCastingTime;
        }

        public float CooldownTime(NWPlayer oPC, float baseCooldownTime, int spellFeatID)
        {
            return baseCooldownTime;
        }

        public int? CooldownCategoryID(NWPlayer oPC, int? baseCooldownCategoryID, int spellFeatID)
        {
            return baseCooldownCategoryID;
        }

        public void OnImpact(NWPlayer player, NWObject target, int perkLevel, int spellFeatID)
        {
            float recoveryPercent;
            int basePotency;
            const float Tier1Modifier = 1;
            const float Tier2Modifier = 2;
            const float Tier3Modifier = 0;
            const float Tier4Modifier = 0;

            switch (perkLevel)
            {
                case 1:
                    basePotency = 10;
                    recoveryPercent = 0.2f;
                    break;
                case 2:
                    basePotency = 15;
                    recoveryPercent = 0.2f;
                    break;
                case 3:
                    basePotency = 20;
                    recoveryPercent = 0.4f;
                    break;
                case 4:
                    basePotency = 25;
                    recoveryPercent = 0.4f;
                    break;
                case 5:
                    basePotency = 30;
                    recoveryPercent = 0.5f;
                    break;
                default: return;
            }

            var effectiveStats = _stat.GetPlayerItemEffectiveStats(player);
            int luck = _perk.GetPCPerkLevel(player, PerkType.Lucky) + effectiveStats.Luck;
            if (_random.Random(100) + 1 <= luck)
            {
                recoveryPercent = 1.0f;
                player.SendMessage("Lucky drain life!");
            }

            var calc = _combat.CalculateForceDamage(
                player, 
                target.Object, 
                ForceAbilityType.Dark, 
                basePotency,
                Tier1Modifier,
                Tier2Modifier,
                Tier3Modifier,
                Tier4Modifier);
            
            _.AssignCommand(player, () =>
            {
                int heal = (int)(calc.Damage * recoveryPercent);
                if (heal > target.CurrentHP) heal = target.CurrentHP;

                _.ApplyEffectToObject(DURATION_TYPE_INSTANT, _.EffectDamage(calc.Damage), target);
                _.ApplyEffectToObject(DURATION_TYPE_INSTANT, _.EffectHeal(heal), player);
                _.ApplyEffectToObject(DURATION_TYPE_TEMPORARY, _.EffectVisualEffect(VFX_BEAM_MIND), target, 1.0f);
            });
            
            _skill.RegisterPCToAllCombatTargetsForSkill(player, SkillType.ForceCombat, target.Object);
            _combat.AddTemporaryForceDefense(target.Object, ForceAbilityType.Dark);
        }

        public void OnPurchased(NWPlayer oPC, int newLevel)
        {
        }

        public void OnRemoved(NWPlayer oPC)
        {
        }

        public void OnItemEquipped(NWPlayer oPC, NWItem oItem)
        {
        }

        public void OnItemUnequipped(NWPlayer oPC, NWItem oItem)
        {
        }

        public void OnCustomEnmityRule(NWPlayer oPC, int amount)
        {
        }

        public bool IsHostile()
        {
            return false;
        }
    }
}