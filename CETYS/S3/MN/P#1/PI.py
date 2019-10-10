def factorial(x):
    return 1 if x <= 1 else x * factorial(x - 1)

def factorial_2(x):
    return 1 if x <= 1 else x * factorial_2(x - 2)

def Pi(iterations):
    pi = 0
    for i in range(iterations):
        pi += factorial(i) / factorial_2(2 * i + 1)
    return 2 * pi

print(Pi(500))
