using System;
using System.Runtime.InteropServices;

namespace NfdSharp
{
    public static class Nfd
    {
        public enum NfdResult
        {
            NFD_ERROR,
            NFD_OKAY,
            NFD_CANCEL
        }

        public struct NfdPathset
        {
            public UIntPtr buf;
            public UIntPtr indices;
            public ulong count;
        }

        public static NfdResult OpenDialog(string filterList, string defaultPath, out string path)
        {
            IntPtr filterListPtr = Utils.ToNfdString(filterList);
            IntPtr defaultPathPtr = Utils.ToNfdString(defaultPath);
            
            var res = NFD_OpenDialog(filterListPtr, defaultPathPtr, out IntPtr outPath);

            Marshal.FreeHGlobal(filterListPtr);
            Marshal.FreeHGlobal(defaultPathPtr);
            
            path = res != NfdResult.NFD_OKAY ? "" : Utils.FromNfdString(outPath);
            
            Marshal.FreeHGlobal(outPath);
            
            return res;
        }
        
        public static NfdResult OpenDialogMultiple(string filterList, string defaultPath, out string[] outPaths)
        {
            IntPtr filterListPtr = Utils.ToNfdString(filterList);
            IntPtr defaultPathPtr = Utils.ToNfdString(defaultPath);

            var res = NFD_OpenDialogMultiple(filterListPtr, defaultPathPtr, out NfdPathset paths);

            Marshal.FreeHGlobal(filterListPtr);
            Marshal.FreeHGlobal(defaultPathPtr);
            
            if (res == NfdResult.NFD_OKAY)
            {
                ulong count = paths.count;

                outPaths = count != 0 ? new string[count] : null;
            
                for (ulong i = 0; i < count; i++)
                {
                    outPaths[i] = Utils.FromNfdString(NFD_PathSet_GetPath(ref paths, i));
                }
                
                NFD_PathSet_Free(ref paths);
            }
            else
            {
                outPaths = null;
            }

            return res;
        }

        public static NfdResult SaveDialog(string filterList, string defaultPath, out string path)
        {
            IntPtr filterListPtr = Utils.ToNfdString(filterList);
            IntPtr defaultPathPtr = Utils.ToNfdString(defaultPath);
            
            var res = NFD_SaveDialog(filterListPtr, defaultPathPtr, out IntPtr outPath);

            Marshal.FreeHGlobal(filterListPtr);
            Marshal.FreeHGlobal(defaultPathPtr);
            
            path = res != NfdResult.NFD_OKAY ? "" : Utils.FromNfdString(outPath);
            
            Marshal.FreeHGlobal(outPath);
            
            return res;
        }

        public static NfdResult PickFolder(string defaultPath, out string path)
        {
            IntPtr defaultPathPtr = Utils.ToNfdString(defaultPath);
            
            var res = NFD_PickFolder(defaultPathPtr, out IntPtr outPath);

            Marshal.FreeHGlobal(defaultPathPtr);
            
            path = res != NfdResult.NFD_OKAY ? "" : Utils.FromNfdString(outPath);
            
            Marshal.FreeHGlobal(outPath);
            
            return res;
        }

        public static string GetError()
        {
            IntPtr message = NFD_GetError();
            string ret = Utils.FromNfdString(message);
            Marshal.FreeHGlobal(message);
            
            return ret;
        }

        #region DllImports

        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern NfdResult NFD_OpenDialog(
            IntPtr filterList,
            IntPtr defaultPath,
            out IntPtr outPath);
        
        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern NfdResult NFD_OpenDialogMultiple(
            IntPtr filterList,
            IntPtr defaultPath,
            out NfdPathset outPaths);
        
        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern NfdResult NFD_SaveDialog(
            IntPtr filterList,
            IntPtr defaultPath,
            out IntPtr outPath);
        
        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern NfdResult NFD_PickFolder(
            IntPtr defaultPath,
            out IntPtr outPath);
        
        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr NFD_GetError();

        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern ulong NFD_PathSet_GetCount(ref NfdPathset pathSet);
        
        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr NFD_PathSet_GetPath(ref NfdPathset pathSet, ulong index);
        
        [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
        private static extern void NFD_PathSet_Free(ref NfdPathset pathSet);

        #endregion
    }
}
