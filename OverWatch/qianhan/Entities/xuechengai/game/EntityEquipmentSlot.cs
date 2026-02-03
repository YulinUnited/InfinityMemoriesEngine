namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game
{
    public class EntityEquipmentSlot
    {

        public EntityEquipmentSlots Slot { get; private set; }
        public EntityEquipmentSlot(EntityEquipmentSlots slot)
        {
            Slot = slot;
        }
        public override string ToString()
        {
            return Slot.ToString();
        }
        public enum EntityEquipmentSlots
        {
            MAINHAND,
            OFFHAND,
            FEET,
            LEGS,
            CHEST,
            HEAD,

        }
        public enum Type
        {
            HAND,
            ARMOR
        }
    }
}
