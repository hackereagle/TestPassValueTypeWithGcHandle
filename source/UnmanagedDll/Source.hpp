#pragma once

#include "DllApi.hpp"

struct testStruct
{
	int A = 0;

	testStruct() = default;
	testStruct(int a) : A(a) {}
	testStruct(testStruct& val) = default;
	testStruct(testStruct&& val) = default;
};

DLL_API void AssignValue(struct testStruct* obj, int val);