import math

def main():
    while True:
        print(
            '***\nActividad #2\n',
            '1 - Taylor                         : e^x * ln(1 + x)\n',
            '2 - Taylor                         : sen(π /4)\n',
            '3 - Bisection, Newton, Fixed Point : sqrt(12, 3)\n',
            '4 - Bisection, Newton              : x - tan(x)\n',
            '5 - Bisection, Newton              : x^x - 2\n',
            'exit - exit the program'
        )
        ans = input()
        if ans == '1': print(func_1(0.5, 5))
        elif ans == '2': print(func_2(math.pi / 4, 6))
        elif ans == '3': print(bisection(func_3, 0, 3, 100), newton(func_3, func_3d, 100), fixed_point(func_3c, 1, 100))
        elif ans == '4': print(bisection(func_4, 4, 5, 100), fixed_point(func_4c, 100, 100))
        elif ans == '5': print(bisection(func_5, 1, 2, 100), newton(func_5, func_5d, 1, 100))
        elif ans == 'exit': return
        else: print('invalid input')


def sign(x): return 1 if x >= 0 else 0


def factorial(x):
    return 1 if x <= 1 else x * factorial(x - 1)

def factorial_2(x):
    return 1 if x <= 1 else x * factorial_2(x - 2)

def Pi(iterations):
    pi = 0
    print('PI :', iterations)
    for i in range(iterations):
        pi += factorial(i) / factorial_2(2 * i + 1)
        print(pi)
    print('---')
    return 2 * pi

def e(x, iterations):
    E = 0
    print('e :', iterations)
    for i in range(iterations):
        E += math.pow(x, i) / factorial(i)
        print(E)
    print('---')
    return E

def ln(x, iterations):
    Ln = 0
    print('ln :', iterations)
    for i in range(1, iterations + 1):
        Ln += math.pow(-1, i - 1) * (math.pow(x - 1, i) / (i))
        print(Ln)
    print('---')
    return Ln

def cos(x, iterations):
    Cos = 0
    print('Cos :', iterations)
    for i in range(iterations):
        Cos += (math.pow(-1, i) / factorial(2 * i)) * math.pow(x, 2 * i)
        print(Cos)
    print('---')
    return Cos

def sen(x, iterations):
    Sen = 0
    print('Sen :', iterations)
    for i in range(iterations):
        Sen += (math.pow(-1, i) / factorial(2 * i + 1)) * math.pow(x, 2 * i + 1)
        print(Sen)
    print('---')
    return Sen


def func_1(x, iterations):
    return e(x, iterations) * ln(x + 1, iterations)

def func_2(x, iterations):
    return 1 / cos(x, iterations)

def func_3(x): return x ** 3 - 12
def func_3d(x): return 3 * x ** 2
def func_3c(x): return math.sqrt(12 / x)

def func_4(x): return x - math.tan(x)
def func_4d(x): return 1 - (1 / math.pow(math.cos(x), 2))
def func_4c(x): return math.tan(x)

def func_5(x): return math.pow(x, x) - 2
def func_5d(x): return math.pow(x, x) * (math.log(x) + 1)


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
        print(x1)
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
