using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using Boolean = InfinityMemoriesEngine.OverWatch.qianhan.Util.Boolean;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.IsC
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 12)]
    public struct Entity
    {
        public Boolean isActive;
        public Boolean isDead;
        public Boolean forceDead;
        public long Entity_id;
        public Boolean isEntity;

        public static Entity From(Entities.Entity obj)
        {
            return new Entity
            {
                isActive = obj.isActive,
                isDead = obj.isDead,
                forceDead = obj.forceDead,
                Entity_id = obj.id,
                isEntity = true
            };
        }
    }

    public unsafe static class Entitys
    {
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Entity* Entity_Create(Boolean isActive, Boolean isDead, Boolean forceDead);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Entity_Destroy(Entity* entity);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setDead();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setDeath();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setEntityAlive(Boolean v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean getEntityAlive();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean getEntity();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void onKillCommand();
    }
}
