import numpy as np

# https://en.wikipedia.org/wiki/Jacobi_method
def jacobi(A, b, max_iterations = 1000):
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
        print('Step : ', 1 + it_count, ' ~ ', x)
    return x, np.dot(A, x) - b

# https://en.wikipedia.org/wiki/Gauss%E2%80%93Seidel_method
def gauss_seidel(A, b, max_iterations = 1000):
    A = np.array(A)
    b = np.array(b)
    x = np.zeros_like(b)
    for it_count in range(1, max_iterations):
        x_new = np.zeros_like(x)
        for i in range(A.shape[0]):
            s1 = np.dot(A[i, :i], x_new[:i])
            s2 = np.dot(A[i, i + 1:], x[i + 1:])
            x_new[i] = (b[i] - s1 - s2) / A[i, i]
        if np.allclose(x, x_new, rtol=1e-8):
            break
        x = x_new
        print('Step : ', 1 + it_count, ' ~ ', x)
    return x, np.dot(A, x) - b

# https://en.wikipedia.org/wiki/LU_decomposition
def LU(A, b):
    A = np.array(A)
    b = np.array(b)
    L = np.tril(A)
    U = np.triu(A)
    
    L[0][0] = 1
    L[1][1] = 1
    L[2][2] = 1

    L[1][0] = A[1][0] / U[0][0]
    U[1][1] = A[1][1] - (L[1][0] * U[0][1])
    U[1][2] = A[1][2] - (L[1][0] * U[0][2])
    L[2][0] = A[2][0] / U[0][0]
    L[2][1] = (A[2][1] - L[2][0] * U[0][1]) / U[1][1]
    U[2][2] = A[2][2] - (L[2][0]*U[0][2] + L[2][1]*U[1][2])

    x = b[0] / L[0][0]
    y = (b[1] - L[1][0] * x) / (L[1][1])
    z = (b[2] - L[2][0] * x - L[2][1] * y) / L[2][2]

    z = (z / U[2][2])
    y = (y - U[1][2] * z) / U[1][1]
    x = (x - U[0][1] * y - U[0][2] * z) / U[0][0]

    return (x, y, z)


if __name__ == "__main__":
    A = [
            [10., -1., 2., 0.],
            [-1., 11., -1., 3.],
            [2., -1., 10., -1.],
            [0., 3., -1., 8.]
        ]
    b = [6., 25., -11., 15.]
    
    result, error = jacobi(A, b)
    print('Jacobi ~ Result: ', result, 'Error: ', error)
    
    result, error = gauss_seidel(A, b)
    print('Gauss Seidel ~ Result: ', result, 'Error: ', error)

    A = [
            [10., -1., 2.],
            [-1., 11., -1.],
            [2., -1., 10.],
            [0., 3., -1.]
        ]
    b = [6., 25., -11.]
    result = LU(A, b)
    print('LU : ', result)
