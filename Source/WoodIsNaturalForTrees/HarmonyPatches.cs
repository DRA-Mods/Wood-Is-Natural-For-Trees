using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace WoodIsNaturalForTrees;

internal static class HarmonyPatches
{
    [HarmonyPatch(typeof(MeditationUtility), nameof(MeditationUtility.CountsAsArtificialBuilding), typeof(Thing))]
    private static class PatchCountsAsArtificialBuildingThing
    {
        private static void Postfix(Thing t, ref bool __result)
        {
            if (!__result ||
                t.Stuff?.stuffProps?.categories == null ||
                !WoodIsNaturalForTreesMod.settings.stuffMadeFrom.Any())
                return;

            if (t.Stuff.stuffProps.categories.All(c => WoodIsNaturalForTreesMod.settings.stuffMadeFrom.Contains(c)))
                __result = false;
        }
    }

    [HarmonyPatch(typeof(MeditationUtility), nameof(MeditationUtility.CountsAsArtificialBuilding), typeof(ThingDef), typeof(Faction))]
    private static class PatchCountsAsArtificialBuildingDef
    {
        private static void Postfix(ThingDef def, ref bool __result)
        {
            if (!__result ||
                Find.DesignatorManager.SelectedDesignator is not Designator_Place place ||
                place.StuffDef?.stuffProps?.categories == null ||
                !WoodIsNaturalForTreesMod.settings.stuffMadeFrom.Any())
                return;

            if (place.StuffDef.stuffProps.categories.All(c => WoodIsNaturalForTreesMod.settings.stuffMadeFrom.Contains(c)))
                __result = false;
        }
    }
}