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

import math
import numpy as np


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
    print('1-)')
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

    print('3-)')
    points = [
        (0., 3.49),
        (0., 3.05),
        (2., 3.24),
        (3., 2.82),
        (3., 3.19),
        (5., 2.78),
        (8., 2.31),
        (8., 2.54),
        (10., 2.03),
        (12., 2.51)
    ]
    m, b = least_squares_regression(points)
    print('y =', m, '* x', '+', b)
    for x in range(0, len(points), 1):
        print('Point : ', points[x], ' ~ LSR : ', least_squares_regression_func(m, b, x))