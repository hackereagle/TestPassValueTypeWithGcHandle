#pragma once

#include "DllApi.hpp"

struct testStruct
{
	int A = 0;
};

DLL_API void AssignValue(struct testStruct* obj, int val);