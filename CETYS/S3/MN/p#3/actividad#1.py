import math


def trapecio(f, a, b, n):
    return

def simpson_1_3(f, a, b, n):
    return 

def simpson_3_8(f, a, b, n):
    return 


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
    print('1.b) (x+1/x)^2 [1, 2] n=6 ->\ntrapecio:   ', trapecio(f,a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))

    f = lambda x : (x ** 2) / math.sqrt(4 - (x ** 2))
    a = -1
    b = math.sqrt(3)
    n = 18
    print('1.c) x^2 / sqrt(4 - x^2) [1, 2] n=6 ->\ntrapecio:   ', trapecio(f,a, b, n), '\nsimpson 1/3:', simpson_1_3(f, a, b, n),'\nsimpson 3/8:', simpson_3_8(f, a, b, n))

    print('============')

    
