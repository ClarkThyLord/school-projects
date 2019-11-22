#include <sstream>
#include <fstream>
#include <iostream>
#include "kmp.hpp"

using namespace std;

int main(int argc, const char *argv[])
{
    std::ifstream inFile;
    inFile.open(argv[1]);

    std::stringstream strStream;
    strStream << inFile.rdbuf();
    std::string src = strStream.str();

    cout << "inicie" << std::endl;
    KMP(src.c_str(), argv[2]);
    cout << "termine" << std::endl;

    return 0;
}
