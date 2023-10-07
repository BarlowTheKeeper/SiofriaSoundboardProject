#pragma once

#include <Windows.h>

#ifdef HOOKINGRAWINPUTDEMODLL_EXPORTS
#define HOOKINGRAWINPUTDEMODLL_API __declspec(dllexport)
#else
#define HOOKINGRAWINPUTDEMODLL_API __declspec(dllimport)
#endif

HOOKINGRAWINPUTDEMODLL_API BOOL InstallHook(HWND hwndParent);

HOOKINGRAWINPUTDEMODLL_API BOOL UninstallHook();
