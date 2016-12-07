#include <stdio.h>
#include <math.h>

double derivada (double (*f) (double x), double h, double x)
{
	double diff = f(x+h) - f(x-h);
	return diff/(2*h);
}

double simpson (double (*f) (double x), double a, double b, int n)
{
	double h = (b-a)/n;
	double x = a;
	double soma = 0;

	while ( x < b )
	{
		soma += (h/6)*(f(x) + 4*f(x+(h/2)) + f(x+h));
		x += h;
	}

	return soma;
}

double pontomedio (double (*f) (double x), double a, double b, int n)
{
	double h = (b-a)/n;
	double x = a;
	double soma = 0;

	while (x < b)
	{
		soma += h*f((x + x + h)/2);
		x += h;
	}

	return soma;
}