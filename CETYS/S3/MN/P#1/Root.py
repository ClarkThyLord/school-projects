def root(x, iterations = 2):
    root = 1.0
    for i in range(1, 3): root = (1.0 / 2.0) * ((root + x) / root)
    return root

x = 2
print(root(x), ' : ', x**(1.0/2.0))
