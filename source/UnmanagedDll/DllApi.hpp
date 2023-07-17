#pragma once

#ifdef EXPORT_DLL
#define DLL_API extern "C" __declspec(dllexport)
#else
#define DLL_API extern "C" __declspec(dllimport)
#endif // EXPORT_DLL