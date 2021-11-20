//#include "stdafx.h"
#include "iostream"

using namespace std;

double myfunc(float a, float b);//оголошення функції. Реалізація після main()


int main()
{
    float number1;//оголошення змінних,
    float number2;//зона видимості яких -
    double result;//вся функція main()
    cout << "Please enter a:\n";
    cin >> number1;
    cout << "\nPlease enter b:\n";
    cin >> number2;
    //Виклик функції із параметрами у вигляді змінних
    result = (myfunc(number1, number2) - sqrt(fabs(pow(number1,2) + pow(number2,2)))) / (number1 - myfunc(number2*2, number1*0.5)); //виклик функції. Результат буде занесено у //змінну result
    cout << "result = "<< result;

    return 0;
}
//Реалізація функції sum
double myfunc(float a, float b)
{
    double c;//Це локальна змінна функції sum
    //Зона її видимості лише ця функція
    c = pow(sin(a),3) * pow(cos(b),2);
    return c;//повернення результату
}