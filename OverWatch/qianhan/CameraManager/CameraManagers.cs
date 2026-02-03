using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;
using Boolean = InfinityMemoriesEngine.OverWatch.qianhan.Util.Boolean;
using MainObject = InfinityMemoriesEngine.OverWatch.qianhan.Objects.MainObject;

namespace InfinityMemoriesEngine.OverWatch.qianhan.CameraManager
{
    public unsafe class CameraManagers:MainObject
    {
        
        public enum CameraType:int
        {
            CAMERA_PERSPECTIVE = 0,
            CAMERA_ORTHOGRAPHIC = 1
        }
        [StructLayout(LayoutKind.Sequential, Size = 600, Pack = 8)]
        public struct CameraState{
            public Boolean isActive;
            public Boolean isRemove;
            public Boolean isDead;
            public Boolean forceDead;

            public CameraType type;

            public Vector3 position;
            public Vector3 target;
            public Vector3 up;
            public Vector3 right;
            public Vector3 forward;

            // 透视投影参数
            public double fovY;
            public double aspect;
            public double nearPlane;
            public double farPlane;

            // 正交投影参数
            public double orthoLeft;
            public double orthoRight;
            public double orthoBottom;
            public double orthoTop;

            // 相机控制参数
            public double moveSpeed;
            public double rotationSpeed;
            public double zoomSpeed;

            // 内部状态
            public int dirty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public double[] viewMatrix;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public double[] projMatrix;

            // 平滑跟随和旋转
            public Vector3 targetPosition;
            public Vector3 targetForward;
            public Vector3 velocity;
            public Vector3 angularVelocity;
            public double smoothTime;
            public double lastUpdateTime;
            public Boolean useSmoothFollow;
            public Boolean useSmoothRotation;
        }
        private static readonly MainObject @object;
        public static void Camera_Start()
        {
            @object.isObject = true;
            CameraState* camera = null;
            if (camera == null)
            {
                nuint ptr = (nuint)MixinGC.MixinGC_Allocate((nuint)sizeof(CameraState));
                camera = (CameraState*)ptr;
            }
        }

        // ----------------- 生命周期 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean Camera_Initialize(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean Camera_Update(CameraState* camera, double deltaTime);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_Cleanup(CameraState* camera);

        // ----------------- 状态检查 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean Camera_IsActive(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean Camera_ShouldRemove(CameraState* camera);

        // ----------------- 矩阵获取 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_GetViewMatrix(CameraState* camera, double* viewMatrix);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_GetProjectionMatrix(CameraState* camera, double* projMatrix);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_GetViewProjectionMatrix(CameraState* camera, double* viewProjMatrix);

        // ----------------- 相机控制 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_SetPosition(CameraState* camera, Vector3 position);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_SetTarget(CameraState* camera, Vector3 target);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_SetUpVector(CameraState* camera, Vector3 up);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_Move(CameraState* camera, Vector3 direction, double distance);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_Rotate(CameraState* camera, double yaw, double pitch, double roll);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_Zoom(CameraState* camera, double zoomDelta);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_LookAt(CameraState* camera, Vector3 eye, Vector3 target, Vector3 up);

        // ----------------- 投影设置 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_SetPerspective(CameraState* camera, double fovY, double aspect, double nearPlane, double farPlane);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_SetOrthographic(CameraState* camera, double left, double right, double bottom, double top, double nearPlane, double farPlane);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_SetType(CameraState* camera, CameraType type);

        // ----------------- 参数获取 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Camera_GetPosition(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Camera_GetTarget(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Camera_GetForward(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Camera_GetRight(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Camera_GetUp(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Camera_GetFOV(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Camera_GetAspectRatio(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern CameraType Camera_GetType(CameraState* camera);

        // ----------------- 工具函数 -----------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_UpdateVectors(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_UpdateMatrices(CameraState* camera);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Camera_MarkDirty(CameraState* camera);
    }
}
