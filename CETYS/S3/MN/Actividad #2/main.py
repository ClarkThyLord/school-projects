import math


def main():
    while True:
        print(
            '***\nActividad #2\n',
            '1    - Newton     : root(x^3 - 3 * x^2 + 1)\n',
            '2    - Newton     : root(21, 3)\n',
            '3    - Punto Fijo : root(21, 3)\n',
            '4    - Bisection  : sen(x) - x^2\n',
            '5    - Newton     : sen(x) - x^2\n',
            '6    - Punto Fijo : sen(x) - x^2\n',
            '7    - Newton : sec(x) -> π',
            'exit - exit the program'
        )
        ans = input()
        if ans == '1': print(bisection(func_1, 0, 1, 100))
        elif ans == '2': print(newton(func_2, func_2d, 0.0001, 100))
        elif ans == '3': print(fixed_point(func_2c, 1))
        elif ans == '4': print(bisection(func_3, 0, 1, 100))
        elif ans == '5': print(newton(func_3, func_3d, 100))
        elif ans == '6': print(fixed_point(func_3c))
        elif ans == '7': print(bisection(func_4, 0, 4))
        elif ans == 'exit': return
        else: print('invalid input')


def func_1(x): return math.pow(x, 3) - 3 * math.pow(x, 2) + 1

def func_2(x): return x ** 3 - 21
def func_2d(x): return 3 * x ** 2
def func_2c(x): return math.sqrt(21 / x)

def func_3(x): return math.sin(x) - math.pow(x, 2)
def func_3d(x): return math.cos(x) - 2 * x
def func_3c(x): return math.sin(x) / x

def func_4(x): return math.sin(x)


def sign(x): return 1 if x >= 0 else 0

def bisection(f, a, b, iterations=100):
    c = 0
    for i in range(iterations):
        c = (a + b) / 2
        if f(c) == 0: return c
        if sign(f(c)) == sign(f(a)): a = c
        else: b = c
    return c

def newton(f, fd, x0, iterations=100):
    x1 = 0
    for i in range(iterations):
        y = f(x0)
        yd = fd(x0)

        x1 = x0 - (y / yd)

        x0 = x1
    return x1

def fixed_point(fc, x=0.5, iterations=100):
    x1 = x
    for i in range(iterations):
        x1 = fc(x1)
    return x1


if __name__== "__main__": main()
