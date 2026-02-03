using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Renderers
{
    public enum RendererResult:int
    {
        RENDERER_OK = 0,
        RENDERER_ERR = -1
    }

    public enum RendererPlatform:int
    {
        RENDER_PLATFORM_WIN32 = 1,
        RENDER_PLATFORM_X11 = 2,
        RENDER_PLATFORM_XCB = 3
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 24)]
    public unsafe struct RendererCreateInfo
    {
        public nuint* native_window;
        public RendererPlatform platform;
        public int width;
        public int height;
        public int enableValidation;

    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 200)]
    public unsafe struct CameraData
    {
        public fixed float view[16];
        public fixed float proj[16];
        public fixed float viewProj[16];
        public int viewport_w;
        public int viewport_h;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 48)]
    public unsafe struct MeshUpload
    {
        public nuint* vertices;
        public size_t vertexBytes;
        public nuint* indices;
        public size_t indexBytes;

        public uint32_t vertexCount;
        public uint32_t indexCount;
        public uint32_t vertexStride;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 32)]
    public unsafe struct PointLight
    {
        public fixed float pos[3];
        public float radius;
        public fixed float color[3];
        public float intensity;
    }

    public unsafe static class Renderer_Vulkan
    {
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererResult Renderer_Create(RendererCreateInfo* ci);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Renderer_Destroy();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererResult Renderer_OnResize(int width, int height);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererResult Renderer_BeginFrame(CameraData* cam);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern RendererResult Renderer_SubmitMesh(MeshUpload* mesh, float* modelMat, uint32_t materialId);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererResult Renderer_EndFrame();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererResult Renderer_Present();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern RendererResult Renderer_SetPointLights(PointLight* lights, uint32_t count);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint32_t Renderer_CreateMaterial(byte* name);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Renderer_DestroyMaterial(uint32_t id);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint Renderer_GetLastError();
    }

}
