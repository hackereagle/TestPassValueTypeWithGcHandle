using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestPassValueTypeWithGcHandle
{
    internal class TestPassValueTypeClass
    {
        public TestPassValueTypeClass() { }

        void PrintTestTitle(string title)
        { 
            Console.WriteLine($"\n\n===== {title} =====");
        }

        [DllImport("./UnmanagedDll.dll", EntryPoint = "AssignValue", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern void AssignValue(IntPtr obj, int val);

        [StructLayout(LayoutKind.Sequential)]
        public struct TestStruct
        {
            public int A = 0;

            public TestStruct()
            { 
            }
        }

        public void TestGcHandleWrapCsStructPassToUnmanagedDll()
        {
            PrintTestTitle("Test GCHandle wrapped C# struct pass to unmanaged dll");
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TestClass
        {
            public int A = 0;

            public TestClass()
            { 
            }
        }

        public void TestGcHandleWrapCsClassPassToUnmanagedDll()
        {
            PrintTestTitle("Test GCHandle wrapped C# class pass to unmanaged dll");
        }

        public void TestModifiedValueTypeWithGcHandleAndPointer()
        {
            PrintTestTitle("Test modified value type variable with GCHandle and pointer.");
            unsafe
            {
                int i = 0;

                object oi = i;
                var gcHandle = GCHandle.Alloc(oi, GCHandleType.Pinned);
                int* pi = (int*)gcHandle.AddrOfPinnedObject();
                *pi += 1;
                Console.WriteLine($"Through GCHandle to get value type address and add: origin = {i}, after GCHandle changed value = {*pi} and the value of {nameof(gcHandle.Target)} = {gcHandle.Target}");

                int* ptr_i = &i;
                *ptr_i += 1;
                Console.WriteLine($"Got pointer of value type directly and add: origin = {i}, after pointer changed value = {*ptr_i}");
            }
        }

        public void TestPassStructWithMarshal()
        {
            PrintTestTitle("Test pass C# struct with Marshal.");
        }
    }
}
