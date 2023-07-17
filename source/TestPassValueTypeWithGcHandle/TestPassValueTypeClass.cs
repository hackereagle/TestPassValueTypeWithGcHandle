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
        }

        public void TestPassStructWithMarshal()
        {
            PrintTestTitle("Test pass C# struct with Marshal.");
        }
    }
}
