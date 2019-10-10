import math


def main():
    while True:
        print(
            '***\nActividad #2\n',
            '1    - bisection : 1 / x; a = -2, b = 1\n',
            '2    - Newtow    : ln(2)\n',
            'exit - exit the program'
        )
        ans = input()
        if ans == '1': print(bisection(func_1, -2, 1, 100))
        if ans == '2': print(bisection(func_2, func_2d, 0.5, 100))
        elif ans == 'exit': return
        else: print('invalid input')


def func_1(x): return 1 / x

def func_2(x): return math.log(2)
def func_2d(x): return 0


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


if __name__== "__main__": main()
