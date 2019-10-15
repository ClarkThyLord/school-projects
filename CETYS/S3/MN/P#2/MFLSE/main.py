import numpy

# https://en.wikipedia.org/wiki/Jacobi_method
def jacobi(A, b, max_iterations = 1000):
    x = numpy.zeros_like(b)
    for it_count in range(max_iterations):
        x_new = numpy.zeros_like(x)

        for i in range(A.shape[0]):
            s1 = numpy.dot(A[i, :i], x[:i])
            s2 = numpy.dot(A[i, i + 1:], x[i + 1:])
            x_new[i] = (b[i] - s1 - s2) / A[i, i]

        if numpy.allclose(x, x_new, atol=1e-10, rtol=0.):
            break

        x = x_new
    return x, numpy.dot(A, x) - b

# https://en.wikipedia.org/wiki/Gauss%E2%80%93Seidel_method
def gauss_seidel(A, b, max_iterations = 1000):
    x = numpy.zeros_like(b)
    for it_count in range(1, max_iterations):
        x_new = numpy.zeros_like(x)
        for i in range(A.shape[0]):
            s1 = numpy.dot(A[i, :i], x_new[:i])
            s2 = numpy.dot(A[i, i + 1:], x[i + 1:])
            x_new[i] = (b[i] - s1 - s2) / A[i, i]
        if numpy.allclose(x, x_new, rtol=1e-8):
            break
        x = x_new
    return x, numpy.dot(A, x) - b

if __name__ == "__main__":
    A = numpy.array([[10., -1., 2., 0.],
              [-1., 11., -1., 3.],
              [2., -1., 10., -1.],
              [0., 3., -1., 8.]])
    b = numpy.array([6., 25., -11., 15.])
    
    result, error = jacobi(A, b)
    print('Jacobi ~ Result: ', result, 'Error: ', error)
    
    result, error = gauss_seidel(A, b)
    print('Gauss Seidel ~ Result: ', result, 'Error: ', error)

def LU(A, b):
    pass
