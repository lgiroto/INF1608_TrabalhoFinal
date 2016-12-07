#include <stdio.h>
#include <math.h>
#include "simpson.h"


double DoubleSimpson (double a, double b, double (*f) (double x), double *v)
{
	double Sab, Sac, Scb;
	double c = (b+a)/2;
	double fa = f(a);
	double fb = f(b);
	double fc = f(c);

	Sab  = ((b-a)/6)*(fa + 4*fc + fb);

	Sac  = ((c-a)/6)*(fa + 4*f((c+a)/2) + fc);

	Scb  = ((b-c)/6)*(fc + 4*f((c+b)/2) + fb);

	*v = Sac + Scb;
	return fabs(Sab - Sac - Scb)/15;
}

double AdaptiveSimpson (double a, double b, double (*f) (double x), double tol)
{
	double c;
	double v;
	double erro;

	erro = DoubleSimpson( a, b, f, &v);

	if ( erro <= tol)
	{
		return v;
	}
	else
	{
		c = (a+b)/2;
		return AdaptiveSimpson(a, c, f, tol) + AdaptiveSimpson(c, b, f, tol); 
	}
}