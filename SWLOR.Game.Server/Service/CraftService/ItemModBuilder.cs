﻿using System.Collections.Generic;

namespace SWLOR.Game.Server.Service.CraftService
{
    public class ItemModBuilder
    {
        private readonly Dictionary<string, ItemModDetail> _itemMods = new Dictionary<string, ItemModDetail>();
        private ItemModDetail _activeItemMod;

        /// <summary>
        /// Creates a new item mod with the specified key.
        /// </summary>
        /// <param name="tag">The tag of the item mod.</param>
        /// <param name="modType">The type of item mod.</param>
        /// <returns>An item mod builder with the configured options.</returns>
        public ItemModBuilder Create(string tag, ItemModType modType)
        {
            _activeItemMod = new ItemModDetail
            {
                ModType = modType
            };
            _itemMods[tag] = _activeItemMod;

            return this;
        }

        /// <summary>
        /// Sets the name of the item mod.
        /// </summary>
        /// <param name="name">The name of the item mod.</param>
        /// <returns>An item mod builder with the configured options.</returns>
        public ItemModBuilder Name(string name)
        {
            _activeItemMod.Name = name;

            return this;
        }

        /// <summary>
        /// Sets the action which will run when the item mod is successfully applied.
        /// </summary>
        /// <param name="action">The action to run.</param>
        /// <returns>An item mod builder with the configured options.</returns>
        public ItemModBuilder ApplyAction(ApplyItemModDelegate action)
        {
            _activeItemMod.ApplyItemModAction = action;

            return this;
        }

        /// <summary>
        /// Returns a built dictionary of item mod details.
        /// </summary>
        /// <returns>A dictionary of item mod details.</returns>
        public Dictionary<string, ItemModDetail> Build()
        {
            return _itemMods;
        }
    }
}
