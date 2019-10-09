import math
import numpy as np

def code(msg, A):
    A = np.array(A)
    codes = []

    i = 0
    for char in msg:
        if i % 3 == 0: codes.append([])
        
        codes[math.floor(i / 3)].append([ord(char) - 96])
        i += 1
    
    while len(codes[-1]) < 3: codes[-1].append([-61])


    res = []

    i = 0
    for code_seg in codes:
        res.append(A.dot((np.array(codes[i]))).tolist())
        i += 1

    ress = []
    for x in res:
        for y in x:
            for z in y: ress.append(z)
    
    return ress

def decode(code, A):
    A_inv = np.invert(np.array(A))
    codes = []

    i = 0
    for sub_code in code:
        if i % 3 == 0: codes.append([])
        
        codes[math.floor(i / 3)].append(sub_code)
        i += 1


    res = []

    i = 0
    for code_seg in codes:
        res.append(A_inv.dot((np.array(codes[i]))).tolist())
        i += 1

    ress = []
    for x in res:
        for y in x:
            for z in y: ress.append(z)
    
    return ress

if __name__== "__main__":
    A = [[1, 2, 3], [1, 1, 2], [0, 1, 2]]
    print(code('citaelmartes', A))
    print(decode(code('citaelmartes', A), A))
