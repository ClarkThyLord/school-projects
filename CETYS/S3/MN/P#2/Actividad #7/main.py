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
        (686., 1600.),
        (770., 1665.),
        (817., 1750.),
        (800., 1685.),
        (809., 1700.),
        (901., 1770.),
        (803., 1725.),
    ]
    m, b = least_squares_regression(points)
    print('y =', m, '* x', '+', b)
    for x in range(0, len(points), 1):
        print('Point : ', points[x], ' ~ LSR : ', least_squares_regression_func(m, b, x))
    
