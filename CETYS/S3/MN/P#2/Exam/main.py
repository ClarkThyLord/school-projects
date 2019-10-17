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


if __name__ == "__main__":
    A = [
            [4., -1., 1., 3.],
            [-1., 5., 1., 1.],
            [2., 0., 4., 2.],
            [0., 1., -1., 7.]
        ]
    b = [14., -1., 19., 10.]
    
    result, error = jacobi(A, b)
    print('Jacobi ~ Result: ', result, 'Error: ', error)
    
    result, error = gauss_seidel(A, b)
    print('Gauss Seidel ~ Result: ', result, 'Error: ', error)

