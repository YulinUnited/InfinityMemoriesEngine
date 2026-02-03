using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using OverWatch.QianHan.Log.network;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities
{
    public class EntityLiving : EntityLivingBase
    {
        public DataParameter<double> TRUEMAXHEALTH = new DataParameter<double>("True_Max_Health");

        public DataParameter<double> TRUECURRENTHEALTH = new DataParameter<double>("True_Current_Health");

        private double trueMaxHealth;

        private double trueCurrentHealth;

        //private static IAttribute HEALTH = new RangedAttribute(null, "EntityLiving.Health", 20000.0D, 0.0D, double.MaxValue).setDescription("Health").setShouldWatch(true);

        public override double getTrueHealth()
        {
            if (dataManagers == null)
            {
                return 0;
            }
            if (double.IsNaN(trueCurrentHealth)) setDeath();
            trueCurrentHealth = dataManagers.get<double>(TRUECURRENTHEALTH);
            return trueCurrentHealth;
        }

        public void setHealth(double health)
        {
            if (double.IsNaN(health))
            {
                Debug.LogError("health为NaN");
                setDeath();
            }
            if (dataManagers == null)
            {
                onEntityStart();
                return;
            }
            double clampValue = Mathf.Mathf_Clamp(health, 0, this.getTrueMaxHealth());
            dataManagers.set(TRUECURRENTHEALTH, clampValue);
            trueCurrentHealth = clampValue;
        }

        public void setMaxHealth(double value)
        {
            if (double.IsNaN(value))
            {
                setDeath();
                return;
            }
            if (value <= 0)
            {
                Debug.LogError($"{value}为零或小于零");
                setDeath();
                return;
            }
            dataManagers.set(TRUEMAXHEALTH, value);
            if (this.getTrueHealth() > value) setHealth(value);
            else
            {
                setHealth(this.getTrueHealth());
            }
        }

        public override double getTrueMaxHealth()
        {
            if (double.IsNaN(trueMaxHealth))
            {
                setDeath();
            }
            if (dataManagers != null)
            {
                if (trueMaxHealth <= 0)
                {
                    Debug.LogError($"{trueMaxHealth}为零或小于零");
                    return 0.0D;
                }
                trueMaxHealth = dataManagers.get<double>(TRUEMAXHEALTH);
                return trueMaxHealth;
            }
            return trueMaxHealth;
        }
    }
}
