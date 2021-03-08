using System;
using System.Collections.Generic;

namespace ConsoleCalc
{
    class Program
    {
        static string PostfixOut = "";
        static Stack<char> Operators = new Stack<char>();

        static void Main(string[] args)
        {
            Console.WriteLine("Enter expression: ");
            string input = Console.ReadLine();
            // to postfix notation
            ToPostfix(input);            
            Console.WriteLine(PostfixOut);
            // get result
            var result = GetResult();            
            Console.WriteLine(result);
            //
            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);            
        }        

        private static void ToPostfix(string input)
        {
            foreach (char ch in input)
            {
                if (ch == ' ')
                    continue;
                if (IsOperator(ch))
                {
                    if (ch != '(' && ch != ')')
                    {
                        if (Operators.Count > 0 && GetPriority(Operators.Peek()) >= GetPriority(ch))
                        {
                            PostfixOut += Operators.Pop();                            
                        }
                        Operators.Push(ch);
                    }
                    if (ch == '(' )
                    {                        
                        Operators.Push(ch);
                    }
                    if (ch == ')')
                    {
                        PostfixOut += Operators.Pop();
                        Operators.Pop();
                    }
                }
                else
                {
                    PostfixOut += ch;
                }
            }
            AddOperatorsToPostfixOut();
        }

        private static bool IsOperator(char ch)
        {
            if (
                ch == '(' || ch == ')' ||
                ch == '+' || ch == '-' ||
                ch == '*' || ch == '/' ||
                ch == '^'
            )
                return true;
            else
                return false;
        }

        private static int GetPriority(char op)
        {
            if (op == '(' || op == ')')
                return 0;
            if (op == '+' || op == '-')
                return 1;
            if (op == '*' || op == '/')
                return 2;            
            if (op == '^')
                return 3;
            return 4;
        }

        private static void AddOperatorsToPostfixOut()
        {
            while(Operators.Count > 0)
            {
                PostfixOut += Operators.Pop();
            }
        }

        public static decimal GetResult()
        {
            Stack<string> stack = new Stack<string>();
            Queue<char> queue = new Queue<char>(PostfixOut.ToCharArray());
            char str = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!IsOperator(str))
                {
                    stack.Push(str.ToString());
                    str = queue.Dequeue();
                }
                else
                {
                    decimal summ = 0;
                    try
                    {   
                        if (str == '+')
                        {
                            decimal a = Convert.ToDecimal(stack.Pop());
                            decimal b = Convert.ToDecimal(stack.Pop());
                            summ = a + b;
                        }
                        if (str == '-')
                        {
                            decimal a = Convert.ToDecimal(stack.Pop());
                            decimal b = Convert.ToDecimal(stack.Pop());
                            summ = b - a;
                        }
                        if (str == '*')
                        {
                            decimal a = Convert.ToDecimal(stack.Pop());
                            decimal b = Convert.ToDecimal(stack.Pop());
                            summ = b * a;
                        }
                        if (str == '/')
                        {
                            decimal a = Convert.ToDecimal(stack.Pop());
                            decimal b = Convert.ToDecimal(stack.Pop());
                            summ = b / a;
                        }
                        if (str == '^')
                        {
                            decimal a = Convert.ToDecimal(stack.Pop());
                            decimal b = Convert.ToDecimal(stack.Pop());
                            summ = Convert.ToDecimal(Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a)));
                        } 
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }                    
                    stack.Push(summ.ToString());
                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;                    
                }

            }
            return Convert.ToDecimal(stack.Pop());            
        }
    }
}
