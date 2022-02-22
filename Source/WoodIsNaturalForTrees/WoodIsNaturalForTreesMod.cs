using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Verse;

namespace WoodIsNaturalForTrees
{
    [UsedImplicitly]
    public class WoodIsNaturalForTreesMod : Mod
    {
        private static Harmony harmony;
        internal static Harmony Harmony => harmony ??= new Harmony("Dra.WoodIsNaturalForTrees");
        public static WoodIsNaturalForTreesSettings settings;

        public WoodIsNaturalForTreesMod(ModContentPack content) : base(content)
        {
            LongEventHandler.ExecuteWhenFinished(() => settings = GetSettings<WoodIsNaturalForTreesSettings>());

            Harmony.PatchAll();

            if (ModLister.GetActiveModWithIdentifier("Ludeon.RimWorld.Ideology") == null && ModLister.GetActiveModWithIdentifier("Ludeon.RimWorld.Royalty") == null)
                Log.Error("[WoodIsNaturalForTrees] No supported DLC detected, this mod is pointless");
        }

        public override void DoSettingsWindowContents(Rect inRect) => settings.DoSettingsWindowContents(inRect);

        public override string SettingsCategory() => "Wood is Natural for Trees";
    }
}