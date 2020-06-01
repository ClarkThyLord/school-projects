
#include <stdio.h>
#include <cstdint>
#include <string>
#include <iostream>
#include <math.h>
using namespace std;

typedef uint64_t uint64;


inline uint64 regla_horner(std::string cadena) {
    uint64 x = 5;
    uint64 llave = cadena[0];
    for (int i = 1; i < cadena.length(); i++)
        llave = llave * x + cadena[i];
    return llave;
}


inline uint64 criterio_division(uint64 llave) {
    return llave % 997;
}

inline uint64 criterio_multiplicacion(uint64 llave) {
    int m = 8;
    float a = 13.0 / 32.0;
    // llave = 21;
    return floor(m * (llave * a - floor(llave * a)));
    // return (m / pow(2, sizeof(uint64))) * ((uint64)(a * llave) % (uint64)pow(2, sizeof(uint64)));
    // return ((13/32) * 21 % 5) / (5.0 / 8);
    // return ((991 * llave) % 997) / (997.0 / 1000.0);
    // return (991 * llave % 1000) / (1000 / 997);
    // return (3 * llave % sizeof(uint64_t)) / (sizeof(uint64_t) / 7);
    // return (991 * llave % sizeof(uint64_t)) / (sizeof(uint64_t) / 997);
    // return 997 * (llave * std::pow(2, sizeof(uint64_t)) (sizeof(uint64_t) / )) % 1);
    // return 997 * ((uint64_t)(llave * (std::pow(2, sizeof(uint64_t)))) % 1);
    // return 997 * ((uint64_t)(llave * 0.6180339887) % 1);
}

inline uint64 criterio_fibonacci(uint64 llave) {
    int m = 8;
    float a = pow(2, sizeof(uint64_t)) / ((1 + sqrt(5)) / 2);
    // llave = 21;
    return floor(m * (llave * a - floor(llave * a)));
}


int main()
{
    string cadena;
    cout << "Introduce una cadena : ";
    cin >> cadena;
    uint64 horner = regla_horner(cadena);
    cout << "\nDivision hash         :   " << criterio_division(horner) << "\n";
    cout << "\nMultiplicacion hash   :   " << criterio_multiplicacion(horner) << "\n";
    cout << "\nFibonacci hash        :   " << criterio_fibonacci(horner) << "\n";

    return 0;
}
