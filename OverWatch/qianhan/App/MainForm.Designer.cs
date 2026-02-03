namespace InfinityMemoriesEngine.OverWatch.qianhan.App
{
    internal class MainForms : Form
    {
        private Label label;
        public MainForms()
        {
            Text = "无限回忆";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;
            label = new Label
            {
                Text = "helloWorld",
                Font = new Font("Microsoft YaHei", 24, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            // 将标签添加到窗口
            Controls.Add(label);

            // 添加一个简单的按钮
            var button = new Button
            {
                Text = "点击我",
                Size = new Size(100, 40),
                Location = new Point(150, 200)
            };

            button.Click += (sender, e) =>
            {
                MessageBox.Show("Hello World from Button!", "消息");
            };

            Controls.Add(button);
        }
    }
}