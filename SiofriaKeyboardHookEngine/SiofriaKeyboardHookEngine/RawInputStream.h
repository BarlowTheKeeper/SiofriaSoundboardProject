#pragma once

#include <windows.h>
#include <vector>
#include <functional>
#include <atomic>
#include <string>
#include <iostream>

struct HookRecord 
{
    USHORT virtualKeyCode;
    BOOL keyPressed;

    HookRecord(USHORT wParam, LPARAM lParam)
        : virtualKeyCode(wParam)
        , keyPressed(lParam & 0x80000000 ? 0 : 1)
    {}
};

struct KeyboardRecord {
    USHORT virtualKeyCode;
    BOOL keyPressed;
    std::wstring deviceName; //Name will be "" on RDP sessions

    KeyboardRecord(LPARAM lParam)
    {
        UINT bufferSize = 0;
        GetRawInputData((HRAWINPUT)lParam, RID_INPUT, NULL, &bufferSize, sizeof(RAWINPUTHEADER));
        std::vector<BYTE> dataBuffer(bufferSize, 0);
        GetRawInputData((HRAWINPUT)lParam, RID_INPUT, dataBuffer.data(), &bufferSize, sizeof(RAWINPUTHEADER));
        RAWINPUT* raw = (RAWINPUT*)dataBuffer.data();
        virtualKeyCode = raw->data.keyboard.VKey;
        keyPressed = raw->data.keyboard.Flags & RI_KEY_BREAK ? 0 : 1;
        GetRawInputDeviceInfo(raw->header.hDevice, RIDI_DEVICENAME, NULL, &bufferSize);
        std::vector<wchar_t> stringBuffer(bufferSize, 0);
        GetRawInputDeviceInfo(raw->header.hDevice, RIDI_DEVICENAME, stringBuffer.data(), &bufferSize);
        deviceName = std::wstring(stringBuffer.data());
    }
};

class RawInputStream
{
public:
    RawInputStream(const RawInputStream&) = delete;
    RawInputStream& operator=(const RawInputStream&) = delete;
    RawInputStream(const RawInputStream&&) = delete;
    RawInputStream&& operator=(const RawInputStream&&) = delete;
    ~RawInputStream();

    static RawInputStream& Instance() 
    {
        static RawInputStream instance;
        return instance;
    }

    RawInputStream& Run();
    void Stop();

    RawInputStream& operator|(std::function<void(KeyboardRecord&)> h);
    RawInputStream& operator|(std::function<bool(HookRecord&)> h);

private:
    RawInputStream();
    

    void Open();
    void Close();
    void CreateWnd();
    void RegisterForKeyboardMouseMessages();

    void RegisterWindowClass();
    static LRESULT CALLBACK MessageProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

    const std::wstring wndClassName = L"HighClassOfSiofria";
    const std::wstring wndNAME = L"WindowsOfSiofria";
    WNDCLASSEX wndClass = {0};

    std::function<void(KeyboardRecord&)> pipeRawHandler;
    std::function<bool(HookRecord&)> pipeHookHandler;

    std::function<void(void)> lazyRun;

    std::atomic<HWND> messageHwnd{ NULL };

    bool running{false};
};
