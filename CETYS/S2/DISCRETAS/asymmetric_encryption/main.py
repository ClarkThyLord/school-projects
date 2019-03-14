# THERE IS A LIMIT TO HOW LONG THE MSG CAN BE GIVEN THESE PRIME NUMBERS

# two random prime numbers
p1 = 53
p2 = 59

n = p1 * p2

# PHI
phi = (p1 - 1) * (p2 - 1)

# doesn't share a common factor with `pi`
e = 3

# private key
prk = int((2 * phi + 1) / 3)

# public key
pbk = (n, e)

def str_to_ascll(_str):
    return int(''.join(str(ord(c)) for c in _str))

def encrypt(msg, pbk):
    return msg ** pbk[1] % pbk[0]

def decrypt(msg, prk, pbk):
    return msg ** prk % pbk[0]

if __name__ == "__main__":
    print(encrypt(89, pbk))
    print(decrypt(encrypt(89, pbk), prk, pbk))
    
    msg = str_to_ascll(input())
    print(msg)
    print(encrypt(msg, pbk))
    print(decrypt(encrypt(msg, pbk), prk, pbk))
