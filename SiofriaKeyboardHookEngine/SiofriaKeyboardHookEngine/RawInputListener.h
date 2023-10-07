#pragma once

#include <windows.h>
#include <queue>
#include <string>
#include <array>
#include <thread>
#include <memory>
#include "RawInputStream.h"

struct DecisionRecord
{
	USHORT virtualKeyCode;
	BOOL decision;

	DecisionRecord(USHORT _virtualKeyCode, BOOL _decision) : virtualKeyCode(_virtualKeyCode), decision(_decision) {}
};

class RawInputListener : public std::enable_shared_from_this<RawInputListener>
{

private:

	void Loop();

	HINSTANCE hInst;
	std::array<TCHAR, 128> szTitle;
	std::array<TCHAR, 128> szWindowClass;

	HWND mainHwnd;
	std::wstring deviceName = L"";
	std::deque<DecisionRecord> decisionBuffer;

	RawInputStream& messageStream;

	void KeyboardHandler(const KeyboardRecord& record);
	bool HookHandler(const HookRecord& record);
	

public:
	RawInputListener(const RawInputListener&) = delete;
	RawInputListener& operator=(const RawInputListener&) = delete;
	RawInputListener(const RawInputListener&&) = delete;
	RawInputListener&& operator=(const RawInputListener&&) = delete;

	RawInputListener();
	~RawInputListener();
	void Start();

	inline bool hasDeviceName() { return !deviceName.empty(); }
};