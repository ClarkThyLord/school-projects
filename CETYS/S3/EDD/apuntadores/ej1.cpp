#include <iostream>
using namespace std;

void	str_inverse(char *str)
{
	while(*str)
	{
		cout << *str;
		str++;
	}
}

int		main() 
{
	char str[] = "Hello world!";
	str_inverse(&str[0]);
	return 0;
}

