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


if __name__== "__main__":
    print('1-)')
    points = [
        (1.7, 3.7),
        (1.6, 3.9),
        (2.8, 6.7),
        (5.6, 9.5),
        (1.3, 3.4),
        (2.2, 5.6),
        (1.3, 3.7),
        (1.1, 2.7),
        (3.2, 5.5),
        (1.5, 2.9),
        (5.2, 10.7),
        (4.6, 7.6),
        (5.8, 11.8),
        (3.0, 4.1)
    ]
    m, b = least_squares_regression(points)
    print('y =', m, '* x', '+', b)
    for x in range(0, 14, 1):
        print('Point : ', points[x], ' ~ LSR : ', least_squares_regression_func(m, b, x))
    
    print('2-)')
    points = [
        (0, 1.037),
        (0.108, 1.402),
        (0.215, 1.638),
        (0.322, 1.774),
        (0.430, 1.803),
        (0.537, 1.715),
        (0.645, 1.509),
        (0.752, 1.214),
        (0.860, 0.831)
    ]
    m, b = least_squares_regression(points)
    print('y =', m, '* x', '+', b)
    for x in range(0, 9, 1):
        print('Point : ', points[x], ' ~ LSR : ', least_squares_regression_func(m, b, x))
    
    print('3-)')
    points = [
        (0.4, 805),
        (0.8, 975),
        (1.2, 1500),
        (1.6, 1950),
        (2, 2850),
        (2.5, 3500)
    ]
    m, b = least_squares_regression(points)
    print('y =', m, '* x', '+', b)
    for x in range(0, 6, 1):
        print('Point : ', points[x], ' ~ LSR : ', least_squares_regression_func(m, b, x))
    
    A = np.array([[6.0, 8.5], [8.5, 15.05]])
    b = np.array([[44.5776], [65.39048]])

    A_inv = np.linalg.inv(A)
    res = np.dot(A_inv, b)

    print(res)
    a = math.e ** res[0][0]
    b = res[1][0]

    print(a, b)
    print(a, 'e^(', b, 'x)')
    print(a, 'e^(', b, '3) = ', a * math.e ** (b * 3))
    # 2500 = 587.1496e ^ (0.7442260387811608 * x)
    # 2500 / 587.1496 = e ^ (0.7442260387811608 * x)
    # 4.2578 = e ^ (0.7442260387811608 * x)
    # ln(4.2578) = ln(e ^ (0.7442260387811608 * x))
    # ln(4.2578) = (0.7442260387811608 * x)
    # ln(4.2578) / 0.7442260387811608 = x
    print(math.log(4.2578) / 0.7442260387811608)
    
