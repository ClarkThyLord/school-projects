import math

def main():
    while True:
        print(
            '***\nActividad #2\n',
            '1 - Bisection : 1 / x\n',
            '2 - Bisection : (20 - y) * y + (20 - y) * sqrt(y) + sqrt(20 - y) * y + sqrt(20 - y) * sqrt(y)\n',
            '3 - Newton    : ln(2)',
            'exit - exit the program'
        )
        ans = input()
        if ans == '1': print(bisection(func_1, -2, 1, 100))
        elif ans == '2': print(bisection(func_2, -10, 10, 100))
        elif ans == '3': print(newton(func_3, func_3d, 0.1, 100))
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


def func_1(x): return 1 / x

def func_2(y): return (20 - y) * y + (20 - y) * math.sqrt(y) + math.sqrt(20 - y) * y + math.sqrt(20 - y) * math.sqrt(y)

def func_3(x): return x ** 2 - math.log(2) ** 2
def func_3d(x): return 2 * x - ((2 * math.log(x)) / x)


if __name__== "__main__": main()
