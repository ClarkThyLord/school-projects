#!/bin/bash
words=(to acquire a surprising plants but the latter is even able to even from a short exposurepresence or possible presence of explosive gas the surroundings in aportable lamps are available which supply an indirect component as well kindles a fire in using these firesticks they convert mechanical stores the range in values is often much greater appear as bright as it did many years ago which vary in character and volatile liquids are in chemical reactions for example if light is to be used as an accompaniment to music the houses rather slowly at first was obtained at close range)
sfn="workload/Artificial Light.txt"               # source file name
for metric in TLB_DATA ENERGY; do                 # metrics
    tfn=analysis/"$metric"_Artificial_Light.csv   # target file name
    likwid-perfctr -f -C 0-3 -g "$metric" -O ./build/kmp workload/"$sfn" ${words[((RANDOM % ${#words[@]}))]} > "$tfn"
done
