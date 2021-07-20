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
        Mods.Value.AddItemValue(ref __instance, id, template);
    }
}

[HarmonyPatch(typeof(GClass1746), MethodType.Constructor)]
[HarmonyPatch(new Type[] { typeof(string), typeof(AmmoTemplate) })]
class AmmoValuePatch
{
    static void Postfix(ref GClass1746 __instance, string id, AmmoTemplate template)
    {
        Mods.Value.AddItemValue(ref __instance, id, template);
    }
}

[HarmonyPatch(typeof(GClass1748), MethodType.Constructor)]
[HarmonyPatch(new Type[] { typeof(string), typeof(GClass1648) })]
class GrenadeValuePatch
{
    static void Postfix(ref GClass1748 __instance, string id, GClass1648 template)
    {
        Mods.Value.AddItemValue(ref __instance, id, template);
    }
}

[HarmonyPatch(typeof(GClass1712), MethodType.Constructor)]
[HarmonyPatch(new Type[] { typeof(string), typeof(GClass1615) })]
class ContainerValuePatch
{
    static void Postfix(ref GClass1748 __instance, string id, GClass1615 template)
    {
        Mods.Value.AddItemValue(ref __instance, id, template);
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

        public static void AddItemValue<T>(ref T __instance, string id, ItemTemplate template) where T: Item
        {
            // TODO: remove items of 0 cost
            //__instance.Attributes.RemoveFirst(a => a.Name == "RUB ₽");
            var atts = new List<GClass1758>();
            atts.AddRange(__instance.Attributes);
            __instance.Attributes = atts;

            GClass1758 attr = new GClass1758(EItemAttributeId.MoneySum);
            attr.Name = "RUB ₽";
            attr.StringValue = new Func<string>(__instance.ValueStr);
            attr.DisplayType = new Func<EItemAttributeDisplayType>(() => EItemAttributeDisplayType.Compact);
            __instance.Attributes.Add(attr);
        }
    }

    public static class ValueExtension
    {
        public static double Value(this Item item)
        {
            double price = item.Template.CreditsPrice;

            // Container
            if (item is GClass1662 container)
            {
                foreach (var slot in container.Slots)
                {
                    foreach (var i in slot.Items)
                    {
                        price += i.Value();
                    }
                }
            }

            if (item is GClass1700 mag)
            {
                foreach (var i in mag.Cartridges.Items)
                {
                    price += i.Value();
                }
                
            }
            
            var medKit = item.GetItemComponent<MedKitComponent>();
            if (medKit != null)
            {
                price *= medKit.HpResource / medKit.MaxHpResource;
            }

            var repair = item.GetItemComponent<RepairableComponent>();
            if (repair != null)
            {
                price *= repair.Durability / repair.MaxDurability;
            }

            var dogtag = item.GetItemComponent<DogtagComponent>();
            if (dogtag != null)
            {
                price *= dogtag.Level;
            }

            price *= item.StackObjectsCount;

            return price;
        }
        public static string ValueStr(this Item item)
        {
            return Math.Round(item.Value()).ToString();
        }
    }
}
#endif