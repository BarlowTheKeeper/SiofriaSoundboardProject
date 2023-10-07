#include "pch.h"
#include "RawInputStream.h"
#pragma comment(lib, "./LowLevelHooksDll.lib")
#include "dllHooks.h"
#include <string>
#include <stdexcept>
#include <iostream>

constexpr UINT const WM_HOOK = WM_APP + 1;

RawInputStream::RawInputStream()
{
    RegisterWindowClass();
}

RawInputStream::~RawInputStream()
{
    if(running) Stop();
}

void RawInputStream::Open()
{
    CreateWnd();
    RegisterForKeyboardMouseMessages();
    std::cout << "Hooked? " << InstallHook(messageHwnd) << "\n";
    running = true;
}

void RawInputStream::Close()
{
    if (UnregisterClass(wndClass.lpszClassName, NULL) == FALSE){}
    running = false;
}

RawInputStream& RawInputStream::Run()
{
    lazyRun = [&]() {
        Open();
        MSG m;
        while (GetMessage(&m, NULL, 0, 0))
        {
            TranslateMessage(&m);
            DispatchMessage(&m);

           
        }
        Close();
    };
    std::cout << "Prerpared to run.\n";
    return *this;
}

void RawInputStream::Stop()
{
    if(PostMessage(messageHwnd, WM_DESTROY, 0, 0) == FALSE)
        std::cerr << "Failed to stop the RawInputStream!\n";
}

LRESULT CALLBACK RawInputStream::MessageProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    RawInputStream& self = RawInputStream::Instance();
    if (msg == WM_INPUT)
    {
        KeyboardRecord record(lParam);
        self.pipeRawHandler(record);
    }
    else if (msg == WM_HOOK)
    {
        HookRecord record(wParam, lParam);

        return self.pipeHookHandler(record);
    }

    //handle return value as should block hook?
    else if ((msg == WM_DESTROY && hWnd == self.messageHwnd))
    {
        UninstallHook();
        PostQuitMessage(0);
    }

    return DefWindowProc(hWnd, msg, wParam, lParam);
}

void RawInputStream::RegisterWindowClass()
{
    if(wndClass.lpszClassName != NULL)
        return;
    ZeroMemory(&wndClass, sizeof(wndClass));
    wndClass.cbSize = sizeof(wndClass);
    wndClass.lpfnWndProc = MessageProc;
    wndClass.lpszClassName = wndClassName.c_str();

    if(RegisterClassEx(&wndClass) == 0)
        throw std::runtime_error("Message-only Window class creation failed: " + std::to_string(GetLastError()));
}

void RawInputStream::CreateWnd()
{
    messageHwnd = CreateWindowEx(0, wndClass.lpszClassName, wndNAME.c_str(), 0, 0, 0, 0, 0, HWND_MESSAGE, NULL, NULL, NULL);
    if (messageHwnd == NULL)
        throw std::runtime_error("Message-only Window creation failed: " + std::to_string(GetLastError()));
}

void RawInputStream::RegisterForKeyboardMouseMessages()
{
    RAWINPUTDEVICE rid[2] = {
        { 0x1, 0x6, RIDEV_INPUTSINK, messageHwnd}, //keyboard
        { 0x1, 0x7, RIDEV_INPUTSINK, messageHwnd}, //keyboard
    };

    if (RegisterRawInputDevices(rid, 2, sizeof(rid[0])) == FALSE)
        throw std::runtime_error("Registering for keyboard and mouse messages failed: " + std::to_string(GetLastError()));
}

RawInputStream& RawInputStream::operator|(std::function<void(KeyboardRecord&)> h)
{
    pipeRawHandler = h;
    std::cout << "PipeRawHandler set.\n";
    if (lazyRun)
    {
        std::cout << "Running lazyrun.\n";
        lazyRun();
    }

    return *this;
}

RawInputStream& RawInputStream::operator|(std::function<bool(HookRecord&)> h)
{
    pipeHookHandler = h;
    std::cout << "pipeHookHandler set.\n";
    return *this;
}