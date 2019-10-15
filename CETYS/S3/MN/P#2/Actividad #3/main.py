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

def vector_transformation(vector, A, iterations=10):
    t1 = np.array(vector)
    t2 = np.array(vector)
    A = np.array(A)

    print('Origianl Vector : ', t1)
    for i in range(iterations):
        t1 = np.linalg.matrix_power(A, 2).dot(t1)
        print('Step : ', 1 + i, ' - ', t1, ' ~ ', math.sqrt(t1[0] ** 2 + t1[1] ** 2))

    print('Origianl Vector : ', t2)
    for i in range(iterations):
        t2 = np.linalg.inv(A).dot(t2)
        print('Step : ', 1 + i, ' - ', t2, ' ~ ', math.sqrt(t2[0] ** 2 + t2[1] ** 2))
    
    return t1.tolist(), t2.tolist()

def jacobi(A, b, max_iterations = 15):
    A = np.array(A)
    b = np.array(b)
    x = np.zeros_like(b)
    for it_count in range(max_iterations):
        x_new = np.zeros_like(x)

        for i in range(A.shape[0]):
            s1 = np.dot(A[i, :i], x[:i])
            s2 = np.dot(A[i, i + 1:], x[i + 1:])
            x_new[i] = (b[i] - s1 - s2) / A[i, i]

        if np.allclose(x, x_new, atol=1e-10, rtol=0.):
            break

        x = x_new
        print('Step : ', 1 + it_count, ' - ', x)
    return x, np.dot(A, x) - b


if __name__== "__main__":
    print('1-)')
    A = [[1, 2, 3], [1, 1, 2], [0, 1, 2]]
    #print(code('citaelmartes', A))
    #print(decode(code('citaelmartes', A), A))
    print(code('enviadolares', A))
    print(decode([85, 58, 39, 70, 45, 30, 73,51, 37, 91, 57, 53], A))

    print('\n\n2-)')
    ang = math.pi / 6
    A = [[math.cos(ang), -math.sin(ang)], [math.sin(ang), math.cos(ang)]]
    print(vector_transformation([1, 1], A))
    #print(vector_transformation([2.5, 2.5], A))

    print('\n\n3-)')
    # Silla   : 10x + 6x  + 12x
    # Mesa    : 12y + 8y  + 12y
    # Comedor : 15z + 12z + 18z
    #           16    11    18
    A = [[10., 12., 15.], [6., 8., 12.], [12., 12., 18.]]
    b = [16. * 60., 11. * 60., 18. * 60.]
    #print(jacobi(A, b, 15)[0])
    print(np.linalg.solve(A,b))
