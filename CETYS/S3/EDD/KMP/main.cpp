#include <iostream>
using namespace std;

int KMP(char *source, char *target)
{
    for (int i = 0; source[i]; i++)
    {
        int offset = 0;
        for (int n = 0; source[n]; n++)
        {
            if (n > 0 && source[i + n] == target[0])
                offset = n - 1;
            if (source[i + n] != target[n])
                break;
            else if (target[n + 1] == '\0')
                return i;
        }
        i += offset;
    }
    return -1;
}

int main()
{
    char *source = "Hello world!";
    char *target = "llo";
    cout << "Source: " << source << " | Target: " << target << " | Match: " << KMP(source, target);

    source = "Somethingrealbiglikethistest!";
    target = "thi";
    cout << "\nSource: " << source << " | Target: " << target << " | Match: " << KMP(source, target);

    source = "ABCBCAA!";
    target = "BCA";
    cout << "\nSource: " << source << " | Target: " << target << " | Match: " << KMP(source, target);

    return 0;
}
