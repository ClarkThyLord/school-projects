import math

def main():
    while True:
        print(
            '***\nProject #1\n',
            '1 - Taylor : ln(1 + x / 1 - x)\n',
            '2 - Taylor : ln(1.5)\n',
            '3 - 4[arctan(1/2)+ arctan(1/3)]\n',
            '4 - Newton : 1/2 * x ^ 3 + 3 * x ^ 2 - 60\n',
            '5 - Newton : 4x^3 - 120x^2 + 800x - 1500',
            'exit - exit the program'
        )
        ans = input()
        if ans == '1': print(ln(0, 100))
        elif ans == '2': print(ln(0.2, 100))
        elif ans == '3': print(4 * (arctan(1/2, 100) + arctan(1/3, 100)))
        elif ans == '4': print(newton(func_1, func_1d, 1, 100))
        elif ans == '5': print(newton(func_2, func_2d, 1, 100))
        elif ans == 'exit': return
        else: print('invalid input')


def sign(x): return 1 if x >= 0 else 0


def ln(x, iterations):
    Ln = 0
    print('ln :', x, iterations)
    for i in range(iterations):
        Ln += (math.pow(x, (2 * i) + 1)) / ((2 * i) + 1)
        print(i + 1, '.', Ln * 2)
    print('---')
    return 2 * Ln

def arctan(x, iterations):
    Arctan = 0
    print('Arctan :', x, iterations)
    for i in range(iterations):
        Arctan += math.pow(-1, i) * ((math.pow(x, 2 * i + 1)) / (2 * i + 1))
        print(i + 1, '.', Arctan)
    print('---')
    return Arctan


def func_1(x): return ((1 / 2) * math.pow(x, 3)) + (3 * math.pow(x, 2)) - 60
def func_1d(x): return (3 / 2) * (x * (x + 4))

def func_2(x): return (4 * math.pow(x, 3)) - (120 * math.pow(x, 2)) + (800 * x) - 1500
def func_2d(x): return 4 * (200 - 60 * x + 3 * math.pow(x, 2))

def bisection(f, a, b, iterations=100):
    print('Bisection :', f.__name__, a, b, iterations)
    c = 0
    for i in range(iterations):
        c = (a + b) / 2
        if f(c) == 0: return c
        if sign(f(c)) == sign(f(a)): a = c
        else: b = c
        print(c)
    print('===')
    return c

def newton(f, fd, x0, iterations=100):
    print('Newton :', f.__name__, fd.__name__, x0, iterations)
    x1 = 0
    for i in range(iterations):
        y = f(x0)
        yd = fd(x0)

        x1 = x0 - (y / yd)

        x0 = x1
        print(i + 1, '.', x1)
    print('===')
    return x1

def fixed_point(fc, x=0.5, iterations=100):
    print('Punto Fijo :', fc.__name__, x, iterations)
    x1 = x
    for i in range(iterations):
        x1 = fc(x1)
        print(x1)
    print('===')
    return x1


if __name__== "__main__": main()
