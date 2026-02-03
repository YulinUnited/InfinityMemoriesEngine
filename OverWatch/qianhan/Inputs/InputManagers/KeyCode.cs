namespace InfinityMemoriesEngine.OverWatch.qianhan.Inputs.InputManagers
{
    public enum KeyCode
    {
        None = 0,

        // 字母键
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,

        // 数字键
        Alpha0 = 0x30,
        Alpha1 = 0x31,
        Alpha2 = 0x32,
        Alpha3 = 0x33,
        Alpha4 = 0x34,
        Alpha5 = 0x35,
        Alpha6 = 0x36,
        Alpha7 = 0x37,
        Alpha8 = 0x38,
        Alpha9 = 0x39,

        // 小键盘
        Num0 = 0x60,
        Num1 = 0x61,
        Num2 = 0x62,
        Num3 = 0x63,
        Num4 = 0x64,
        Num5 = 0x65,
        Num6 = 0x66,
        Num7 = 0x67,
        Num8 = 0x68,
        Num9 = 0x69,

        // 数学运算符
        ADD = 0x6B,   // Numpad +
        SUB = 0x6D,   // Numpad -
        MUL = 0x6A,   // Numpad *
        DIV = 0x6F,   // Numpad /
        MOD = 0xBA,   // 自定义操作符，可以用于补充

        // 控制键,如果左右键扩充为左右按键，不扩充的话，可能会导致冲突
        Backspace = 0x08,
        Tab = 0x09,
        Enter = 0x0D,
        LeftShift = 0x10,
        RightShift = 0x11,
        CapsLock = 0x14,
        LeftControl = 0x12,
        RightControl = 0x13,
        LeftAlt = 0x15,
        RightAlt = 0x16,
        Escape = 0x1B,
        LeftWin = 0x20,
        RightWin = 0x5C,
        ScrollLock = 0x40,
        Pause = 0x17,

        // 方向键
        LeftArrow = 0x25,
        UpArrow = 0x26,
        RightArrow = 0x27,
        DownArrow = 0x28,

        // 功能键
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,

        // 编辑键
        Insert = 0x2D,
        Delete = 0x2E,
        Home = 0x24,
        End = 0x23,
        PageUp = 0x21,
        PageDown = 0x22,

        // 鼠标键
        Mouse0 = 0x100, // 左键
        Mouse1 = 0x101, // 右键
        Mouse2 = 0x102, // 中键
        Mouse3 = 0x103, // 额外按钮 1
        Mouse4 = 0x104, // 额外按钮 2
        Mouse5 = 0x105, // 额外按钮 3
        Mouse6 = 0x106, // 额外按钮 4
        Mouse7 = 0x107, // 额外按钮 5
        Mouse8 = 0x108, // 额外按钮 6

        // 媒体键
        VolumeMute = 0xAD,
        VolumeDown = 0xAE,
        VolumeUp = 0xAF,
        MediaNextTrack = 0xB0,
        MediaPrevTrack = 0xB1,
        MediaStop = 0xB2,
        MediaPlayPause = 0xB3,
        LaunchMail = 0xB4,
        LaunchMediaSelect = 0xB5,
        LaunchApp1 = 0xB6,
        LaunchApp2 = 0xB7,

        // Windows 键
        LeftWindows = 0x5B,
        RightWindows = 0x5C,
        Apps = 0x5D,

        // 电源管理键
        Sleep = 0x5F,
    }
}
