

#include <iostream>
#include "RawInputListener.h"

/*
Huuuge credit to 
Vít Blecha

Who pointed me in the right direction @ https://www.codeproject.com/Articles/716591/Combining-Raw-Input-and-keyboard-Hook-to-selective
*/

int main()
{
    std::cout << "Started..\n";
    std::make_shared<RawInputListener>()->Start();
    std::cout << "Ended..\n";
}
