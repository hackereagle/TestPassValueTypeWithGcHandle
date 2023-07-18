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

        [DllImport("UnmanagedDll.dll", EntryPoint = "AssignValue", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern void AssignValue(IntPtr obj, int val);

        [StructLayout(LayoutKind.Sequential)]
        public struct TestStruct
        {
            public int A = 0;

            public TestStruct()
            { 
            }

            public TestStruct(int a)
            {
                A = a;
            }

            public override string ToString() 
            {
                return $"TestStruct.A = {A}";
            }
        }

        public void TestGcHandleWrapCsStructPassToUnmanagedDll()
        {
            PrintTestTitle("Test GCHandle wrapped C# struct pass to unmanaged dll");
            TestStruct ts = new TestStruct();
            Console.WriteLine($"Before change value with unmanaged dll. {ts.ToString()}");

            GCHandle gcHandle = GCHandle.Alloc( ts, GCHandleType.Pinned );
            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            AssignValue( ptr, 100 );
            Console.WriteLine($"After change value with unmanaged dll. {ts.ToString()}");
            ts = (TestStruct)gcHandle.Target!;
            Console.WriteLine($"After assign GCHandle.Target back to struct. {ts.ToString()}");

            gcHandle.Free();
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TestClass
        {
            public int A = 0;

            public TestClass()
            { 
            }

            public TestClass(int a)
            {
                A = a;
            }

            public override string ToString() 
            {
                return $"TestClass.A = {A}";
            }
        }

        public void TestGcHandleWrapCsClassPassToUnmanagedDll()
        {
            PrintTestTitle("Test GCHandle wrapped C# class pass to unmanaged dll");
            TestClass tc = new TestClass();
            Console.WriteLine($"Before change value with unmanaged dll. {tc.ToString()}");

            GCHandle gcHandle = GCHandle.Alloc( tc, GCHandleType.Pinned );
            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            AssignValue( ptr, 100 );
            Console.WriteLine($"After change value with unmanaged dll. {tc.ToString()}");

            gcHandle.Free();
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
            TestStruct ts = new TestStruct(20);
            Console.WriteLine($"Before change value with unmanaged dll. {ts.ToString()}");

            int size = Marshal.SizeOf(ts);
#if USE_ALLOC_H_GLOBSL
            IntPtr ptr = Marshal.AllocHGlobal(size);
#else
            IntPtr ptr = Marshal.AllocCoTaskMem(size);
#endif //USE_ALLOC_H_GLOBSL
            Marshal.StructureToPtr(ts, ptr, false );
            AssignValue( ptr, 100 );
            Console.WriteLine($"After change value with unmanaged dll. {ts.ToString()}");
            ts = (TestStruct)Marshal.PtrToStructure( ptr, typeof(TestStruct))!;
            Console.WriteLine($"After assign GCHandle.Target back to struct. {ts.ToString()}");
#if USE_ALLOC_H_GLOBSL
            Marshal .FreeHGlobal( ptr );
#else
            Marshal.FreeCoTaskMem( ptr );
#endif //USE_ALLOC_H_GLOBSL
        }

        public void TestPassClassWithMarshal()
        {
            PrintTestTitle("Test pass C# class with Marshal.");
            TestClass tc = new TestClass(20);
            Console.WriteLine($"Before change value with unmanaged dll. {tc.ToString()}");

            int size = Marshal.SizeOf(tc);
#if USE_ALLOC_H_GLOBSL
            IntPtr ptr = Marshal.AllocHGlobal(size);
#else
            IntPtr ptr = Marshal.AllocCoTaskMem(size);
#endif //USE_ALLOC_H_GLOBSL
            Marshal.StructureToPtr(tc, ptr, false);
            AssignValue( ptr, 100 );
            Console.WriteLine($"After change value with unmanaged dll. {tc.ToString()}");
            tc = (TestClass)Marshal.PtrToStructure(ptr, typeof(TestClass))!;
            Console.WriteLine($"After assign GCHandle.Target back to class. {tc.ToString()}");
#if USE_ALLOC_H_GLOBSL
            Marshal .FreeHGlobal( ptr );
#else
            Marshal.FreeCoTaskMem( ptr );
#endif //USE_ALLOC_H_GLOBSL
        }
    }
}
