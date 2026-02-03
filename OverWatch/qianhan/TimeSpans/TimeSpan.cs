using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.TimeSpans
{
    public class TimeSpan : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TS_TICKCB(double tickSec, uint64_t frameIndex, nint userData);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TS_Init(double mainTickSec);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint64_t TS_AddCallback(TS_TICKCB cb, nint userData, int priority);

        // Remove callback by id. safe to call even if id not exist.
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TS_RemoveCallback(uint64_t id);

        // Called every main loop with elapsed seconds; non-blocking.
        // Internally will dispatch callbacks when accumulated >= mainTick.
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TS_Update(double deltaTimeSec);

        // Change main tick interval at runtime (>0)
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TS_SetMainTick(double mainTickSec);

        // Stops and cleans internal state.
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TS_Stop();

        // Get current frame index (monotonic)
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint64_t TS_GetFrameIndex();

        // Version (Major<<16 | Minor)
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint32_t TS_GetVersion();


        public delegate void TickHandler(double tickSec, ulong frameIndex);


        private TS_TICKCB _native;
        private GCHandle _self;
        private TickHandler _handler;
        private ulong _cbId;
        public int Init(double tick, TickHandler handler)
        {
            _handler = handler;
            _native = new TS_TICKCB(TickCallback);
            _self = GCHandle.Alloc(this,GCHandleType.Normal);
            int r = TS_Init(tick);
            if(r==0)
            {
                _cbId = TS_AddCallback(_native, GCHandle.ToIntPtr(_self), 0);
            }
            return r;
        }

        private static void TickCallback(double tick,uint64_t frame,nint user)
        {
            if (user == IntPtr.Zero) return;
            var h = GCHandle.FromIntPtr(user);
            if (h.Target is TimeSpan s)
            {
                try { s._handler?.Invoke(tick, frame); } catch (Exception ex) { Console.WriteLine(ex); }
            }
        }

        public void Update(double dt) => TS_Update(dt);
        public void RemoveCallback() { if (_cbId != 0) { TS_RemoveCallback(_cbId); _cbId = 0; } }
        public void SetMainTick(double t) => TS_SetMainTick(t);
        public ulong FrameIndex => TS_GetFrameIndex();

        public void Dispose()
        {
            if (_cbId != 0) TS_RemoveCallback(_cbId);
            TS_Stop();
            if (_self.IsAllocated) _self.Free();
            _native = null;
            _handler = null;
        }
    }
}
