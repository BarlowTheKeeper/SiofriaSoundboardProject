#include "RawInputListener.h"
#include <iostream>
#include <fstream>

#define RETURN_KEYCODE 13

void RawInputListener::KeyboardHandler(const KeyboardRecord& record)
{
    if (record.keyPressed == 0)
        return;

    std::cout << "keydode: " << record.virtualKeyCode << '\n';

    if (deviceName.empty() && record.virtualKeyCode == RETURN_KEYCODE)
    {
        deviceName = record.deviceName;
        std::wcout << "Device name recorded: " << deviceName << '\n';
        return;
    }

    bool block = !deviceName.empty() && (deviceName == record.deviceName);
    decisionBuffer.push_back(DecisionRecord(record.virtualKeyCode, block)); //Block all from this keyboard
    
}

bool RawInputListener::HookHandler(const HookRecord& record)
{
    if (record.keyPressed == 0)
        return false;

    std::cout << "Message from WM_HOOK: " << record.virtualKeyCode << '\n';

    bool blockThisHook = false;
    bool recordFound = false;

    int index = 1;
    if (!decisionBuffer.empty())
    {
        std::deque<DecisionRecord>::iterator iterator = decisionBuffer.begin();
        while (iterator != decisionBuffer.end())
        {
            if (iterator->virtualKeyCode == record.virtualKeyCode)
            {
                blockThisHook = iterator->decision;
                recordFound = true;
                for (int i = 0; i < index; ++i)
                    decisionBuffer.pop_front();
                
                break;
            }
            ++iterator;
            ++index;
        }
    }

    if (blockThisHook && record.keyPressed)
    {
        std::cout << "Writing to file: " << record.virtualKeyCode << '\n';
        std::ofstream ofs("blocked_input.txt");
        ofs << record.virtualKeyCode << "\n";
        ofs.close();
    }
    return blockThisHook;
}

void RawInputListener::Loop()
{
    std::cout << "Starting the message stream.\n";
    
    messageStream.Run() 
    | 
    [self{ shared_from_this() }](HookRecord& r)
    {
        return self->HookHandler(r);
    }
    |
    [self{ shared_from_this() }](KeyboardRecord& r)
    {
        self->KeyboardHandler(r);
    };
    
    
}

RawInputListener::RawInputListener()
    :messageStream(RawInputStream::Instance())
{};

void RawInputListener::Start() 
{
    try
    {
        Loop();
    }
    catch (const std::exception& e) {
        std::cerr << "Error in Loop method: " << std::string(e.what()) << '\n';
    }
}

RawInputListener::~RawInputListener()
{
    messageStream.Stop();
};

