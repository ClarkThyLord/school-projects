import math

def main():
    while True:
        print('***\nActividad #1\n1 - PI\n2 - e\n3 - e^2\n4 - ln(2)\n5 - cos(π/6)\n6 - sen(π/3)\n7 - exit')
        ans = input()
        if ans == '1': print(Pi(500))
        elif ans == '2': print(e(1, 50))
        elif ans == '3': print(e(2, 50))
        elif ans == '4': print(ln(2, 1000))
        elif ans == '5': print(cos(math.pi / 6, 10))
        elif ans == '6': print(sen(math.pi / 3, 10))
        elif ans == '7': return
        else: print('invalid input')

def factorial(x):
    return 1 if x <= 1 else x * factorial(x - 1)

def factorial_2(x):
    return 1 if x <= 1 else x * factorial_2(x - 2)

def Pi(iterations):
    pi = 0
    for i in range(iterations): pi += factorial(i) / factorial_2(2 * i + 1)
    return 2 * pi

def e(x, iterations):
    e = 0
    for i in range(iterations): e += math.pow(x, i) / factorial(i)
    return e

def ln(x, iterations):
    Ln = 0
    for i in range(1, iterations + 1): Ln += math.pow(-1, i - 1) * (math.pow(x - 1, i) / (i))
    return Ln

def cos(x, iterations):
    Cos = 0
    for i in range(iterations): Cos += (math.pow(-1, i) / factorial(2 * i)) * math.pow(x, 2 * i)
    return Cos

def sen(x, iterations):
    Sen = 0
    for i in range(iterations): Sen += (math.pow(-1, i) / factorial(2 * i + 1)) * math.pow(x, 2 * i + 1)
    return Sen

if __name__== "__main__": main()
