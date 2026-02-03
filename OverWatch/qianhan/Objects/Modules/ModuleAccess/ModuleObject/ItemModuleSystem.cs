using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject
{
    public static class ItemModuleSystem
    {
        private static readonly ConditionalWeakTable<Item, Dictionary<Type, object>> Module = new();
        private static readonly ConditionalWeakTable<ItemStack, Dictionary<Type, object>> ModuleCache = new();
        /// <summary>
        /// 给物品添加模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="module"></param>
        public static void AttachItemModule<T>(this Item items, T module) where T : class
        {
            if (!Module.TryGetValue(items, out var moduleDict))
            {
                moduleDict = new Dictionary<Type, object>();
                Module.Add(items, moduleDict);
            }
            moduleDict[typeof(T)] = module;
        }
        /// <summary>
        /// 获取物品模块，若没有则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T getItemModule<T>(this Item items) where T : class
        {
            if (Module.TryGetValue(items, out var moduleDict) && moduleDict.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
            return null;
        }
        /// <summary>
        /// 为物品栈添加模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <param name="module"></param>
        public static void AttachStackModule<T>(this ItemStack stack, T module) where T : class
        {
            if (!ModuleCache.TryGetValue(stack, out var moduleDict))
            {
                moduleDict = new Dictionary<Type, object>();
                ModuleCache.Add(stack, moduleDict);
            }
            moduleDict[typeof(T)] = module;
        }
        /// <summary>
        /// 获取物品栈模块，若没有则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static T getStackModule<T>(this ItemStack stack) where T : class
        {
            if (ModuleCache.TryGetValue(stack, out var moduleDict) && moduleDict.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
            return null;
        }
        /// <summary>
        /// 判断对象是否为物品类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsItem(this object obj)
        {
            return obj is Item || obj is ItemStack;
        }
        /// <summary>
        /// 判断对象是否为物品栈类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsItemStack(this object obj)
        {
            return obj is ItemStack;
        }
        /// <summary>
        /// 将对象转换为物品类型，如果转换成功则返回转换的物品，并将结果存储在items中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static void AsItem(this object obj, out Item items)
        {
            if (obj is Item itemObj)
            {
                items = itemObj;
                Debug.Log($"转换成功，物品名称: {itemObj.getItemName()}, 最大耐久: {itemObj.getMaxDamage()}, 最小耐久: {itemObj.getMinDamage()}");
            }
            else
            {
                items = null;
                Debug.LogError("转换失败，无法将对象转换为物品类型。请确保对象是item类型。");
            }
        }
        /// <summary>
        /// 将对象转换为物品栈类型，如果转换成功则返回转换的物品栈，并将结果存储在stack中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="stack"></param>
        public static void AsItemStack(this object obj, out ItemStack stack)
        {
            if (obj is ItemStack itemStack)
            {
                stack = itemStack;
                Debug.Log($"转换成功，物品栈名称: {itemStack.getItemName()}, 数量: {itemStack.count}, 最大数量: {itemStack.maxCount}");
            }
            else
            {
                stack = null;
                Debug.LogError("转换失败，无法将对象转换为物品栈类型。请确保对象是ItemStack类型。");
            }
        }
    }
}
