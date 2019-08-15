#include <iostream>

void	str_inverse(char *str)
{
	while(*str)
	{
		std::cout << *str;
		str++;
	}
}

int		main() 
{
	char str[] = "Hello world!";
	str_inverse(&str[0]);
	return 0;
}

