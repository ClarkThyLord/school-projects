#ifndef __kmp__
#define __kmp__

int KMP(const char *source, const char *target)
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

#endif
