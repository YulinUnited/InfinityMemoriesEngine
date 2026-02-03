namespace InfinityMemoriesEngine.OverWatch.qianhan.App
{
    internal class MixinMainForm:Form
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new MainForms());
            //ApplicationConfiguration.Initialize();
        }
    }
}

