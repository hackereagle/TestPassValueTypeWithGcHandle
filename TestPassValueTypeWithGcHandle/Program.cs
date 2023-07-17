// See https://aka.ms/new-console-template for more information
using TestPassValueTypeWithGcHandle;

Console.WriteLine("Test Pass Value Type to Unmanaged Dll");

TestPassValueTypeClass test = new TestPassValueTypeClass();
test.TestGcHandleWrapCsStructPassToUnmanagedDll();
test.TestGcHandleWrapCsClassPassToUnmanagedDll();
test.TestModifiedValueTypeWithGcHandleAndPointer();
test.TestPassStructWithMarshal();

Console.ReadLine();