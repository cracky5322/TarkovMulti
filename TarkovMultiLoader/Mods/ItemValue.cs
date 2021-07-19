#if ITEMVALUE
using System;
using System.Collections.Generic;
using HarmonyLib;
using EFT.InventoryLogic;

[HarmonyPatch(typeof(Item), MethodType.Constructor)]
[HarmonyPatch(new Type[] { typeof(string), typeof(ItemTemplate) })]
class ItemValuePatch
{
    static void Postfix(ref Item __instance, string id, ItemTemplate template)
    {

        GClass1758 attr = new GClass1758(EItemAttributeId.MoneySum);
        attr.Name = "RUB ₽";
        attr.StringValue = new Func<string>(priceCalc(__instance, template));
        attr.DisplayType = new Func<EItemAttributeDisplayType>(() => EItemAttributeDisplayType.Compact);
        __instance.Attributes.Add(attr);
    }

    static Func<string> priceCalc(Item __instance, ItemTemplate template) {
        return () =>
        {
            float price = template.CreditsPrice;

            var medKit = __instance.GetItemComponent<MedKitComponent>();
            if (medKit != null)
            {
                price *= medKit.HpResource / medKit.MaxHpResource;
            }

            var repair = __instance.GetItemComponent<RepairableComponent>();
            if (repair != null)
            {
                price *= repair.Durability / repair.MaxDurability;
            }

            return Math.Round(price).ToString();
        };
    }

}

namespace Mods
{
    public class Value : BaseMod
    {
        override public void Init(Dictionary<string, string> options)
        {
            base.Init(options);
            var harmony = new HarmonyLib.Harmony("com.kcy.ItemValuePatch");
            harmony.PatchAll();
        }
    }
}
#endif