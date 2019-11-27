import math

# h = (b - a) / n

def trapecio(f, a, b, n):
    x = ((f(a) + f(b)) / 2)

    for k in range(n - 1):
        x += f(a + (k * ((b - a) / n)))
    
    return ((b - a) / n) * x

def simpson_1_3(f, a, b, n):
    x = f(a) + f(b)
    
    for k in range(n - 1):
        step = a + k * ((b - a) / n)
        x += 2 * f(step) if k % 2 == 0 else 4 * f(step)
    
    return x * (((b - a) / n) / 3)

def simpson_3_8(f, a, b, n):
    x = f(a) + f(b)
    
    for k in range(n - 1):
        step = a + k * ((b - a) / n)
        x += 2 * f(step) if k % 3 == 0 else 3 * f(step)
    
    return x * ((3 * ((b - a) / n)) / 8)


if __name__ == '__main__':
    print('Actividad #1\n************')

    f = lambda x : x * math.log(x)
    a = 1
    b = 2
    n = 6
    print('1.a) xln(x) [1, 2] n=6 ->\ntrapecio:   ', trapecio(f,a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))

    f = lambda x : (x + (2 / x)) ** 2
    a = 1
    b = 2
    n = 12
    print('1.b) (x+1/x)^2 [1, 2] n=12 ->\ntrapecio:   ', trapecio(f,a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))

    f = lambda x : (x ** 2) / math.sqrt(4 - (x ** 2))
    a = -1
    b = math.sqrt(3)
    n = 18
    print('1.c) x^2 / sqrt(4 - x^2) [-1, sqrt(3)] n=18 ->\ntrapecio:   ', trapecio(f, a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))

    print('============')

    f = lambda x : 1 / math.sqrt(1 - (x ** 2))
    #f = lambda x : math.asin(x)
    a = -0.99
    b = 0.99
    n = 1000
    x = trapecio(f, a, b, n)
    print('2.a) 1 / sqrt(1 - x^2) (-1, 1) ->\ntrapecio:   ', x, '\nerror:', 3.141592653589793 - x)

    f = lambda x : math.cos(x) / (math.e ** x)
    a = 0
    b = (5 * math.pi) / 4
    n = 1000
    x = trapecio(f, a, b, n)
    print('2.b) cos(x) / e^x [5pi/4, e^x] ->\ntrapecio:   ', x, '\nerror:', 0.5 - x)

    print('============')

    f = lambda x : ((1/3) * x ** 3) + 1
    a = 0
    b = 2
    n = 1000
    x = simpson_1_3(f, a, b, n)
    print('3) y=1/3x^3 [0, 2] n=1000 ->\nsimpson 1/3: ', x, '\nerror:', 3.333333333333333 - x)
    
    print('============')

    f = lambda x : 4 * math.sqrt(49 - (x * x))
    a = 0
    b = 1.8525
    n = 1000
    x = simpson_1_3(f, a, b, n)
    print('4) 4 * sqrt(49 - x^2) [0, 1.8525] n=1000 ->\ntrapecio:   ', trapecio(f, a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))
