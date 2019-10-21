import math
import numpy as np


def converge(x): return math.floor(x) if x - int(x) < 0.5 else math.ceil(x)
def matrix_converge(m): return [converge(x) for x in m]


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


# https://www.mathsisfun.com/data/least-squares-regression.html
def least_squares_regression(points):
    sum_x = 0.
    sum_y = 0.
    sum_x2 = 0.
    sum_xy = 0.

    for point in points:
        sum_x += point[0]
        sum_y += point[1]
        sum_x2 += point[0] ** 2
        sum_xy += point[0] * point[1]

    m = ((len(points) * sum_xy) - (sum_x * sum_y)) / ((len(points) * sum_x2) - (sum_x ** 2))

    b = (sum_y - (m * sum_x)) / len(points)

    return m, b

def least_squares_regression_func(m, b, x):
    return m * x + b


if __name__ == "__main__":
    print('1-)\na.1)\nJacobi')
    A = [
            [4., -1., 1., 0.],
            [4., -8, 1., 2.],
            [-2., 1., 5., -1],
            [1., -4., 3., 10.]
        ]
    b = [7., -23., 16., -15.]
    
    result, error = jacobi(A, b, 15)
    print('Resultado:', result, '\nb.1) Converge:', matrix_converge(result), '\nError:', error)

    print('\na.2)\nGauss Seidel')
    result, error = gauss_seidel(A, b, 15)
    print('Resultado:', result, '\nb.2) Converge:', matrix_converge(result), '\nError:', error)

    print('\nc)\nComparación: Al comparar los métodos de Jacobi y Gauss Seidel, encontré que el método de Gauss Seidel es más preciso y exacto en menos iteraciones que el método de Gauss, hasta el punto de que Jacobi tendría que hacer de 2 a 4 veces las iteraciones que el de Gauss Seidel Método.')
    print('\nd)\nConclusión: Ambos métodos son simples pero elegantes y fáciles de implementar, mucho más, realizan muy pocas operaciones en comparación con otros métodos, pero proporcionan resultados tan precisos y exactos.')
