import math
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
    return x.tolist(), np.dot(A, x) - b

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
    return x.tolist(), np.dot(A, x) - b


if __name__== "__main__":
    print('1-)')
    # 3x1 + 1x2 + 1x3
    # 3x1 + 6x2 + 2x3
    # 3x1 + 3x2 + 7x3
    # 1     0     4
    A = [[3., 3., 3], [1., 6., 3.], [1., 2., 7.]]
    b = [1., 0., 4.]
    resultj, errorj = jacobi(A, b, 15)
    print('Jacobi : ', resultj)
    resultg, errorg = gauss_seidel(A, b, 15)
    print('Gauss Seidel : ', resultj)
    print('Exact : ', np.linalg.solve(A,b))
    print('Jacobi Error : ', np.linalg.solve(A,b) - resultj, ' Gauss Seidel Error : ', np.linalg.solve(A,b) - resultg)
    print('Conclusion : Al ejecutar esto desde mi escritorio, obtengo un resultado más preciso al usar el método Jacobi, el método Gauss Seidel requiere más iteraciones para obtener un resultado exacto')

    print('\n\n2-)')
