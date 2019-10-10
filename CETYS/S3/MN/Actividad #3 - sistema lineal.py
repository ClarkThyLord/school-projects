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
    A_inv = np.linalg.inv(A)
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

    ress = ''
    for x in res:
        for y in x:
            ress += chr(96 + int(y))
    
    return ress

def vector_transformation(vector, A):
    t1 = None
    t2 = None
    vector = np.array(vector)
    A = np.array(A)
    for i in range(10):
        t1 = vector * np.linalg.matrix_power(A, 2)
        t2 = vector * np.linalg.inv(A)
    return t1.tolist(), t2.tolist()

def time_optimization():
    pass

if __name__== "__main__":
    A = [[1, 2, 3], [1, 1, 2], [0, 1, 2]]
    print(code('citaelmartes', A))
    print(decode(code('citaelmartes', A), A))

    print(code('enviadolares', A))
    print(decode([85, 58, 39, 70, 45, 30, 73,51, 37, 91, 57, 53], A))

    ang = math.pi / 6
    A = [[math.cos(ang), -math.sin(ang)], [math.sin(ang), math.cos(ang)]]
    print(vector_transformation([[1, 1]], A))
    print(vector_transformation([[2.5, 2.5]], A))

    # Silla   : 10x + 6x  + 12x = 28x
    # Mesa    : 12y + 8y  + 12y = 22y
    # Comedor : 15z + 12z + 18z = 45z
    #           16    11    18
    time_optimization()