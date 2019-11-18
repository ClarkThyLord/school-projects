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
    print('1.c) x^2 / sqrt(4 - x^2) [-1, sqrt(3)] n=18 ->\ntrapecio:   ', trapecio(f,a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))

    print('============')
