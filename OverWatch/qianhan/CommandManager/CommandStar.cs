using InfinityMemoriesEngine.OverWatch.qianhan.Buttions;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Inputs;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.CommandManager
{
    public class CommandStar
    {
        public Input inputField;
        public Buttons button;
        private Entity entity;
        private DamageSource source;
        public EntityPlayer player;
        internal int commandLevel;

        // Start is called before the first frame update
        public virtual void Start()
        {
            // 绑定按钮点击事件到 ExecuteCommand 方法
            button.onClick.AddListener(OnButtonClick);
            inputField.onEndEdit.AddListener(ExecuteCommand);
            source = new DamageSource();
            player = (EntityPlayer)player.getEntity();
        }

        // 当按钮点击时调用
        public virtual void OnButtonClick()
        {
            ExecuteCommand(inputField.Text);
        }

        public virtual void ExecuteCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            if (command.StartsWith("kill"))
            {
                KillCommands(command);
            }
            else
            {

            }

            inputField.Text = ""; // 清空输入框
        }

        public virtual void KillCommands(string command)
        {
            string[] commandParts = command.Split(' ');
            if (commandParts.Length > 1 && commandParts[1] == "@e")
            {
                if (entity is IEnumerable<Entity> entities)
                {
                    foreach (Entity entity in entities)
                    {
                        if (entity is EntityLivingBase livingBase)
                        {
                            if (livingBase.invulnerable)
                            {
                                livingBase.onKillCommands();
                            }
                        }
                    }
                }
            }
            else if (commandParts.Length > 1 && commandParts[1] == "@p")
            {
                if (entity is EntityLivingBase livingBase)
                {
                    if (livingBase is EntityPlayer entityPlayer)
                    {
                        entityPlayer.onKillCommands();
                    }
                }
            }
            else if (commandParts.Length > 1 && commandParts[1] == "@p")
            {
                EntityLivingBase livingBase = (EntityLivingBase)entity;
                if (livingBase is EntityPlayer)
                {
                    EntityPlayer entityPlayer = (EntityPlayer)livingBase;
                    if (entityPlayer != null)
                    {
                        entityPlayer.onKillCommands();
                    }
                }
            }
        }
    }
}
