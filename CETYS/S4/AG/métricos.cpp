#include <map>
#include <cmath>
#include <string>
#include <vector>
#include <cstdint>
#include <fstream>
#include <iostream>

inline uint64_t regla_horner(std::string cadena) {
	uint64_t x = 5;
	uint64_t llave = cadena[0];
	for (int i = 1; i < cadena.length(); i++)
		llave = llave * x + cadena[i];
	return llave;
}


inline uint64_t criterio_division(uint64_t llave) {
	return llave % 131101;
}


inline uint64_t criterio_multiplicacion(uint64_t llave) {
	int m = 131101;
	float a = 131099 / 131102;
	return floor(m * (llave * a - floor(llave * a)));
}

inline uint64_t criterio_fibonacci(uint64_t llave) {
	int m = 131101;
	float a = pow(2, sizeof(uint64_t)) / ((1 + sqrt(5)) / 2);
	return floor(m * (llave * a - floor(llave * a)));
}

int contar_bits(uint64_t llave)
{ 
	int cuenta = 0;
	while (llave){
		cuenta += llave & 1; 
		llave >>= 1; } 
	return cuenta; 
}

float_t avalancha(uint64_t llave, uint64_t hash)
{
	uint64_t diff = llave ^ hash;
	return contar_bits(diff) / 32.0;
}


int main()
{
	std::vector<uint64_t> llaves;
	std::ifstream archivo("diccionario.csv");
	if (archivo.is_open()) {
		std::string linea;
		while(std::getline(archivo, linea))
			llaves.push_back(regla_horner(linea)); }
	float_t size = (llaves.size());
	// numero primo 131101
	float_t* frecuencia_division = new float_t[131102];
	float_t* frecuencia_multiplicacion = new float_t[131102];
	float_t* frecuencia_fibonacci = new float_t[131102];
	for (int i = 0; i < size; i++) {
		uint64_t llave = llaves[i];
		frecuencia_division[criterio_division(llave)] += 1.0;
		frecuencia_multiplicacion[criterio_multiplicacion(llave)] += 1.0;
		frecuencia_fibonacci[criterio_fibonacci(llave)] += 1.0; }
	float_t bottom = (size / (2 * 131102)) * (size + (2 * 131102) - 1);
	float_t top_division=0.0;
	float_t top_multiplicacion=0.0;
	float_t top_fibonacci=0.0;
	for (int i = 0;  i <  131102 - 1; i++) {
		top_division += frecuencia_division[i] * (frecuencia_division[i] + 1) / 2;
		top_multiplicacion += frecuencia_multiplicacion[i] * (frecuencia_multiplicacion[i] + 1) / 2;
		top_fibonacci += frecuencia_fibonacci[i] * (frecuencia_fibonacci[i] + 1) / 2; }
	std::cout << "Distribucion Division: " << top_division / bottom << std::endl;
	std::cout << "Distribucion Multiplicacion: " << top_multiplicacion / bottom << std::endl;
	std::cout << "Distribucion Fibonacci: " << top_fibonacci / bottom << std::endl;
	float_t avalancha_division=0.0;
	float_t avalancha_multiplicacion=0.0;
	float_t avalancha_fibonacci=0.0;
	for (int i = 0; i < size; i++) {
		uint64_t llave = llaves[i];
		avalancha_division += avalancha(llave, criterio_division(llave));
		avalancha_multiplicacion += avalancha(llave, criterio_multiplicacion(llave));
		avalancha_fibonacci += avalancha(llave, criterio_fibonacci(llave)); }
	std::cout << "Avalancha Division: " << avalancha_division / size << std::endl;
	std::cout << "Avalancha Multiplicacion: " << avalancha_multiplicacion / size << std::endl;
	std::cout << "Avalancha Fibonacci: " << avalancha_fibonacci / size << std::endl;
	return 0;
}
